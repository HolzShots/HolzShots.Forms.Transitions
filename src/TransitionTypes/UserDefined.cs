namespace HolzShots.Forms.Transitions.TransitionTypes;

/// <summary>
/// This class allows you to create user-defined transition types. You specify these
/// as a list of TransitionElements. Each of these defines:
/// End time , End value, Interpolation method
///
/// For example, say you want to make a bouncing effect with a decay:
///
/// EndTime%    EndValue%   Interpolation
/// --------    ---------   -------------
/// 50          100         Acceleration
/// 75          50          Deceleration
/// 85          100         Acceleration
/// 91          75          Deceleration
/// 95          100         Acceleration
/// 98          90          Deceleration
/// 100         100         Acceleration
///
/// The time values are expressed as a percentage of the overall transition time. This
/// means that you can create a user-defined transition-type and then use it for transitions
/// of different lengths.
///
/// The values are percentages of the values between the start and end values of the properties
/// being animated in the transitions. 0% is the start value and 100% is the end value.
///
/// The interpolation is one of the values from the InterpolationMethod enum.
///
/// So the example above accelerates to the destination (as if under gravity) by
/// t=50%, then bounces back up to half the initial height by t=75%, slowing down
/// (as if against gravity) before falling down again and bouncing to decreasing
/// heights each time.
///
/// </summary>
public class UserDefined : ITransitionType
{
    private IList<TransitionElement> _elements;
    private float _transitionTime;
    private int _currentElement;

    public UserDefined() { }

    /// <summary>
    /// Constructor. You pass in the list of TransitionElements and the total time
    /// (in milliseconds) for the transition.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="transitionTime"/> is zero or negative.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="elements"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="elements"/> is empty, or its end times are not strictly increasing and greater than zero.
    /// </exception>
    public UserDefined(IList<TransitionElement> elements, int transitionTime)
    {
        Setup(elements, transitionTime);
    }

    /// <summary>
    /// Sets the list of TransitionElements and the total time (in milliseconds)
    /// for the transition. Also resets the current element.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="transitionTime"/> is zero or negative.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="elements"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="elements"/> is empty, or its end times are not strictly increasing and greater than zero.
    /// </exception>
    public void Setup(IList<TransitionElement> elements, int transitionTime)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(transitionTime);
        ArgumentNullException.ThrowIfNull(elements);

        // We check that the elements list has some members...
        if (elements.Count == 0)
            throw new ArgumentException("The list of elements must have at least one element.", nameof(elements));

        // End times must be strictly increasing and start above zero. Otherwise an
        // element would have a zero-length interval and OnTimer would divide by zero...
        var previousEndTime = 0.0f;
        foreach (var element in elements)
        {
            if (!(element.EndTime > previousEndTime))
                throw new ArgumentException($"Element end times must be strictly increasing and greater than zero, but found end time {element.EndTime} after {previousEndTime}.", nameof(elements));
            previousEndTime = element.EndTime;
        }

        _elements = elements;
        _transitionTime = transitionTime;
        _currentElement = 0;
    }

    /// <exception cref="NullReferenceException">The instance was created with the parameterless constructor and <see cref="Setup"/> was not called.</exception>
    /// <exception cref="Exception">An element has an <see cref="InterpolationMethod"/> that is not handled.</exception>
    public void OnTimer(int time, out float percentage, out bool completed)
    {
        var transitionTimeFraction = time / _transitionTime;
        GetElementInfo(
            transitionTimeFraction,
            out var elementStartTime,
            out var elementEndTime,
            out var elementStartValue,
            out var elementEndValue,
            out InterpolationMethod interpolationMethod
        );

        // We find how far through this element we are as a fraction...
        var elementInterval = elementEndTime - elementStartTime;
        var elementElapsedTime = transitionTimeFraction - elementStartTime;
        var elementTimeFraction = elementElapsedTime / elementInterval;

        // We convert the time-fraction to an fraction of the movement within the
        // element using the interpolation method...
        var elementDistance = interpolationMethod switch
        {
            InterpolationMethod.Linear => elementTimeFraction,
            InterpolationMethod.Acceleration => Utility.ConvertLinearToAcceleration(elementTimeFraction),
            InterpolationMethod.Deceleration => Utility.ConvertLinearToDeceleration(elementTimeFraction),
            InterpolationMethod.EaseInEaseOut => Utility.ConvertLinearToEaseInEaseOut(elementTimeFraction),
            _ => throw new Exception("Interpolation method not handled: " + interpolationMethod.ToString()),
        };

        // We now know how far through the transition we have moved, so we can interpolate
        // the start and end values by this amount...
        percentage = Utility.Interpolate(elementStartValue, elementEndValue, elementDistance);

        // Has the transition completed?
        if (time >= _transitionTime)
        {
            // The transition has completed, so we make sure that
            // it is at its final value...
            completed = true;
            percentage = elementEndValue;
        }
        else
        {
            completed = false;
        }
    }

    /// <summary>Returns the element info for the time-fraction passed in.</summary>
    private void GetElementInfo(float timeFraction, out float startTime, out float endTime, out float startValue, out float endValue, out InterpolationMethod interpolationMethod)
    {
        // We need to return the start and end values for the current element. So this
        // means finding the element for the time passed in as well as the previous element.

        // We hold the 'current' element as a hint. This was in fact the
        // element used the last time this function was called. In most cases
        // it will be the same one again, but it may have moved to a subsequent
        // on (maybe even skipping elements if enough time has passed)...
        int count = _elements.Count;

        // The hint may also be too far ahead: this happens when the same
        // transition-type instance is reused for a new transition, or a
        // transition is run again. So we first move back while the time is
        // before the end of the previous element...
        while (_currentElement > 0 && timeFraction < _elements[_currentElement - 1].EndTime / 100.0f)
            --_currentElement;

        for (; _currentElement < count; ++_currentElement)
        {
            var element = _elements[_currentElement];
            var elementEndTime = element.EndTime / 100.0f;
            if (timeFraction < elementEndTime)
                break;
        }

        // If we have gone past the last element, we just use the last element...
        if (_currentElement == count)
        {
            _currentElement = count - 1;
        }

        // We find the start values. These come from the previous element, except in the
        // case where we are currently in the first element, in which case they are zeros...
        startTime = 0.0f;
        startValue = 0.0f;
        if (_currentElement > 0)
        {
            var previousElement = _elements[_currentElement - 1];
            startTime = previousElement.EndTime / 100.0f;
            startValue = previousElement.EndValue / 100.0f;
        }

        // We get the end values from the current element...
        var currentElement = _elements[_currentElement];
        endTime = currentElement.EndTime / 100.0f;
        endValue = currentElement.EndValue / 100.0f;
        interpolationMethod = currentElement.InterpolationMethod;
    }
}

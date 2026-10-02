namespace HolzShots.Forms.Transitions.TransitionTypes;

/// <summary>
/// This class manages a linear transition. The percentage complete for the transition
/// increases linearly with time.
/// </summary>
public class Linear : ITransitionType
{
    private readonly float _transitionTime;

    /// <summary>Constructor. You pass in the time that the transition will take (in milliseconds).</summary>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="transitionTime"/> is zero or negative.</exception>
    public Linear(int transitionTime)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(transitionTime);
        _transitionTime = transitionTime;
    }

    public void OnTimer(int time, out float percentage, out bool completed)
    {
        percentage = time / _transitionTime;
        if (percentage >= 1.0)
        {
            percentage = 1.0f;
            completed = true;
        }
        else
        {
            completed = false;
        }
    }
}

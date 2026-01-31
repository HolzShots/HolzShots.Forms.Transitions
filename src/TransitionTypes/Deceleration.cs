namespace HolzShots.Forms.Transitions.TransitionTypes;

/// <summary>
/// Manages a transition starting from a high speed and decelerating to zero by
/// the end of the transition.
/// </summary>
public class Deceleration : ITransitionType
{
    private readonly float _transitionTime;
    public Deceleration(int transitionTime)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(transitionTime, 0);
        _transitionTime = transitionTime;
    }

    /// <summary>
    /// Works out the percentage completed given the time passed in.
    /// This uses the formula:
    ///   s = ut + 1/2at^2
    /// The initial velocity is 2, and the acceleration to get to 1.0
    /// at t=1.0 is -2, so the formula becomes:
    ///   s = t(2-t)
    /// </summary>
    public void OnTimer(int time, out float percentage, out bool completed)
    {
        // We find the percentage time elapsed...
        var elapsed = time / _transitionTime;
        percentage = elapsed * (2.0f - elapsed);
        if (elapsed >= 1.0)
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

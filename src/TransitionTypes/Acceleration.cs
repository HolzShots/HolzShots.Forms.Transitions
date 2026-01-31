namespace HolzShots.Forms.Transitions.TransitionTypes;

/// <summary>
/// Manages transitions under constant acceleration from a standing start.
/// </summary>
public class Acceleration : ITransitionType
{
    private readonly float _transitionTime;
    /// <summary>
    /// Constructor. You pass in the time that the transition
    /// will take (in milliseconds).
    /// </summary>
    public Acceleration(int transitionTime)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(transitionTime, 0);
        _transitionTime = transitionTime;
    }

    /// <summary>
    /// Works out the percentage completed given the time passed in.
    /// This uses the formula:
    ///   s = ut + 1/2at^2
    /// The initial velocity is 0, and the acceleration to get to 1.0
    /// at t=1.0 is 2, so the formula just becomes:
    ///   s = t^2
    /// </summary>
    public void OnTimer(int time, out float percentage, out bool completed)
    {
        // We find the percentage time elapsed...
        var elapsed = time / _transitionTime;
        percentage = elapsed * elapsed;
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

namespace HolzShots.Forms.Transitions.TransitionTypes;

/// <summary>
/// This transition animates with an exponential decay. This has a damping effect
/// similar to the motion of a needle on an electomagnetically controlled dial.
/// </summary>
public class CriticalDamping : ITransitionType
{
    #region Public methods

    /// <summary>
    /// Constructor. You pass in the time that the transition
    /// will take (in milliseconds).
    /// </summary>
    public CriticalDamping(int transitionTime)
    {
        if (transitionTime <= 0)
            throw new Exception("Transition time must be greater than zero.");

        _transitionTime = transitionTime;
    }

    #endregion

    #region ITransitionMethod Members

    public void OnTimer(int iTime, out float percentage, out bool completed)
    {
        var elapsed = iTime / _transitionTime;
        percentage = (1.0f - MathF.Exp(-1.0f * elapsed * 5f)) / 0.993262053f;

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

    #endregion

    #region Private data

    private readonly float _transitionTime;

    #endregion
}

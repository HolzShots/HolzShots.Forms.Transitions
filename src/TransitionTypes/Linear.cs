namespace HolzShots.Forms.Transitions.TransitionTypes;

/// <summary>
/// This class manages a linear transition. The percentage complete for the transition
/// increases linearly with time.
/// </summary>
public class Linear : ITransitionType
{
    #region Public methods

    /// <summary>
    /// Constructor. You pass in the time (in milliseconds) that the
    /// transition will take.
    /// </summary>
    public Linear(int iTransitionTime)
    {
        if (iTransitionTime <= 0)
            throw new Exception("Transition time must be greater than zero.");

        _transitionTime = iTransitionTime;
    }

    #endregion

    #region ITransitionMethod Members

    /// <summary>
    /// We return the percentage completed.
    /// </summary>
    public void OnTimer(int iTime, out double dPercentage, out bool bCompleted)
    {
        dPercentage = (iTime / _transitionTime);
        if (dPercentage >= 1.0)
        {
            dPercentage = 1.0;
            bCompleted = true;
        }
        else
        {
            bCompleted = false;
        }
    }

    #endregion

    #region Private data

    private readonly double _transitionTime;

    #endregion
}

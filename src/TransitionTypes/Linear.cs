namespace HolzShots.Forms.Transitions.TransitionTypes;

/// <summary>
/// This class manages a linear transition. The percentage complete for the transition
/// increases linearly with time.
/// </summary>
public class Linear : ITransitionType
{
    private readonly float _transitionTime;
    public Linear(int transitionTime)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(transitionTime, 0);
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

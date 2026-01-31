namespace HolzShots.Forms.Transitions.TransitionTypes;

/// <summary>
/// This transition animates with an exponential decay. This has a damping effect
/// similar to the motion of a needle on an electomagnetically controlled dial.
/// </summary>
public class CriticalDamping : ITransitionType
{
    private readonly float _transitionTime;
    public CriticalDamping(int transitionTime)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(transitionTime, 0);
        _transitionTime = transitionTime;
    }

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
}

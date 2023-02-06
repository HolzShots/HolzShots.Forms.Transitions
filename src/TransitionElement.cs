
namespace HolzShots.Forms.Transitions;

public enum InterpolationMethod
{
    Linear,
    Acceleration,
    Deceleration,
    EaseInEaseOut
}

/// <param name="EndTime">The percentage of elapsed time, expressed as (for example) 75 for 75%.</param>
/// <param name="EndValue">
/// The value of the animated properties at the EndTime. This is the percentage
/// movement of the properties between their start and end values. This should
/// be expressed as (for example) 75 for 75%.
/// </param>
/// <param name="InterpolationMethod">
/// The interpolation method to use when moving between the previous value
/// and the current one.
/// </param>
public record TransitionElement(double EndTime, double EndValue, InterpolationMethod InterpolationMethod);

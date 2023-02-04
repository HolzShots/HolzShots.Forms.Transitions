
namespace Forms.Transitions
{
    public enum InterpolationMethod
    {
        Linear,
        Acceleration,
        Deceleration,
        EaseInEaseOut
    }

    public class TransitionElement
    {
        public TransitionElement(double endTime, double endValue, InterpolationMethod interpolationMethod)
        {
            EndTime = endTime;
            EndValue = endValue;
            InterpolationMethod = interpolationMethod;
        }

        /// <summary>
        /// The percentage of elapsed time, expressed as (for example) 75 for 75%.
        /// </summary>
        public double EndTime { get; }

        /// <summary>
        /// The value of the animated properties at the EndTime. This is the percentage
        /// movement of the properties between their start and end values. This should
        /// be expressed as (for example) 75 for 75%.
        /// </summary>
        public double EndValue { get; }

        /// <summary>
        /// The interpolation method to use when moving between the previous value
        /// and the current one.
        /// </summary>
        public InterpolationMethod InterpolationMethod { get; }
    }
}

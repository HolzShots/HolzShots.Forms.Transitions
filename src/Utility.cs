using System.ComponentModel;
using System.Numerics;

namespace HolzShots.Forms.Transitions
{
    internal class Utility
    {
        /// <summary>Returns the value of the property passed in.</summary>
        public static object GetValue(object target, string propertyName)
        {
            Type targetType = target.GetType();
            var propertyInfo = targetType.GetProperty(propertyName) ?? throw new Exception($"Object: {target} does not have the property: {propertyName}");
            return propertyInfo.GetValue(target, null);
        }

        /// <summary>Sets the value of the property passed in.</summary>
        public static void SetValue(object target, string propertyName, object value)
        {
            Type targetType = target.GetType();
            var propertyInfo = targetType.GetProperty(propertyName) ?? throw new Exception($"Object: {target} does not have the property: {propertyName}");
            propertyInfo.SetValue(target, value, null);
        }

        public static double Interpolate(double a, double b, double percentage) => a + ((b - a) * percentage);
        public static int Interpolate(int a, int b, float percentage) => (int)(a + ((b - a) * percentage));
        public static float Interpolate(float a, float b, float percentage) => a + ((b - a) * percentage);
        public static Vector2 Interpolate(Vector2 a, Vector2 b, float percentage) => a + ((b - a) * percentage);
        public static Vector3 Interpolate(Vector3 a, Vector3 b, float percentage) => a + ((b - a) * percentage);
        public static Vector4 Interpolate(Vector4 a, Vector4 b, float percentage) => a + ((b - a) * percentage);

        /// <summary>
        /// Converts a fraction representing linear time to a fraction representing
        /// the distance traveled under an ease-in-ease-out transition.
        /// </summary>
        public static float ConvertLinearToEaseInEaseOut(float elapsed)
        {
            // The distance traveled is made up of two parts: the initial acceleration,
            // and then the subsequent deceleration...
            var firstHalfTime = (elapsed > 0.5f) ? 0.5f : elapsed;
            var secondHalfTime = (elapsed > 0.5f) ? elapsed - 0.5f : 0.0f;
            return 2 * firstHalfTime * firstHalfTime + 2 * secondHalfTime * (1.0f - secondHalfTime);
        }

        /// <summary>
        /// Converts a fraction representing linear time to a fraction representing
        /// the distance traveled under a constant acceleration transition.
        /// </summary>
        public static float ConvertLinearToAcceleration(float elapsed) => elapsed * elapsed;

        /// <summary>
        /// Converts a fraction representing linear time to a fraction representing
        /// the distance traveled under a constant deceleration transition.
        /// </summary>
        public static float ConvertLinearToDeceleration(float elapsed) => elapsed * (2.0f - elapsed);

        /// <summary>
        /// Fires the event passed in in a thread-safe way.
        /// </summary><remarks>
        /// This method loops through the targets of the event and invokes each in turn. If the
        /// target supports ISychronizeInvoke (such as forms or controls) and is set to run
        /// on a different thread, then we call BeginInvoke to marshal the event to the target
        /// thread. If the target does not support this interface (such as most non-form classes)
        /// or we are on the same thread as the target, then the event is fired on the same
        /// thread as this is called from.
        /// </remarks>
        public static void RaiseEvent<T>(EventHandler<T> theEvent, object sender, T args) where T : EventArgs
        {
            if (theEvent == null)
                return;


            // We loop through each of the delegate handlers for this event. For each of
            // them we need to decide whether to invoke it on the current thread or to
            // make a cross-thread invocation...
            foreach (EventHandler<T> handler in theEvent.GetInvocationList())
            {
                try
                {
                    if (handler.Target is not ISynchronizeInvoke target || !target.InvokeRequired)
                    {
                        // Either the target is not a form or control, or we are already
                        // on the right thread for it. Either way we can just fire the
                        // event as normal...
                        handler(sender, args);
                    }
                    else
                    {
                        // The target is most likely a form or control that needs the
                        // handler to be invoked on its own thread...
                        target.BeginInvoke(handler, [sender, args]);
                    }
                }
                catch (Exception)
                {
                    // The event handler may have been detached while processing the events.
                    // We just ignore this and invoke the remaining handlers.
                }
            }
        }
    }
}

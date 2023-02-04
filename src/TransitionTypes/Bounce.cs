﻿using System.Collections.Generic;

namespace Forms.Transitions.TransitionTypes
{
    /// <summary>
    /// This transition bounces the property to a destination value and back to the
    /// original value. It is accelerated to the destination and then decelerated back
    /// as if being dropped with gravity and bouncing back against gravity.
    /// </summary>
    public class Bounce : UserDefined
    {
        #region Public methods

        /// <summary>
        /// Constructor. You pass in the total time taken for the bounce.
        /// </summary>
        public Bounce(int iTransitionTime)
        {
            // We create a custom "user-defined" transition to do the work...
            var elements = new List<TransitionElement>() {
                new TransitionElement(50, 100, InterpolationMethod.Acceleration),
                new TransitionElement(100, 0, InterpolationMethod.Deceleration),
            };
            Setup(elements, iTransitionTime);
        }

        #endregion
    }
}

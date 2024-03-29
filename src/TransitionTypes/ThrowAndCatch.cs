﻿using System.Collections.Generic;

namespace HolzShots.Forms.Transitions.TransitionTypes;

/// <summary>
/// This transition bounces the property to a destination value and back to the
/// original value. It is decelerated to the destination and then acclerated back
/// as if being thrown against gravity and then descending back with gravity.
/// </summary>
public class ThrowAndCatch : UserDefined
{
    #region Public methods

    /// <summary>
    /// Constructor. You pass in the total time taken for the bounce.
    /// </summary>
    public ThrowAndCatch(int iTransitionTime)
    {
        // We create a custom "user-defined" transition to do the work...
        var elements = new List<TransitionElement>() {
            new(50, 100, InterpolationMethod.Deceleration),
            new(100, 0, InterpolationMethod.Acceleration),
        };
        Setup(elements, iTransitionTime);
    }

    #endregion
}

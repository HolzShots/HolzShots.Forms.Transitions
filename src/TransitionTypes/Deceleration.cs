﻿using System;

namespace HolzShots.Forms.Transitions.TransitionTypes;

/// <summary>
/// Manages a transition starting from a high speed and decelerating to zero by
/// the end of the transition.
/// </summary>
public class Deceleration : ITransitionType
{
    #region Public methods

    /// <summary>
    /// Constructor. You pass in the time that the transition
    /// will take (in milliseconds).
    /// </summary>
    public Deceleration(int iTransitionTime)
    {
        if (iTransitionTime <= 0)
            throw new Exception("Transition time must be greater than zero.");

        _transitionTime = iTransitionTime;
    }

    #endregion

    #region ITransitionMethod Members

    /// <summary>
    /// Works out the percentage completed given the time passed in.
    /// This uses the formula:
    ///   s = ut + 1/2at^2
    /// The initial velocity is 2, and the acceleration to get to 1.0
    /// at t=1.0 is -2, so the formula becomes:
    ///   s = t(2-t)
    /// </summary>
    public void OnTimer(int iTime, out double dPercentage, out bool bCompleted)
    {
        // We find the percentage time elapsed...
        double dElapsed = iTime / _transitionTime;
        dPercentage = dElapsed * (2.0 - dElapsed);
        if (dElapsed >= 1.0)
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

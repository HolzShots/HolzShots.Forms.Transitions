﻿using System;

namespace Forms.Transitions.ManagedTypes
{
    /// <summary>
    /// Manages transitions for double properties.
    /// </summary>
    internal class DoubleManagedType : IManagedType
    {
        #region IManagedType Members

        /// <summary>
        ///  Returns the type managed by this class.
        /// </summary>
        public Type GetManagedType() => typeof(double);

        /// <summary>
        /// Returns a copy of the double passed in.
        /// </summary>
        public object Copy(object o) => (double)o;

        /// <summary>
        /// Returns the value between start and end for the percentage passed in.
        /// </summary>
        public object GetIntermediateValue(object start, object end, double dPercentage)
        {
            double dStart = (double)start;
            double dEnd = (double)end;
            return Utility.Interpolate(dStart, dEnd, dPercentage);
        }

        #endregion
    }
}

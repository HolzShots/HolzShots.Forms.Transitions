﻿using System;

namespace Transitions.ManagedTypes
{
    /// <summary>
    /// Manages transitions for int properties.
    /// </summary>
    internal class Int32ManagedType : IManagedType
    {
        #region IManagedType Members

        /// <summary>
        /// Returns the type we are managing.
        /// </summary>
        public Type GetManagedType() => typeof(int);

        /// <summary>
        /// Returns a copy of the int passed in.
        /// </summary>
        public object Copy(object o) => (int)o;

        /// <summary>
        /// Returns the value between the start and end for the percentage passed in.
        /// </summary>
        public object GetIntermediateValue(object start, object end, double dPercentage)
        {
            int iStart = (int)start;
            int iEnd = (int)end;
            return Utility.Interpolate(iStart, iEnd, dPercentage);
        }

        #endregion
    }
}

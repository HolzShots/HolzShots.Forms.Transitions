using System;
using System.Drawing;

namespace HolzShots.Forms.Transitions.ManagedTypes;

/// <summary>
/// Class that manages transitions for Point properties.
/// </summary>
internal class PointManagedType : IManagedType
{
    #region IManagedType Members

    /// <summary>
    /// Returns the type we are managing.
    /// </summary>
    public Type GetManagedType() => typeof(Point);

    /// <summary>
    /// Returns a copy of the point object passed in.
    /// </summary>
    public object Copy(object o)
    {
        var c = (Point)o;
        return new Point(c.X, c.Y);
    }

    /// <summary>
    /// Creates an intermediate value for the points depending on the percentage passed in.
    /// </summary>
    public object GetIntermediateValue(object start, object end, double dPercentage)
    {
        var startPoint = (Point)start;
        var endPoint = (Point)end;

        int newX = Utility.Interpolate(startPoint.X, endPoint.X, dPercentage);
        int newY = Utility.Interpolate(startPoint.Y, endPoint.Y, dPercentage);

        return new Point(newX, newY);
    }

    #endregion
}

namespace HolzShots.Forms.Transitions.ManagedTypes;


/// <summary>
/// Class that manages transitions for Rectangle properties.
/// </summary>
internal class RectangleManagedType : IManagedType
{
    #region IManagedType Members

    /// <summary>
    /// Returns the type we are managing.
    /// </summary>
    public Type GetManagedType() => typeof(Rectangle);

    /// <summary>
    /// Returns a copy of the rectangle object passed in.
    /// </summary>
    public object Copy(object o)
    {
        var c = (Rectangle)o;
        return new Rectangle(c.X, c.Y, c.Width, c.Height);
    }

    /// <summary>
    /// Creates an intermediate value for the rectangles depending on the percentage passed in.
    /// </summary>
    public object GetIntermediateValue(object start, object end, float percentage)
    {
        var startRectangle = (Rectangle)start;
        var endRectangle = (Rectangle)end;

        int newX = Utility.Interpolate(startRectangle.X, endRectangle.X, percentage);
        int newY = Utility.Interpolate(startRectangle.Y, endRectangle.Y, percentage);
        int newWidth = Utility.Interpolate(startRectangle.Width, endRectangle.Width, percentage);
        int newHeight = Utility.Interpolate(startRectangle.Height, endRectangle.Height, percentage);

        return new Rectangle(newX, newY, newWidth, newHeight);
    }

    #endregion
}

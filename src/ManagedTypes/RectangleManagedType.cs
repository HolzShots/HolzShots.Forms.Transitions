namespace HolzShots.Forms.Transitions.ManagedTypes;


internal class RectangleManagedType : IManagedType
{
    public Type GetManagedType() => typeof(Rectangle);
    public object Copy(object o)
    {
        var c = (Rectangle)o;
        return new Rectangle(c.X, c.Y, c.Width, c.Height);
    }
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
}

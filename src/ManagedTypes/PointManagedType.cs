namespace HolzShots.Forms.Transitions.ManagedTypes;

internal class PointManagedType : IManagedType
{
    public Type GetManagedType() => typeof(Point);
    public object Copy(object o)
    {
        var c = (Point)o;
        return new Point(c.X, c.Y);
    }
    public object GetIntermediateValue(object start, object end, float percentage)
    {
        var startPoint = (Point)start;
        var endPoint = (Point)end;

        int newX = Utility.Interpolate(startPoint.X, endPoint.X, percentage);
        int newY = Utility.Interpolate(startPoint.Y, endPoint.Y, percentage);

        return new Point(newX, newY);
    }
}

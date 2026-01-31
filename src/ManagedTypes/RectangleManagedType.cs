using System.Numerics;

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

        var startVector = new Vector4(
            startRectangle.X,
            startRectangle.Y,
            startRectangle.Width,
            startRectangle.Height
        );
        var endVector = new Vector4(
            endRectangle.X,
            endRectangle.Y,
            endRectangle.Width,
            endRectangle.Height
        );

        var res = Utility.Interpolate(startVector, endVector, percentage);
        return new Rectangle((int)res.X, (int)res.Y, (int)res.Z, (int)res.W);
    }
}

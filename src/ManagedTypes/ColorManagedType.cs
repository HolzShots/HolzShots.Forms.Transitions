using System.Numerics;

namespace HolzShots.Forms.Transitions.ManagedTypes;

internal class ColorManagedType : IManagedType
{
    public Type GetManagedType() => typeof(Color);
    public object Copy(object o) => Color.FromArgb(((Color)o).ToArgb());
    public object GetIntermediateValue(object start, object end, float percentage)
    {
        var startColor = (Color)start;
        var endColor = (Color)end;
        var startVector = new Vector4(
            startColor.A,
            startColor.R,
            startColor.G,
            startColor.B
        );
        var endVector = new Vector4(
            endColor.A,
            endColor.R,
            endColor.G,
            endColor.B
        );

        var res = Utility.Interpolate(startVector, endVector, percentage);

        return Color.FromArgb((int)res.X, (int)res.Y, (int)res.Z, (int)res.W);
    }
}

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

        // Transition types may overshoot (percentage > 1) or undershoot (percentage < 0),
        // which would push components out of the valid 0..255 range and make
        // Color.FromArgb throw. So we clamp them...
        res = Vector4.Clamp(res, Vector4.Zero, new Vector4(255f));

        return Color.FromArgb((int)res.X, (int)res.Y, (int)res.Z, (int)res.W);
    }
}

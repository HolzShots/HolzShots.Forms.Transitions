namespace HolzShots.Forms.Transitions.ManagedTypes;

internal class ColorManagedType : IManagedType
{
    public Type GetManagedType() => typeof(Color);
    public object Copy(object o) => Color.FromArgb(((Color)o).ToArgb());
    public object GetIntermediateValue(object start, object end, float percentage)
    {
        var startColor = (Color)start;
        var endColor = (Color)end;
        return Color.FromArgb(
            Utility.Interpolate(startColor.A, endColor.A, percentage),
            Utility.Interpolate(startColor.R, endColor.R, percentage),
            Utility.Interpolate(startColor.G, endColor.G, percentage),
            Utility.Interpolate(startColor.B, endColor.B, percentage)
        );
    }
}

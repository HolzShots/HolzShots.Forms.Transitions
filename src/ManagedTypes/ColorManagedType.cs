namespace HolzShots.Forms.Transitions.ManagedTypes;

/// <summary>
/// Class that manages transitions for Color properties. For these we
/// need to transition the R, G, B and A sub-properties independently.
/// </summary>
internal class ColorManagedType : IManagedType
{
    #region IManagedType Members

    /// <summary>
    /// Returns the type we are managing.
    /// </summary>
    public Type GetManagedType() => typeof(Color);

    /// <summary>
    /// Returns a copy of the color object passed in.
    /// </summary>
    public object Copy(object o)
    {
        var c = (Color)o;
        var result = Color.FromArgb(c.ToArgb());
        return result;
    }

    /// <summary>
    /// Creates an intermediate value for the colors depending on the percentage passed in.
    /// </summary>
    public object GetIntermediateValue(object start, object end, float percentage)
    {
        var startColor = (Color)start;
        var endColor = (Color)end;

        // We interpolate the R, G, B and A components separately...
        int new_R = Utility.Interpolate(startColor.R, endColor.R, percentage);
        int new_G = Utility.Interpolate(startColor.G, endColor.G, percentage);
        int new_B = Utility.Interpolate(startColor.B, endColor.B, percentage);
        int new_A = Utility.Interpolate(startColor.A, endColor.A, percentage);
        return Color.FromArgb(new_A, new_R, new_G, new_B);
    }

    #endregion
}

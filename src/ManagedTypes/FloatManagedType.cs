namespace HolzShots.Forms.Transitions.ManagedTypes;

internal class FloatManagedType : IManagedType
{
    public Type GetManagedType() => typeof(float);
    public object Copy(object o) => (float)o;
    public object GetIntermediateValue(object start, object end, float percentage) => Utility.Interpolate((float)start, (float)end, percentage);
}

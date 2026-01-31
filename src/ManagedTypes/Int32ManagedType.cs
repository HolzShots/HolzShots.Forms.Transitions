namespace HolzShots.Forms.Transitions.ManagedTypes;

internal class Int32ManagedType : IManagedType
{
    public Type GetManagedType() => typeof(int);
    public object Copy(object o) => (int)o;
    public object GetIntermediateValue(object start, object end, float percentage) => Utility.Interpolate((int)start, (int)end, percentage);
}

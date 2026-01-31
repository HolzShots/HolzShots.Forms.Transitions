namespace HolzShots.Forms.Transitions.ManagedTypes;

internal class DoubleManagedType : IManagedType
{
    public Type GetManagedType() => typeof(double);
    public object Copy(object o) => (double)o;
    public object GetIntermediateValue(object start, object end, float percentage) => Utility.Interpolate((double)start, (double)end, percentage);
}

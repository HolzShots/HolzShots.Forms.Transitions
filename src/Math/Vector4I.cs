using System.Numerics;
using System.Runtime.Intrinsics;

namespace HolzShots.Forms.Transitions.Math;

public readonly struct Vector4I : IEquatable<Vector4I>
{
    public static readonly Vector4I Zero = new(0, 0, 0, 0);
    public static readonly Vector4I One = new(1, 1, 1, 1);

    private readonly Vector128<int> _v;

    public int X => _v.GetElement(0);
    public int Y => _v.GetElement(1);
    public int Z => _v.GetElement(2);
    public int W => _v.GetElement(3);

    public Vector4I(int x, int y, int z, int w) => _v = Vector128.Create(x, y, z, w);
    private Vector4I(Vector128<int> v) => _v = v;

    public static Vector4I operator +(Vector4I a, Vector4I b) => new(a._v + b._v);
    public static Vector4I operator -(Vector4I a, Vector4I b) => new(a._v - b._v);
    public static Vector4I operator *(Vector4I v, int scalar) => new(v._v * scalar);
    public static Vector4I operator /(Vector4I v, int scalar) => new(v.X / scalar, v.Y / scalar, v.Z / scalar, v.W / scalar); // division does not support SIMD

    public static bool operator ==(Vector4I a, Vector4I b) => a.Equals(b);
    public static bool operator !=(Vector4I a, Vector4I b) => !a.Equals(b);

    public static implicit operator Vector4(Vector4I v) => new(v.X, v.Y, v.Z, v.W);
    public static explicit operator Vector4I(Vector4 v) => new((int)v.X, (int)v.Y, (int)v.Z, (int)v.W);

    public int LengthSquared() => Vector128.Sum(_v * _v);

    public override string ToString() => $"({X}, {Y}, {Z}, {W})";
    public override bool Equals(object? obj) => obj is Vector4I other && Equals(other);
    public bool Equals(Vector4I other) => X == other.X && Y == other.Y && Z == other.Z && W == other.W;
    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

using System.Numerics;

namespace HolzShots.Forms.Transitions.Math;

public readonly struct Vector4I(int x, int y, int z, int w) : IEquatable<Vector4I>
{
    public readonly int X = x;
    public readonly int Y = y;
    public readonly int Z = z;
    public readonly int W = w;

    public static readonly Vector4I Zero = new(0, 0, 0, 0);
    public static readonly Vector4I One = new(1, 1, 1, 1);

    public static Vector4I operator +(Vector4I a, Vector4I b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
    public static Vector4I operator -(Vector4I a, Vector4I b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
    public static Vector4I operator *(Vector4I v, int scalar) => new(v.X * scalar, v.Y * scalar, v.Z * scalar, v.W * scalar);
    public static Vector4I operator /(Vector4I v, int scalar) => new(v.X / scalar, v.Y / scalar, v.Z / scalar, v.W / scalar);

    public static bool operator ==(Vector4I a, Vector4I b) => a.Equals(b);
    public static bool operator !=(Vector4I a, Vector4I b) => !a.Equals(b);
    public static implicit operator Vector4(Vector4I v) => new(v.X, v.Y, v.Z, v.W);
    public static explicit operator Vector4I(Vector4 v) => new((int)v.X, (int)v.Y, (int)v.Z, (int)v.W);

    public int LengthSquared() => X * X + Y * Y + Z * Z + W * W;
    public override string ToString() => $"({X}, {Y}, {Z}, {W})";
    public override bool Equals(object? obj) => obj is Vector4I other && Equals(other);
    public bool Equals(Vector4I other) => X == other.X && Y == other.Y && Z == other.Z && W == other.W;
    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

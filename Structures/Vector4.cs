using System;

namespace KG_Task4.Structures
{
    internal struct Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public float Length => (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W);

        public Vector4 Normalized => this == Vector4.Zero ? new Vector4(0, 0, 0, 1) : this * (1 / Length);

        public Vector3 ToVector3() => new Vector3(X / W, Y / W, Z / W);

        #region Constructors
        public static Vector4 Zero => new Vector4(0, 0, 0, 0);
        #endregion

        #region Operations

        public static bool operator ==(Vector4 a, Vector4 b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;

        public static bool operator !=(Vector4 a, Vector4 b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.W != b.W;

        public static Vector4 operator +(Vector4 a) => a;

        public static Vector4 operator -(Vector4 a) => new Vector4(-a.X, -a.Y, -a.Z, -a.W);

        public static Vector4 operator +(Vector4 a, Vector4 b) => new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);

        public static Vector4 operator -(Vector4 a, Vector4 b) => a + (-b);

        public static float operator *(Vector4 a, Vector4 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;

        public static Vector4 operator *(float a, Vector4 b) => new Vector4(a * b.X, a * b.Y, a * b.Z, a * b.W);

        public static Vector4 operator *(Vector4 a, float b) => b * a;

        #endregion


        #region Overrides
        public override string ToString() => $"({X}, {Y}, {Z}, {W})";

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
        #endregion
    }
}

using System;

namespace KG_Task4.Structures
{
    internal struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float Length => (float)Math.Sqrt(X * X + Y * Y + Z * Z);

        public Vector3 Normalized => this == Vector3.Zero ? new Vector3(1,0,0) : this * (1 / Length);

        public Vector4 ToVector4() => new Vector4(X, Y, Z, 1);

        // ?
        public static float Angle(Vector3 a, Vector3 b) => (float)Math.Acos(a * b);

        #region Constructors
        public static Vector3 Zero => new Vector3(0,0,0);
        #endregion

        #region Operations
        public static bool operator ==(Vector3 a, Vector3 b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;

        public static bool operator !=(Vector3 a, Vector3 b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z;

        public static Vector3 operator +(Vector3 a) => a;

        public static Vector3 operator -(Vector3 a) => new Vector3(-a.X, -a.Y, -a.Z);

        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Vector3 operator -(Vector3 a, Vector3 b) => a + (-b);

        public static float operator *(Vector3 a, Vector3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static Vector3 operator *(float a, Vector3 b) => new Vector3(a * b.X, a * b.Y, a * b.Z);

        public static Vector3 operator *(Vector3 a, float b) => b * a;

        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
                );
        }

        #endregion

        public override string ToString() => $"({X}, {Y}, {Z})";

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }
}

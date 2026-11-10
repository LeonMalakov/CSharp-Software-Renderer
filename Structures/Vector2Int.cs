using System;

namespace KG_Task4.Structures
{
    internal struct Vector2Int
    {
        public int X;
        public int Y;

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public float Length => (float)Math.Sqrt(X * X + Y * Y);


        #region Constructors
        public static Vector2Int Zero => new Vector2Int(0, 0);
        #endregion

        #region Operations
        public static bool operator ==(Vector2Int a, Vector2Int b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Vector2Int a, Vector2Int b) => a.X != b.X || a.Y != b.Y;

        public static Vector2Int operator +(Vector2Int a) => a;

        public static Vector2Int operator -(Vector2Int a) => new Vector2Int(-a.X, -a.Y);

        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new Vector2Int(a.X + b.X, a.Y + b.Y);

        public static Vector2Int operator -(Vector2Int a, Vector2Int b) => a + (-b);

        public static int operator *(Vector2Int a, Vector2Int b) => a.X * b.X + a.Y * b.Y;

        public static Vector2Int operator *(int a, Vector2Int b) => new Vector2Int(a * b.X, a * b.Y);

        public static Vector2Int operator *(Vector2Int a, int b) => b * a;

        #endregion

        public override string ToString() => $"({X}, {Y})";

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }
}

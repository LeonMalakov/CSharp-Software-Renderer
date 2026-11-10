using System;

namespace KG_Task4.Structures
{
    internal struct Matrix4
    {
        public Vector4 Row0;
        public Vector4 Row1;
        public Vector4 Row2;
        public Vector4 Row3;

        public Matrix4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3) : this()
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }

        public Vector4 Column0 => new Vector4(Row0.X, Row1.X, Row2.X, Row3.X);
        public Vector4 Column1 => new Vector4(Row0.Y, Row1.Y, Row2.Y, Row3.Y);
        public Vector4 Column2 => new Vector4(Row0.Z, Row1.Z, Row2.Z, Row3.Z);
        public Vector4 Column3 => new Vector4(Row0.W, Row1.W, Row2.W, Row3.W);


        #region Constructors
        public static Matrix4 Identity => new Matrix4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1)
            );

        public static Matrix4 CreateProjectionMatrixByFovX(float near, float far, float fovx)
        {
            float s = (float)(1 / Math.Tan(Utilities.DegToRad(fovx) * 0.5f));
            float fDivfMinusN = -far / (far - near);

            return new Matrix4(
                new Vector4(s, 0, 0, 0),
                new Vector4(0, s, 0, 0),
                new Vector4(0, 0, fDivfMinusN, -1),
                new Vector4(0, 0, fDivfMinusN * near, 0)
                );
        }

        public static Matrix4 CreateProjectionMatrixByFovY(float near, float far, float fovy)
        {
            float s = (float)(1 / Math.Tan(Utilities.DegToRad(fovy) * 0.5f));
            
            return new Matrix4(
                new Vector4(s, 0, 0, 0),
                new Vector4(0, s, 0, 0),
                new Vector4(0, 0, (far + near) / (far - near), 1),
                new Vector4(0, 0, - 2*far*near/(far - near), 0)
                );
        }

        public static Matrix4 CreateScaleMatrix(Vector3 v) => new Matrix4(
            new Vector4(v.X, 0, 0, 0),
            new Vector4(0, v.Y, 0, 0),
            new Vector4(0, 0, v.Z, 0),
            new Vector4(0, 0, 0, 1)
            );

        public static Matrix4 CreateTranslationMatrix(Vector3 v) => new Matrix4(
            new Vector4(1, 0, 0, v.X),
            new Vector4(0, 1, 0, v.Y),
            new Vector4(0, 0, 1, v.Z),
            new Vector4(0, 0, 0, 1)
            );

        public static Matrix4 CreateRotationX(float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);

            return new Matrix4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, cos, -sin, 0),
                new Vector4(0, sin, cos, 0),
                new Vector4(0, 0, 0, 1)
            );
        }

        public static Matrix4 CreateRotationY(float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);

            return new Matrix4(
                new Vector4(cos, 0, sin, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(-sin, 0, cos, 0),
                new Vector4(0, 0, 0, 1)
            );
        }

        public static Matrix4 CreateRotationZ(float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);

            return new Matrix4(
                new Vector4(cos, -sin, 0, 0),
                new Vector4(sin, cos, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1)
          );
        }

        public static Matrix4 CreateRotationByAxis(Vector3 axis, float angle)
        {
            axis = axis.Normalized;
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            float oneMinusCos = 1 - cos;

            return new Matrix4(
                new Vector4(
                (float)(cos + axis.X * axis.X * oneMinusCos),
                (float)(axis.X * axis.Y * oneMinusCos - axis.Z * sin),
                (float)(axis.X * axis.Z * oneMinusCos + axis.Y * sin),
                0),
                new Vector4(
                (float)(axis.Y * axis.X * oneMinusCos + axis.Z * sin),
                (float)(cos + axis.Y * axis.Y * oneMinusCos),
                (float)(axis.X * axis.Z * oneMinusCos - axis.X * sin),
                0),
                new Vector4(
                (float)(axis.Z * axis.X * oneMinusCos - axis.Y * sin),
                (float)(axis.X * axis.Y * oneMinusCos + axis.X * sin),
                (float)(cos + axis.Z * axis.Z * oneMinusCos),
                0),
                new Vector4(0, 0, 0, 1)
            );
        }

        public static Matrix4 CreateRotationFromQuaternion(Quaternion q)
        {
            return new Matrix4(
                new Vector4(
                    q.W * q.W + q.X * q.X - q.Y * q.Y - q.Z * q.Z,
                    2 * q.X * q.Y - 2 * q.Z * q.W,
                    2 * q.X * q.Z + 2 * q.Y * q.W,
                    0),
                new Vector4(
                    2 * q.X * q.Y + 2 * q.Z * q.W,
                    q.W * q.W - q.X * q.X + q.Y * q.Y - q.Z * q.Z,
                    2 * q.Y * q.Z - 2 * q.X * q.W,
                    0),
                new Vector4(
                    2 * q.X * q.Z - 2 * q.Y * q.W,
                    2 * q.Y * q.Z + 2 * q.X * q.W,
                    q.W * q.W - q.X * q.X - q.Y * q.Y + q.Z * q.Z,
                    0),
                new Vector4(0, 0, 0, 1)
                );
        }
        #endregion


        #region Operations
        public static Matrix4 operator +(Matrix4 a) => a;

        public static Matrix4 operator -(Matrix4 a) => new Matrix4(-a.Row0, -a.Row1, -a.Row2, -a.Row3);

        public static Matrix4 operator +(Matrix4 a, Matrix4 b) =>
            new Matrix4(a.Row0 + b.Row0, a.Row1 + b.Row1, a.Row2 + b.Row2, a.Row3 + b.Row3);

        public static Matrix4 operator -(Matrix4 a, Matrix4 b) => a + (-b);

        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            Vector4 r0 = new Vector4(a.Row0 * b.Column0, a.Row0 * b.Column1, a.Row0 * b.Column2, a.Row0 * b.Column3);
            Vector4 r1 = new Vector4(a.Row1 * b.Column0, a.Row1 * b.Column1, a.Row1 * b.Column2, a.Row1 * b.Column3);
            Vector4 r2 = new Vector4(a.Row2 * b.Column0, a.Row2 * b.Column1, a.Row2 * b.Column2, a.Row2 * b.Column3);
            Vector4 r3 = new Vector4(a.Row3 * b.Column0, a.Row3 * b.Column1, a.Row3 * b.Column2, a.Row3 * b.Column3);

            return new Matrix4(r0, r1, r2, r3);
        }

        public static Matrix4 operator *(float a, Matrix4 b) => new Matrix4(a * b.Row0, a * b.Row1, a * b.Row2, a * b.Row3);

        public static Vector4 operator *(Matrix4 a, Vector4 b) => new Vector4(a.Row0 * b, a.Row1 * b, a.Row2 * b, a.Row3 * b);

        #endregion

        public override string ToString() => $"{Row0.ToString()}\n{Row1.ToString()}\n{Row2.ToString()}\n{Row3.ToString()}";
    }
}

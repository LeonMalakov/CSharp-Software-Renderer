using System;

namespace KG_Task4.Structures
{
    internal struct Quaternion
    {
        public float W;

        public Vector3 XYZ;

        public float X => XYZ.X;
        public float Y => XYZ.Y;
        public float Z => XYZ.Z;

        public Quaternion(float w, Vector3 xyz)
        {
            W = w;
            XYZ = xyz;
        }

        public static Quaternion Identity => new Quaternion(1, Vector3.Zero);

        public Quaternion Inversed
        { 
            get
            {
                float d = 1f / (W * W + XYZ.X * XYZ.X + XYZ.Y * XYZ.Y + XYZ.Z * XYZ.Z);
                return new Quaternion(W * d, (-XYZ) * d);
            } 
        }

        public Vector3 ToEuler()
        {
            Vector3 v;

            // roll (x-axis rotation)
            double sinr_cosp = 2 * (W * XYZ.X + XYZ.Y * XYZ.Z);
            double cosr_cosp = 1 - 2 * (XYZ.X * XYZ.X + XYZ.Y * XYZ.Y);
            v.X = (float)Math.Atan2(sinr_cosp, cosr_cosp);

            // pitch (y-axis rotation)
            double sinp = 2 * (W * XYZ.Y - XYZ.Z * XYZ.X);
            if (Math.Abs(sinp) >= 1)
                v.Y = (float)Math.PI / 2 * (Math.Sign(sinp) != 0 ? Math.Sign(sinp) : 1);
            else
                v.Y = (float)Math.Asin(sinp);

            // yaw (z-axis rotation)
            double siny_cosp = 2 * (W * XYZ.Z + XYZ.X * XYZ.Y);
            double cosy_cosp = 1 - 2 * (XYZ.Y * XYZ.Y + XYZ.Z * XYZ.Z);
            v.Z = (float)Math.Atan2(siny_cosp, cosy_cosp);

            return Utilities.RadToDeg(v);
        }

        public static Quaternion FromEuler(Vector3 v)
        {
            v = Utilities.DegToRad(v);

            float cosX = (float)Math.Cos(v.X * 0.5);
            float sinX = (float)Math.Sin(v.X * 0.5);
            float cosZ = (float)Math.Cos(v.Z * 0.5f);
            float sinZ = (float)Math.Sin(v.Z * 0.5f);
            float cosY = (float)Math.Cos(v.Y * 0.5);
            float sinY = (float)Math.Sin(v.Y * 0.5);

            return new Quaternion(
                cosX * cosY * cosZ + sinX * sinY * sinZ,
                new Vector3(
                sinX * cosY * cosZ - cosX * sinY * sinZ,
                cosX * sinY * cosZ + sinX * cosY * sinZ,
                cosX * cosY * sinZ - sinX * sinY * cosZ)
                );
        }

        #region Operations

        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            Vector3 xyz = Vector3.Cross( a.XYZ, b.XYZ) + a.W * b.XYZ + b.W * a.XYZ;
            float w = a.W * b.W - (a.XYZ * b.XYZ);

            return new Quaternion(w, xyz);
        }

        #endregion
    }
}

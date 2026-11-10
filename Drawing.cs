using KG_Task4.Structures;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace KG_Task4
{
    internal static class Drawing
    {
        public static void DrawLine(Vector2Int p0, Vector2Int p1, Bitmap map, Color color)
        {
            bool mainY = false;
            if (Math.Abs(p0.X - p1.X) < Math.Abs(p0.Y - p1.Y))
            {
                int t = p0.X;
                p0.X = p0.Y;
                p0.Y = t;

                t = p1.X;
                p1.X = p1.Y;
                p1.Y = t;

                mainY = true;
            }
            if (p0.X > p1.X)
            {
                int t = p0.X;
                p0.X = p1.X;
                p1.X = t;

                t = p0.Y;
                p0.Y = p1.Y;
                p1.Y = t;
            }
            int dx = p1.X - p0.X;
            int dy = p1.Y - p0.Y;
            int derror2 = Math.Abs(dy) * 2;
            int error2 = 0;
            int y = p0.Y;
            for (int x = p0.X; x <= p1.X; x++)
            {
                if (mainY)
                {
                    map.SetPixel(y, x, color);
                }
                else
                {
                    map.SetPixel(x, y, color);
                }
                error2 += derror2;
                if (error2 > dx)
                {
                    y += (p1.Y > p0.Y ? 1 : -1);
                    error2 -= dx * 2;
                }
            }
        }

        public static void DrawTriangle(Vector3 p0, Vector3 p1, Vector3 p2, Bitmap map, BitmapData data, float near, float far, float[] zBuffer, Color color)
        {
            // Values for checking if pixel is inside triangle.      
            float x_s0 = p1.X * p0.Y - p0.X * p1.Y;
            float x_s1 = p0.X - p1.X;
            float x_s2 = p1.Y - p0.Y;

            float y_s0 = -(p2.X * p0.Y - p0.X * p2.Y);
            float y_s1 = -(p0.X - p2.X);
            float y_s2 = -(p2.Y - p0.Y);

            float x;
            float y;
            float z = p2.X * (p1.Y - p0.Y) + p0.X * (p2.Y - p1.Y) + p1.X * (p0.Y - p2.Y);
            //

            // Depth test values.
            float dt_x_s = p1.Y * p2.Z - p1.Y * p0.Z - p0.Y * p2.Z - p2.Y * p1.Z + p2.Y * p0.Z + p0.Y * p1.Z;
            float dt_y_s = p2.X * p1.Z - p2.X * p0.Z - p0.X * p1.Z - p1.X * p2.Z + p1.X * p0.Z + p0.X * p2.Z;
            float dt_z_s = p1.X * p2.Y - p1.X * p0.Y - p0.X * p2.Y - p2.X * p1.Y + p2.X * p0.Y + p0.X * p1.Y;
            //

            // Bounds box.
            Vector2Int boundsMin = new Vector2Int(map.Width - 1, map.Height - 1);
            Vector2Int boundsMax = new Vector2Int(0, 0);

            boundsMax.X = (int)Math.Min(boundsMin.X, Math.Max(boundsMax.X, Math.Max(p0.X, Math.Max(p1.X, p2.X))));
            boundsMax.Y = (int)Math.Min(boundsMin.Y, Math.Max(boundsMax.Y, Math.Max(p0.Y, Math.Max(p1.Y, p2.Y))));
            boundsMin.X = (int)Math.Max(0, Math.Min(boundsMin.X, Math.Min(p0.X, Math.Min(p1.X, p2.X))));
            boundsMin.Y = (int)Math.Max(0, Math.Min(boundsMin.Y, Math.Min(p0.Y, Math.Min(p1.Y, p2.Y))));
            //

            Vector2Int point;

            int stride = data.Stride;

            unsafe
            {
                byte* ptr = (byte*)data.Scan0;

                for (point.X = boundsMin.X; point.X <= boundsMax.X; point.X++)
                {
                    for (point.Y = boundsMin.Y; point.Y <= boundsMax.Y; point.Y++)
                    {
                        // Check if pixel is inside triangle.                        
                        x = x_s0 + x_s1 * point.Y + x_s2 * point.X;
                        y = y_s0 + y_s1 * point.Y + y_s2 * point.X;

                        if (1 - (x + y) / z < 0 ||
                            y / z < 0 ||
                            x / z < 0)
                            continue;
                        //

                        // Depth test.
                        float dt_z = (((p0.X - point.X) * dt_x_s + (p0.Y - point.Y) * dt_y_s) / dt_z_s) + p0.Z;

                        if (dt_z < near || dt_z > far || zBuffer[point.X + point.Y * map.Width] <= dt_z) continue;

                        zBuffer[point.X + point.Y * map.Width] = dt_z;
                        //

                        // Set pixel.
                        ptr[(point.X * 3) + point.Y * stride] = color.B;
                        ptr[(point.X * 3) + point.Y * stride + 1] = color.G;
                        ptr[(point.X * 3) + point.Y * stride + 2] = color.R;
                        //  map.SetPixel(point.X, point.Y, color);
                    }
                }
            }


        }


        public static bool IsPointInsideTriangle(Vector2Int p0, Vector2Int p1, Vector2Int p2, Vector2Int point)
        {
            Vector3 v = TriangleCoords(p0, p1, p2, point);

            return v.X > 0 && v.Y > 0 && v.Z > 0;
        }

        public static Vector3 TriangleCoords(Vector2Int p0, Vector2Int p1, Vector2Int p2, Vector2Int p)
        {
            Vector3 v = Vector3.Cross(
                new Vector3(p2.X - p0.X, p1.X - p0.X, p0.X - p.X),
                new Vector3(p2.Y - p0.Y, p1.Y - p0.Y, p0.Y - p.Y));

            if (Math.Abs(v.Z) < 1) return new Vector3(-1, 1, 1);

            return new Vector3(1 - (v.X + v.Y) / v.Z, v.Y / v.Z, v.X / v.Z);
        }
    }
}

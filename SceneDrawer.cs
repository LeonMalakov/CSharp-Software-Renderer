using KG_Task4.Structures;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace KG_Task4
{
    internal class SceneDrawer
    {
        private const float ViewScale = 2;
        private const float NearClipPlane = 0.005f;
        private const float FarClipPlane = 100;
        private Bitmap _map;
        private Graphics _graphics;
        private Color _color;

        private float _scaleFactor;
        private float _aspect;
        private int _screenHeight;
        private int _screenWidth;

        private BitmapData _mapData;
        private float[] _zBuffer;

        private float _nearClip;
        private float _farClip;

        public Scene Scene { get; set; }

        public Camera Camera { get; private set; }

        public SceneDrawer()
        {
            Camera = new Camera();
        }

        public Bitmap InitializeMap(int width, int height)
        {
            if (width <= 0 || height <= 0) return null;

            // Screen size values.
            _screenWidth = width;
            _screenHeight = height;
            _aspect = _screenWidth / _screenHeight;
            _scaleFactor = _screenHeight / ViewScale;

            // Initialize bitmap and depth-buffer.
            _map = new Bitmap(_screenWidth, _screenHeight, PixelFormat.Format24bppRgb);
            _zBuffer = new float[_screenWidth * _screenHeight];

            // Graphics.
            _graphics = Graphics.FromImage(_map);
            _color = Color.Gold;

            // Near/Far.
            _nearClip = (Camera.ProjectionMatrix * new Vector4(0, 0, NearClipPlane, 1)).ToVector3().Z;
            _farClip = (Camera.ProjectionMatrix * new Vector4(0, 0, FarClipPlane, 1)).ToVector3().Z;

            return _map;
        }

        public void Update()
        {
            if (Scene == null) return;

            // Start renderer timer.
            long ticks = DateTime.UtcNow.Ticks;

            // Clear map.
            _graphics.Clear(Scene.ClearColor);

            // Clear depth-buffer.
            for (int i = 0; i < _zBuffer.Length; i++) _zBuffer[i] = float.MaxValue;

            // Drawing optimization: lock map bits for manual setup.
            _mapData = _map.LockBits(new Rectangle(0, 0, _map.Width, _map.Height), ImageLockMode.ReadWrite, _map.PixelFormat);

            foreach (SceneObject o in Scene.Objects)
            {
                DrawObject(o);
            }

            // Drawing optimization: unlock map bits after image ready.
            _map.UnlockBits(_mapData);

            // Stop renderer timer. And place result in left-bottom of screen.
            ticks = DateTime.UtcNow.Ticks - ticks;
            _graphics.DrawString((ticks / (float)TimeSpan.TicksPerSecond).ToString(), SystemFonts.MessageBoxFont, Brushes.Black, 20, _screenHeight - 20);

        }

        private void DrawObject(SceneObject o)
        {          
            Matrix4 transf = o.Transform.CreateTransformationMatrix();
            Matrix4 view = Camera.CreateViewMatrix();
            Matrix4 proj = Camera.ProjectionMatrix;

            Vector3[] verts = o.Mesh.Vertices;
            (int, int, int)[] facesVerts = o.Mesh.FacesVertices;

            for (int i = 0; i < facesVerts.Length; i++)
            {
                // Model to world.
                Vector4 wpoint0 = transf * verts[facesVerts[i].Item1].ToVector4();
                Vector4 wpoint1 = transf * verts[facesVerts[i].Item2].ToVector4();
                Vector4 wpoint2 = transf * verts[facesVerts[i].Item3].ToVector4();

                // World to camera-view.
                wpoint0 = view * wpoint0;
                wpoint1 = view * wpoint1;
                wpoint2 = view * wpoint2;

                // Near/Far clipping.  
                if (wpoint0.Z < NearClipPlane || wpoint0.Z > FarClipPlane ||
                    wpoint1.Z < NearClipPlane || wpoint1.Z > FarClipPlane ||
                    wpoint2.Z < NearClipPlane || wpoint2.Z > FarClipPlane)
                    continue;

                // Projection.
                Vector3 cpoint0 = (proj * wpoint0).ToVector3();
                Vector3 cpoint1 = (proj * wpoint1).ToVector3();
                Vector3 cpoint2 = (proj * wpoint2).ToVector3();

                // Face normal.
                Vector3 normal = -Vector3.Cross(cpoint2 - cpoint0, cpoint1 - cpoint0).Normalized;

                // Backface culling.
                if (normal * new Vector3(0, 0, -1) <= 0) continue;

                // To screen space.
                Vector3 spoint0 = WorldToScreenPoint(cpoint0);
                Vector3 spoint1 = WorldToScreenPoint(cpoint1);
                Vector3 spoint2 = WorldToScreenPoint(cpoint2);

                // Lighting.
                float intensivity = normal * Scene.LightDirection;
                intensivity = Utilities.Clamp(intensivity, 0, 1);

                Color drawColor = Color.FromArgb((int)(intensivity * _color.R),
                    (int)(intensivity * _color.G), (int)(intensivity * _color.B));


                // Draw Triangle.
                Drawing.DrawTriangle(spoint0, spoint1, spoint2, _map, _mapData, _nearClip, _farClip, _zBuffer, drawColor);
            }

        }

        private Vector3 WorldToScreenPoint(Vector3 v)
        {
            return new Vector3((v.X * _scaleFactor + (_screenWidth / 2f)), (v.Y * _scaleFactor + (_screenHeight / 2f)), v.Z);
        }

        private (float, float) ScreenToWorldPoint(PointF p)
        {
            return (p.X - (_screenWidth / 2f) / _scaleFactor, (p.Y - _screenHeight / 2f) / _scaleFactor);
        }

    }
}

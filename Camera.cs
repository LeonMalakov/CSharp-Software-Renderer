using KG_Task4.Structures;

namespace KG_Task4
{
    internal class Camera
    {
        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; } = Quaternion.Identity;

        public Matrix4 ProjectionMatrix { get; private set; }

        public Camera()
        {
            ProjectionMatrix = Matrix4.CreateProjectionMatrixByFovY(.5f, 100, 80);
        }

        public void SetFOV(float fov)
        {
            ProjectionMatrix = Matrix4.CreateProjectionMatrixByFovY(.5f, 100, fov);
        }

        public Matrix4 CreateViewMatrix()
        {
            return Matrix4.CreateTranslationMatrix(-Position) * Matrix4.CreateRotationFromQuaternion(Rotation.Inversed);
        }
    }
}

using KG_Task4.Structures;

namespace KG_Task4
{
    internal class Transformation
    {
        private Vector3 _position;

        private Quaternion _rotation;

        private Vector3 _scale;

        public Vector3 Position { get => _position; set => _position = value; }

        public Quaternion Rotation { get => _rotation; set => _rotation = value; }

        public Vector3 Euler { get => _rotation.ToEuler(); set => _rotation = Quaternion.FromEuler(value); }

        public Vector3 Scale { get => _scale; set => _scale = value; }

        public Matrix4 CreateTransformationMatrix()
        {
            return Matrix4.CreateTranslationMatrix(_position) * 
                Matrix4.CreateRotationFromQuaternion(_rotation) * 
                Matrix4.CreateScaleMatrix(_scale);
        }
    }
}

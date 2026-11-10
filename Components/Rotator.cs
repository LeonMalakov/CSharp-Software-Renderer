using KG_Task4.Structures;

namespace KG_Task4
{
    internal class Rotator : Component
    {
        public Vector3 RotationEuler;

        protected override void OnInitialize()
        {
            Environment.Instance.BeforeFrameUpdate += FrameUpdate;
        }

        protected override void OnDestroy()
        {
            Environment.Instance.BeforeFrameUpdate -= FrameUpdate;
        }

        private void FrameUpdate(float deltaTime)
        {
            _object.Transform.Rotation *= Quaternion.FromEuler(RotationEuler * deltaTime);
        }
    }
}

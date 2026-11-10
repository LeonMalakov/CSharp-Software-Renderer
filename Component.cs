namespace KG_Task4
{
    internal abstract class Component
    {
        protected SceneObject _object;

        public void Initialize(SceneObject so)
        {
            if (_object != null)
                throw new System.Exception("Component can not be attached to few objects.");

            _object = so;

            OnInitialize();
        }

        public void Destroy()
        {
            OnDestroy();
        }

        protected abstract void OnInitialize();

        protected abstract void OnDestroy();
    }
}

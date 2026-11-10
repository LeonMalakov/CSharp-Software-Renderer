using KG_Task4.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace KG_Task4
{
    internal class Scene
    {
        private List<SceneObject> _objects = new List<SceneObject>();

        public event Action ListModified;

        public Color ClearColor { get; set; } = Color.White;

        public Vector3 LightDirection { get; set; } = new Vector3(-0.5f,0f,-1).Normalized;

        public IReadOnlyList<SceneObject> Objects => _objects;

        public void AddObject(SceneObject o)
        {
            _objects.Add(o);

            ListModified?.Invoke();
        }

        public bool RemoveObject(SceneObject o)
        {
            if (_objects.Contains(o))
            {
                _objects.Remove(o);

                ListModified?.Invoke();

                return true;
            }

            return false;
        }
    }
}

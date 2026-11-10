using System;
using System.Collections.Generic;
using System.Linq;
using KG_Task4.Structures;

namespace KG_Task4
{
    internal class SceneObject
    {
        private Dictionary<Type, Component> _components = new Dictionary<Type, Component>();

     
        public string Name { get; private set; }
        
        public Transformation Transform { get; } = new Transformation();

        public Mesh Mesh { get; set; }


        public Component[] GetAllComponents()
        {
            return _components.Values.ToArray();
        }

        public T GetComponent<T>() where T:Component
        {
            if (_components.ContainsKey(typeof(T))) 
                return _components[typeof(T)] as T;

            return null;
        }

        public T AddComponent<T>() where T : Component, new()
        {
            if (!_components.ContainsKey(typeof(T)))
            {
                T comp = new T();
                _components.Add(typeof(T), comp);
                comp.Initialize(this);
                return comp;
            }

            return null;
        }

        public Component AddComponent(Type compType)
        {
            if (!_components.ContainsKey(compType))
            {
                Component comp = (Component)System.Activator.CreateInstance(compType);
                _components.Add(compType, comp);
                comp.Initialize(this);
                return comp;
            }

            return null;
        }

        public bool RemoveComponent(Component comp)
        {
            if (_components.ContainsValue(comp))
            {
                comp.Destroy();
                _components.Remove(comp.GetType());
                return true;
            }

            return false;
        }

        public void Destroy()
        {
            foreach (Component c in GetAllComponents())
                c.Destroy();
        }

        public SceneObject(string name)
        {
            Transform.Position = Vector3.Zero;
            Transform.Rotation = Quaternion.Identity;
            Transform.Scale = new Vector3(1, 1, 1);

            Name = name;
        }

        public SceneObject(string name, Mesh mesh)
        {
            Transform.Position = Vector3.Zero;
            Transform.Rotation = Quaternion.Identity;
            Transform.Scale = new Vector3(1,1,1);
            Mesh = mesh;

            Name = name;
        }
    }
}

using System.IO;
using KG_Task4.Structures;

namespace KG_Task4
{
    internal static class PrimitivesLibrary
    {
        public static Mesh Quad =>
               new Mesh()
               {
                   Vertices = new Vector3[] {
                    new Vector3(-1, -1, 0),
                    new Vector3(1, -1, 0),
                    new Vector3(1, 1, 0),
                    new Vector3(-1, 1, 0)
                    },

                   FacesVertices = new (int, int, int)[] {
                        (0, 1, 2),
                        (0, 3, 2)
                   }
               };

        private static Mesh cube;

        private static Mesh icosahedron;

        public static Mesh Cube
        {
            get
            {
                if (cube == null)
                {
                    string path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Primitives", "Primitive_Cube.obj");
                    cube = ObjLoader.LoadFromFile(path);

                    if (cube == null)
                    {
                        throw new System.Exception("Primitive 'Cube' could not be loaded.");
                    }
                }

                return cube;
            }
        }

        public static Mesh Icosahedron
        {
            get
            {
                if (icosahedron == null)
                {
                    string path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Primitives", "Primitive_Icosahedron.obj");
                    icosahedron = ObjLoader.LoadFromFile(path);

                    if (icosahedron == null)
                    {
                        throw new System.Exception("Primitive 'Icosahedron' could not be loaded.");
                    }
                }

                return icosahedron;
            }
        }

    }
}

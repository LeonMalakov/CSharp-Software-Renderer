using System.IO;

namespace KG_Task4
{
    internal static class ObjectsLibrary
    {
        public static string[] GetObjectsNames()
        {
            string path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Objects");

            string[] files = Directory.GetFiles(path);

            for (int i = 0; i < files.Length; i++)
            { 
                string s = Path.GetFileName(files[i]);
                files[i] = s.Substring(0, s.Length - 4);
            }

            return files;
        }

        public static Mesh LoadObject(string name)
        {
            string path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Objects", name + ".obj");
            Mesh mesh = ObjLoader.LoadFromFile(path);

            return mesh;
        }

    }
}

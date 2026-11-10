using System.Collections.Generic;
using System.IO;
using KG_Task4.Structures;

namespace KG_Task4
{
    internal static class ObjLoader
    {
        public static Mesh LoadFromFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                return LoadFromString(sr.ReadToEnd());
            }
        }

        public static Mesh LoadFromString(string obj)
        {
            string[] lines = obj.Split('\n');

            List<Vector3> verts = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();

            List<(int, int, int)> faces = new List<(int, int, int)>();
            List<(int, int, int)> normalsInds = new List<(int, int, int)>();

            foreach (string line in lines)
            {
                // Vertices.
                if (line.StartsWith("v "))
                {
                    if (!ParseVectorLine(line, out Vector3 v)) return null;
                    verts.Add(v);
                }
                else if (line.StartsWith("vn "))
                {
                    if (!ParseVectorLine(line, out Vector3 n)) return null;
                    normals.Add(n);
                }
                else if (line.StartsWith("f "))
                {
                    if (!ParseFaceLine(line, out (int, int, int) f, out (int, int, int) n)) return null;
                    faces.Add(f);
                    normalsInds.Add(n);
                }
            }

            Mesh mesh = new Mesh() { Vertices = verts.ToArray(), Normals = normals.ToArray(),
                FacesVertices = faces.ToArray(), FacesNormals = normalsInds.ToArray() };

            return mesh;
        }

        private static bool ParseVectorLine(string line, out Vector3 v)
        {
            v = new Vector3();
            string[] values = line.Split(' ');

            if (values.Length < 4)
                return false;


            if (!Utilities.FloatTryParse(values[1], out v.X)) return false;

            if (!Utilities.FloatTryParse(values[2], out v.Y)) return false;

            if (!Utilities.FloatTryParse(values[3], out v.Z)) return false;


            return true;
        }

        private static bool ParseFaceLine(string line, out (int, int, int) f, out (int, int, int) n)
        {
            f = default;
            n = default;

            string[] values = line.Split(' ');

            if (values.Length < 4) return false;

            if (!ParseFaceIndexes(values[1], out f.Item1, out n.Item1)) return false;
            if (!ParseFaceIndexes(values[2], out f.Item2, out n.Item2)) return false;
            if (!ParseFaceIndexes(values[3], out f.Item3, out n.Item3)) return false;

            return true;
        }

        private static bool ParseFaceIndexes(string line, out int vertInd, out int normInd)
        {
            vertInd = 0;
            normInd = 0;

            string[] values = line.Split('/');

            // Vert index.
            if (values.Length >= 1)
            {
                if (!int.TryParse(values[0], out vertInd))
                    return false;

                vertInd--;
            }

            // UV index.
            if (values.Length >= 2)
            {

            }

            // Normal index.
            if (values.Length >= 3)
            {
                int.TryParse(values[2], out normInd);

                normInd--;
            }

            return true;
        }

    }
}

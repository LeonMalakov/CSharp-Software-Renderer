using KG_Task4.Structures;

namespace KG_Task4
{
    internal class Mesh
    {
        public Vector3[] Vertices { get; set; }

        public Vector3[] Normals { get; set; }

        public (int, int, int)[] FacesVertices { get; set; }

        public (int, int, int)[] FacesNormals { get; set; }

        public Mesh()
        {
        }

        public int[] GetIndices()
        {
            int[] indices = new int[FacesVertices.Length * 3];

            for (int i = 0; i < FacesVertices.Length; i++)
            {
                indices[i * 3] = FacesVertices[i].Item1;
                indices[i * 3 + 1] = FacesVertices[i].Item2;
                indices[i * 3 + 2] = FacesVertices[i].Item3;
            }

            return indices;
        }
    }
}

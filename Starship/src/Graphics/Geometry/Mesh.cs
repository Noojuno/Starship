using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Starship.Graphics.Geometry
{
    public class Mesh
    {
        public List<Vector3> Vertices = new List<Vector3>();
        public List<Vector3> Normals = new List<Vector3>();
        public List<Vector2> UV = new List<Vector2>();
        public List<int> Triangles = new List<int>();
        public VertexPosition[] VertexPositions;

        public void AddVertex(Vector3 pos)
        {
            this.Vertices.Add(pos);
        }

        public void AddNormal(Vector3 pos)
        {
            this.Normals.Add(pos);
        }

        public void AddUV(Vector2 uv)
        {
            this.UV.Add(uv);
        }

        public void AddTriangle(int tri)
        {
            this.Triangles.Add(tri);
        }

        public VertexPosition[] ToVertexPositions()
        {
            this.VertexPositions = new VertexPosition[this.Vertices.Count];

            var i = 0;
            foreach (var vertex in this.Vertices)
            {
                this.VertexPositions[i++] = new VertexPosition(vertex);
            }

            return this.VertexPositions;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Starship.Graphics.Geometry;
using Starship.Mathematics;

namespace Starship.Graphics
{
    [Serializable]
    public class MeshData
    {
        protected List<List<int>> triangles = new List<List<int>>();
        protected List<Vector2> uv = new List<Vector2>();
        protected List<Vector3> vertices = new List<Vector3>();

        public MeshData() : this(1)
        {
        }

        public MeshData(int subMeshCount = 1)
        {
            this.SubMeshCount = subMeshCount;
        }

        public virtual int SubMeshCount
        {
            get { return this.triangles.Count; }
            set
            {
                if (value > this.triangles.Count)
                {
                    var difference = value - this.triangles.Count;
                    for (var i = 0; i < difference; i++) this.triangles.Add(new List<int>());
                }
                else if (this.triangles.Count > value)
                {
                    var difference = this.triangles.Count - value;
                    for (var i = 0; i < difference; i++) this.triangles.RemoveAt(this.triangles.Count - i);
                }
            }
        }

        public virtual List<Vector3> Vertices => this.vertices;

        public virtual List<List<int>> Triangles => this.triangles;

        public virtual List<Vector2> UV => this.uv;

        public virtual void AddVertex(Vector3 vertex)
        {
            this.vertices.Add(vertex);
        }

        public virtual void AddTriangle(int subMesh, int triangle)
        {
            if (subMesh >= this.triangles.Count) return;
            this.triangles[subMesh].Add(triangle);
        }

        public virtual void AddUV(Vector2 uv)
        {
            this.uv.Add(uv);
        }

        public virtual void AddCube(Vector3 position, Rectangle[] uvs)
        {
            this.AddCube(position, 0, uvs);
        }

        public virtual void AddCube(Vector3 position, int subMesh, Rectangle[] uvs)
        {
            this.AddQuad(position, subMesh, Direction.Forward, uvs[0]);
            this.AddQuad(position, subMesh, Direction.Back, uvs[1]);
            this.AddQuad(position, subMesh, Direction.Right, uvs[2]);
            this.AddQuad(position, subMesh, Direction.Left, uvs[3]);
            this.AddQuad(position, subMesh, Direction.Up, uvs[4]);
            this.AddQuad(position, subMesh, Direction.Down, uvs[5]);
        }

        public virtual void AddQuad(Vector3 position, Direction direction, Rectangle uv)
        {
            this.AddQuad(position, 0, direction, uv);
        }

        public virtual void AddQuad(Vector3 position, int subMesh, Direction direction, Rectangle uv)
        {
            switch (direction)
            {
                case Direction.Forward:
                    this.vertices.Add(position + Vector3.Forward + Vector3.Left);
                    this.vertices.Add(position + Vector3.Forward);
                    this.vertices.Add(position + Vector3.Forward + Vector3.Left + Vector3.Up);
                    this.vertices.Add(position + Vector3.Forward + Vector3.Up);
                    break;
                case Direction.Back:
                    this.vertices.Add(position);
                    this.vertices.Add(position + Vector3.Left);
                    this.vertices.Add(position + Vector3.Up);
                    this.vertices.Add(position + Vector3.Left + Vector3.Up);
                    break;
                case Direction.Right:
                    this.vertices.Add(position + Vector3.Forward);
                    this.vertices.Add(position);
                    this.vertices.Add(position + Vector3.Forward + Vector3.Up);
                    this.vertices.Add(position + Vector3.Up);
                    break;
                case Direction.Left:
                    this.vertices.Add(position + Vector3.Left);
                    this.vertices.Add(position + Vector3.Left + Vector3.Forward);
                    this.vertices.Add(position + Vector3.Left + Vector3.Up);
                    this.vertices.Add(position + Vector3.Left + Vector3.Forward + Vector3.Up);
                    break;
                case Direction.Up:
                    this.vertices.Add(position + Vector3.Up);
                    this.vertices.Add(position + Vector3.Up + Vector3.Left);
                    this.vertices.Add(position + Vector3.Up + Vector3.Forward);
                    this.vertices.Add(position + Vector3.Up + Vector3.Forward + Vector3.Left);
                    break;
                case Direction.Down:
                    this.vertices.Add(position + Vector3.Forward);
                    this.vertices.Add(position + Vector3.Forward + Vector3.Left);
                    this.vertices.Add(position);
                    this.vertices.Add(position + Vector3.Left);
                    break;
            }

            this.triangles[subMesh].Add(this.vertices.Count - 4);
            this.triangles[subMesh].Add(this.vertices.Count - 3);
            this.triangles[subMesh].Add(this.vertices.Count - 2);
            this.triangles[subMesh].Add(this.vertices.Count - 3);
            this.triangles[subMesh].Add(this.vertices.Count - 1);
            this.triangles[subMesh].Add(this.vertices.Count - 2);
            this.uv.Add(new Vector2(uv.X + uv.Width, uv.Y));
            this.uv.Add(new Vector2(uv.X, uv.Y));
            this.uv.Add(new Vector2(uv.X + uv.Width, uv.Y + uv.Height));
            this.uv.Add(new Vector2(uv.X, uv.Y + uv.Height));
        }

        public VertexPosition[] ToVertexPositions()
        {
            var vPos = new VertexPosition[this.Vertices.Count];

            var i = 0;
            foreach (var vertex in this.Vertices)
            {
                vPos[i] = new VertexPosition(vertex);
            }

            return vPos;
        }

        public VertexBuffer ToVertexBuffer(GraphicsDevice device)
        {
            VertexBuffer buffer = new VertexBuffer(
                device,
                VertexPositionNormalTexture.VertexDeclaration,
                this.Vertices.Count,
                BufferUsage.WriteOnly);

            buffer.SetData(this.ToVertexPositions());

            return buffer;
        }

        public IndexBuffer ToIndexBuffer(GraphicsDevice device)
        {
            IndexBuffer buffer = new IndexBuffer(
                device,
                IndexElementSize.SixteenBits,
                this.triangles.Count,
                BufferUsage.WriteOnly);

            buffer.SetData(this.triangles[0].ToArray());

            return buffer;
        }

        /* public virtual Mesh ToMesh()
        {
            var mesh = new Mesh();
            mesh.subMeshCount = this.triangles.Count;
            mesh.vertices = this.vertices.ToArray();
            for (var i = 0; i < this.triangles.Count; i++) mesh.SetTriangles(this.triangles[i], i, true);
            mesh.uv = this.uv.ToArray();
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            return mesh;
        } */
    }
}
using Microsoft.Xna.Framework;

namespace Starship.Graphics.Geometry
{
    public class CubeMesh : Mesh
    {
        public CubeMesh(float size)
        {
            this.AddNormal(new Vector3(0, 0, -1));
            this.AddVertex(new Vector3(size, -size, -size));
            this.AddVertex(new Vector3(-size, -size, -size));
            this.AddVertex(new Vector3(-size, size, -size));
            this.AddVertex(new Vector3(size, size, -size));

            this.AddNormal(new Vector3(0, 0, 1));
            this.AddVertex(new Vector3(-size, -size, size));
            this.AddVertex(new Vector3(size, -size, size));
            this.AddVertex(new Vector3(size, size, size));
            this.AddVertex(new Vector3(-size, size, size));

            this.AddNormal(new Vector3(1, 0, 0));
            this.AddVertex(new Vector3(size, -size, size));
            this.AddVertex(new Vector3(size, -size, -size));
            this.AddVertex(new Vector3(size, size, -size));
            this.AddVertex(new Vector3(size, size, size));

            this.AddNormal(new Vector3(-1, 0, 0));
            this.AddVertex(new Vector3(-size, -size, -size));
            this.AddVertex(new Vector3(-size, -size, size));
            this.AddVertex(new Vector3(-size, size, size));
            this.AddVertex(new Vector3(-size, size, -size));

            this.AddNormal(new Vector3(0, -1, 0));
            this.AddVertex(new Vector3(-size, -size, -size));
            this.AddVertex(new Vector3(size, -size, -size));
            this.AddVertex(new Vector3(size, -size, size));
            this.AddVertex(new Vector3(-size, -size, size));

            this.AddNormal(new Vector3(0, 1, 0));
            this.AddVertex(new Vector3(size, size, -size));
            this.AddVertex(new Vector3(-size, size, -size));
            this.AddVertex(new Vector3(-size, size, size));
            this.AddVertex(new Vector3(size, size, size));

            //left
            this.AddUV(new Vector2(0.0f, 0.0f));
            this.AddUV(new Vector2(-1.0f, 1.0f));
            this.AddUV(new Vector2(-1.0f, 0.0f));
            this.AddUV(new Vector2(0.0f, 1.0f));
 
            // Back
            this.AddUV(new Vector2(0.0f, 0.0f));
            this.AddUV(new Vector2(0.0f, 1.0f));
            this.AddUV(new Vector2(-1.0f, 1.0f));
            this.AddUV(new Vector2(-1.0f, 0.0f));
 
            // right
            this.AddUV(new Vector2(-1.0f, 0.0f));
            this.AddUV(new Vector2(0.0f, 0.0f));
            this.AddUV(new Vector2(0.0f, 1.0f));
            this.AddUV(new Vector2(-1.0f, 1.0f));
 
            // top
            this.AddUV(new Vector2(0.0f, 0.0f));
            this.AddUV(new Vector2(0.0f, 1.0f));
            this.AddUV(new Vector2(-1.0f, 0.0f));
            this.AddUV(new Vector2(-1.0f, 1.0f));
 
            // front
            this.AddUV(new Vector2(0.0f, 0.0f));
            this.AddUV(new Vector2(1.0f, 1.0f));
            this.AddUV(new Vector2(0.0f, 1.0f));
            this.AddUV(new Vector2(1.0f, 0.0f));
 
            // bottom
            this.AddUV(new Vector2(0.0f, 0.0f));
            this.AddUV(new Vector2(0.0f, 1.0f));
            this.AddUV(new Vector2(-1.0f, 1.0f));
            this.AddUV(new Vector2(-1.0f, 0.0f));

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvZ.Blocks;
using Microsoft.Xna.Framework;
using Starship;
using Starship.Graphics;
using Starship.World;
using Starship.World.Blocks;

namespace Runed.Voxel
{
    public class MeshBuilder
    {
        public static MeshData BuildChunk(Chunk chunk)
        {
            var meshData = new MeshData();

            for (int x = 0; x < Chunk.Size; x++)
            {
                for (int y = 0; y < Chunk.Size; y++)
                {
                    for (int z = 0; z < Chunk.Size; z++)
                    {
                        var offsetPosition = new Vector3(x, y, z) + (chunk.Position * Chunk.Size);

                        if (chunk[x, y, z].Definition.ShouldRender())
                        {
                            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                            {
                                var adjacentBlock = chunk.World.GetAdjacentBlock(offsetPosition, direction);

                                if (adjacentBlock.Definition == BlockRegistry.Get("air") || !adjacentBlock.Definition.ShouldRender() /* ||
                                    adjacentBlock.Definition.HasCustomModel || adjacentBlock.Definition.Translucent */ &&
                                    adjacentBlock.Definition.GetRegistryName() != chunk[x, y, z].Definition.GetRegistryName())
                                {
                                    meshData.AddQuad(new Vector3(x, y, z), direction, Rectangle.Empty);//chunk[x, y, z].Definition.GetTexture(direction).UV);
                                }
                            }
                        }
                    }
                }
            }

            chunk.Dirty = false;

            return meshData;
        }
    }
}
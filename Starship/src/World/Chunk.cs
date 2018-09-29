using System;
using Microsoft.Xna.Framework;
using Starship.World.Blocks;

namespace Starship.World
{
    public class Chunk
    {
        // STATIC VARIABLES
        public static int Size = 16;

        // PUBLIC VARIABLES
        public World World;
        public Vector3 Position;

        public bool Dirty = true;
        public bool Loaded = false;
        public bool Rendered = false;

        // PRIVATE VARIABLES
        public Block[,,] Blocks { get; set; }

        public Chunk(World world, Vector3 position)
        {
            this.World = world;
            this.Position = position;

            this.Blocks = new Block[Chunk.Size, Chunk.Size, Chunk.Size];
        }

        public void RebuildAdjacentChunks()
        {
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (this.World.GetAdjacentChunk(this.Position, direction) != null)
                {
                    this.World.GetAdjacentChunk(this.Position, direction).Dirty = true;

                }
            }
        }

        /// <summary>
        /// Gets the blockDefinition at the given index.
        /// </summary>
        /// <param name="x">The x coordinate of the blockDefinition</param>
        /// <param name="y">The y coordinate of the blockDefinition</param>
        /// <param name="z">The z coordinate of the blockDefinition</param>
        /// <returns></returns>
        public Block this[int x, int y, int z]
        {
            get
            {
                if (x < 0 || x >= Chunk.Size || y < 0 || y >= Chunk.Size || z < 0 || z >= Chunk.Size)
                {
                    throw new ArgumentOutOfRangeException("Fix this to return the blockDefinition in the world instance.");
                }

                return this.Blocks[x, y, z];
            }
            set
            {
                if (x < 0 || x >= Chunk.Size || y < 0 || y >= Chunk.Size || z < 0 || z >= Chunk.Size)
                {
                    throw new ArgumentOutOfRangeException("Fix this to return the blockDefinition in the world instance.");
                }

                this.Blocks[x, y, z] = value;

                if (x == Chunk.Size - 1 || y == Chunk.Size - 1 || z == Chunk.Size - 1)
                {
                    this.RebuildAdjacentChunks();
                }

                this.Dirty = true;
            }
        }

        /// <summary>
        /// Gets the blockDefinition at the given index.
        /// </summary>
        /// <param name="index">The index as a Vector3i.</param>
        /// <returns></returns>
        public Block this[Vector3 index]
        {
            get => this[(int)index.X, (int)index.Y, (int)index.Z];
            set => this[(int)index.X, (int)index.Y, (int)index.Z] = value;
        }

    }
}
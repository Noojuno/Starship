using System;
using System.Security.Principal;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Starship.Mathematics;
using Starship.World.Blocks;

namespace Starship.World
{
    public class World
    {
        public int Seed = 0;
        public string Name = "world";
        
        // CONFIG
        //TODO: WorldConfig class?
        public static int MaxX = 100;
        public static int MaxY = 100;
        public static int MaxZ = 100;

        public static int MinX = -100;
        public static int MinY = -100;
        public static int MinZ = -100;

        //public TerrainGenerator TerrainGenerator;
        public Dictionary<Vector3, Chunk> _chunks;

        public World(int seed)
        {
            this.Seed = seed;

            //this.TerrainGenerator = new TerrainGenerator(this.Seed);
            this._chunks = new Dictionary<Vector3, Chunk>();
        }

        public Block GetBlock(int x, int y, int z)
        {
            return this.GetBlock(new Vector3(x, y, z));
        }

        public Block GetBlock(Vector3 worldPos)
        {
            int chunkX = (int)worldPos.X >> (int)Math.Sqrt(Chunk.Size);
            int chunkY = (int)worldPos.Y >> (int)Math.Sqrt(Chunk.Size);
            int chunkZ = (int)worldPos.Z >> (int)Math.Sqrt(Chunk.Size);

            Vector3 chunkPos = new Vector3(chunkX, chunkY, chunkZ);

            int blockX = (int)Math.Abs(worldPos.X - (chunkX * Chunk.Size));
            int blockY = (int)Math.Abs(worldPos.Y - (chunkY * Chunk.Size));
            int blockZ = (int)Math.Abs(worldPos.Z - (chunkZ * Chunk.Size));

            Vector3 blockPos = new Vector3(blockX, blockY, blockZ);

            if (!this.ChunkExistsAt(chunkPos) || blockX >= Chunk.Size || blockY >= Chunk.Size || blockZ >= Chunk.Size) return new Block(BlockRegistry.Get("air"));

            return this._chunks[chunkPos][blockPos];
        }

        public void SetBlock(int x, int y, int z, BlockDefinition blockDefinition)
        {
            this.SetBlock(new Vector3(x, y, z), blockDefinition);
        }

        public void SetBlock(Vector3 worldPos, BlockDefinition blockDefinition)
        {
            int chunkX = (int)worldPos.X >> (int)Math.Sqrt(Chunk.Size);
            int chunkY = (int)worldPos.Y >> (int)Math.Sqrt(Chunk.Size);
            int chunkZ = (int)worldPos.Z >> (int)Math.Sqrt(Chunk.Size);

            Vector3 chunkPos = new Vector3(chunkX, chunkY, chunkZ);

            int blockX = (int)Math.Abs(worldPos.X - (chunkX * Chunk.Size));
            int blockY = (int)Math.Abs(worldPos.Y - (chunkY * Chunk.Size));
            int blockZ = (int)Math.Abs(worldPos.Z - (chunkZ * Chunk.Size));

            Vector3 blockPos = new Vector3(blockX, blockY, blockZ);

            if (!this.ChunkExistsAt(chunkPos) || blockX >= Chunk.Size || blockY >= Chunk.Size ||
                blockZ >= Chunk.Size) return;

            this._chunks[chunkPos][blockPos] = new Block(blockDefinition);
        }

        public Chunk CreateChunk(Vector3 position)
        {
            var chunk = new Chunk(this, position);
            this._chunks[position] = chunk;

            return chunk;
        }

        public Chunk GetChunk(Vector3 position)
        {
            if (this._chunks.ContainsKey(position))
            {
                return this._chunks[position];
            }

            return null;
        }

        public Chunk GetAdjacentChunk(Vector3 position, Direction direction)
        {
            return this.GetChunk(position.AdjustByDirection(direction));
        }

        public Block GetAdjacentBlock(Vector3 position, Direction direction)
        {
            return this.GetBlock(position.AdjustByDirection(direction));
        }

        public string DumpChunks()
        {
            var s = "";

            foreach (var chunk in this._chunks.Keys)
            {
                s += chunk.ToString() + ", ";
            }

            return s;
        }

        public bool ChunkExistsAt(Vector3 position)
        {
            return this._chunks.ContainsKey(position);
        }

        public bool HasAdjacentChunk(Vector3 position, Direction direction)
        {
            return false;
        }

        public void SetSeed(int seed)
        {
            this.Seed = seed;
            //this.TerrainGenerator.SetSeed(seed);
        }

        public void Update()
        {

        }

    }
}
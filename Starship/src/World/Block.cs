using System;
using System.Collections.Generic;
using System.Text;
using Starship.World.Blocks;

namespace Starship.World
{
    
    public struct Block
    {
        /* public Chunk Chunk { get; }
        public int Id { get; } */

        public BlockDefinition Definition { get; }

        public Block(BlockDefinition definition)
        {
            this.Definition = definition;
        }
    }
}

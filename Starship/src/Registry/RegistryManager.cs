using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starship.World.Blocks;

namespace Starship.Registry
{
    public static class RegistryManager
    {
        public static Registry<BlockDefinition> BLOCKS = new Registry<BlockDefinition>();
        //public static Registry<BlockDefinition> BLOCKS = new Registry<BlockDefinition>();
    }
}

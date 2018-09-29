using System.Collections.Generic;
using Starship.Util;

namespace Starship.World.Blocks
{

    public static class BlockRegistry 
    {
        private static Dictionary<string, BlockDefinition> blocks = new Dictionary<string, BlockDefinition>();

        public static void Register(BlockDefinition blockDefinition)
        {
            blocks.Add(blockDefinition.GetUnlocalizedName(), blockDefinition);
            Logger.GetLogger().DebugFormat("Registered BlockDefinition: {0}", blockDefinition.GetUnlocalizedName());
        }

        public static void Deregister(BlockDefinition blockDefinition)
        {
            blocks.Remove(blockDefinition.GetUnlocalizedName());
        }
        public static void Deregister(string blockName)
        {
            blocks.Remove(blockName);
        }

        public static BlockDefinition Get(string id)
        {
            return blocks[id];
        }

        public static Dictionary<string, BlockDefinition> GetBlocks()
        {
            return blocks;
        }
    }
}

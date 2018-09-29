using Starship.World.Blocks;

namespace DvZ.Blocks
{
    public sealed class BlockDefinitionTest : BlockDefinition
    {
        public BlockDefinitionTest() : base("test", "test")
        {
            this.SetUnlocalizedName("test");
        }
    }
}

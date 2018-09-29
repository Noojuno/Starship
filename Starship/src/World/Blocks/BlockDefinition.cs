using Starship.Registry;

namespace Starship.World.Blocks
{
    public class BlockDefinition : IRegisterable
    {
        private string unlocalizedName;
        private string registryName;

        public BlockDefinition(string regName, string unlocName)
        {
            this.registryName = regName;
            this.unlocalizedName = unlocName;
        }

        public virtual void SetUnlocalizedName(string name)
        {
            this.unlocalizedName = name;
        }

        public virtual string GetUnlocalizedName()
        {
            return this.unlocalizedName;
        }

        public virtual string GetRegistryName()
        {
            return this.registryName;
        }

        public virtual void SetRegistryName(string name)
        {
            this.registryName = name;
        }

        public virtual bool ShouldRender()
        {
            return true;
        }
    }
}

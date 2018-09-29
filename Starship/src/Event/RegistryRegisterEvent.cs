using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redbus.Events;
using Starship.Registry;
using Starship.World.Blocks;

namespace Starship.Event
{
    public class RegistryRegisterEvent<T> : EventBase where T : IRegisterable
    {
        public Registry<T> GetRegistry()
        {
            if (typeof(T) == typeof(BlockDefinition))
            {
                return (Registry<T>)(object)RegistryManager.BLOCKS;
            }

            throw new NullReferenceException("Registry doesn't exist");
        }
    }
}

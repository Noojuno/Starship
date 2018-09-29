using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starship.Util;

namespace Starship.Registry
{
    public class Registry<T> where T : IRegisterable
    {
        private static readonly Dictionary<string, T> registry = new Dictionary<string, T>();

        public void Register(T registerable)
        {
            registry.Add(registerable.GetRegistryName(), registerable);
            Logger.GetLogger().DebugFormat("Registered: {0}", registerable.GetRegistryName());
        }

        public void Deregister(T registerable)
        {
            registry.Remove(registerable.GetRegistryName());
        }

        public Dictionary<string, T> Get()
        {
            return registry;
        }

    }
}

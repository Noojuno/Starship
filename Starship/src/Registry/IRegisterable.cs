using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starship.Registry
{
    public interface IRegisterable
    {
        string GetRegistryName();
        void SetRegistryName(string name);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Starship.Util
{
    public class Logger
    {
        public static ILog GetLogger()
        {
            return LogManager.GetLogger(typeof(Logger));
        }

        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }
    }
}

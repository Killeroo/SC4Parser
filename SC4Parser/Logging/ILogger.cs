using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC4Parser.Logging
{
    public interface ILogger
    {
        void EnableChannel(LogLevel level);

        void Log(LogLevel level, string format, params object[] args);
    }
}

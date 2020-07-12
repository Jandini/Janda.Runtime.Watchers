using CommandLine;
using Microsoft.Extensions.Logging;

namespace Janda.Runtime.Watchers
{
    internal class Options : IApplicationOptions
    {
        [Option("logdir", Hidden = true, Required = false)]
        public string LogDir { get; set; }

        [Option("loglevel", Hidden = true, Default = LogLevel.Information)]
        public LogLevel LogLevel { get; set; }
    }
}

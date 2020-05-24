using CommandLine;

namespace Janda.Runtime.Application
{
    internal class Options : IApplicationOptions
    {
        [Option("logdir", Required = false, HelpText = "Set log directory and enable logging to file")]
        public string LogDir { get; set; }
    }
}

using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Janda.Runtime.Watchers
{
    public class MethodWatcherService : IMethodWatcherService
    {
        private readonly ILogger<MethodWatcherService> _logger;

        internal static Dictionary<string, MethodWatcher> _watchers = new Dictionary<string, MethodWatcher>();

        internal static MethodWatcher CreateWatcher(MethodBase method, object[] args)
        {
            var key = method.ToString();

            if (!_watchers.TryGetValue(key, out var watcher))
            {
                watcher = new MethodWatcher(method);
                _watchers.Add(key, watcher);
            }

            watcher.Call(args);
            return watcher;
        }


        public MethodWatcherService(ILogger<MethodWatcherService> logger)
        {
            _logger = logger;
        }

        public void Verbose()
        {
            WatchMethodAttribute._logger = _logger;
        }

        public void Report()
        {
            _logger.LogInformation("Method count: {@count}", _watchers.Keys.Count);
            _logger.LogInformation("Method names: {@methods}", _watchers.Keys);

            foreach (var watcher in _watchers.Values)
            {
                List<dynamic> arguments = new List<dynamic>(); 

                foreach (var parameter in watcher.ArgumentNames)
                {
                    arguments.Add(
                        new
                        {
                            Name = parameter,
                            Count = watcher.Values.Where(a=>a.Key.Name == parameter).Count()
                        });
                };

                _logger.LogInformation("{@method} {@arguments}", new object[] {
                    new
                    {
                        Name = watcher.MethodName,
                        Calls = watcher.Calls.Count,
                        Values = watcher.Values.Count,
                    },
                    arguments.ToArray()
                });;
            }
        }
    }
}

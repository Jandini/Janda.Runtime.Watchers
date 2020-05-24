using Janda.Runtime.Watchers;
using Microsoft.Extensions.Logging;

namespace Janda.Runtime.Application
{
    public class Service : IApplicationService
    {
        private readonly ILogger<Service> _logger;
        private readonly IApplicationOptions _options;
        private readonly IApplicationSettings _settings;
        private readonly IMethodWatcherService _methodWatcher;

        public Service(ILogger<Service> logger, IApplicationOptions options, IApplicationSettings settings, IMethodWatcherService methodWatcher)
        {
            _logger = logger;
            _options = options;
            _settings = settings;
            _methodWatcher = methodWatcher;
        }
        
        [WatchMethod]
        public int Run()
        {
            _logger.LogInformation("Running {0} {1} {2}", Application.Name, Application.Version, _settings.Description);

            _methodWatcher.Verbose();

            Parse(new byte[512], "matt", 4);
            Parse(new byte[512], "matt", 4);
            Parse(new byte[51], "matt", 4);

            _logger.LogInformation("Running application");
            _methodWatcher.Report();

            return 128;
        }


        [WatchMethod]
        public static int Parse(byte[] buffer, string param1, int param2)
        {
            var abc = "abc";

            var logger = Application.GetLogger<Service>();

            logger.LogInformation("abc GetHashCode: {HashCode}", abc.GetHashCode());

            abc = "a";
            logger.LogInformation("abc GetHashCode: {HashCode}", abc.GetHashCode());
            abc = "abc";
            logger.LogInformation("abc GetHashCode: {HashCode}", abc.GetHashCode());


            return param1?.Length ?? -10 + param2;
        }

      

    }
}

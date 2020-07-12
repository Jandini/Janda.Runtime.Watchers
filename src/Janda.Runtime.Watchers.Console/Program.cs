using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using CommandLine;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Janda.Runtime.Watchers;

// This is required to WatchMethod work
[module: WatchMethod]

namespace Janda.Runtime.Watchers
{

    class Program : IProgram
    {
        static int Main(string[] args)
        {
            return Application.Run<Program, Options>(options => Parser.Default.ParseArguments<Options>(args).WithParsed(options));
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddMethodWatcher()
                .AddSingleton<IApplicationService, ApplicationService>();
        }

        public IConfiguration CreateConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .Build();
        }

        public void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(Application.Configuration)
                .WriteTo.Console(theme: AnsiConsoleTheme.Code, outputTemplate: "{Message:lj}{NewLine}{Exception}");

            var applicationOptions = Application.Options as Options;

            if (!string.IsNullOrEmpty(applicationOptions?.LogDir))
                loggerConfiguration.WriteTo.File(
                    path: Path.Combine(applicationOptions.LogDir, Path.ChangeExtension(Application.Name, "log")),
                    rollingInterval: RollingInterval.Day);

            loggingBuilder.AddSerilog(
                loggerConfiguration.CreateLogger(),
                dispose: true);
        }
    }
}
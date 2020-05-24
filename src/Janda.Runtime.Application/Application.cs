using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Janda.Runtime.Application
{
    internal class Application
    {
        public static IServiceProvider Services { get; private set; }
        public static IApplicationOptions Options { get; private set; }
        public static IConfiguration Configuration { get; private set; }
        public static string Version { get; private set; }
        public static string Name { get; private set; }


        static Application()
        {
            Name = AppDomain.CurrentDomain.FriendlyName;
            Version = Assembly.GetEntryAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion;
        }

        public static int Run<TProgram, TOptions>(Action<Action<TOptions>> parseArgs)
            where TProgram : IApplicationProgram, new()
            where TOptions : IApplicationOptions
        {
            var applicationProgram = new TProgram();
            int returnCode = 0;

            parseArgs(options =>
            {
                Options = options;

                var serviceCollection = new ServiceCollection();
                var settings = new Settings();

                try
                {
                    Configuration = applicationProgram.CreateConfiguration();
                    Configuration.Bind(Name, settings);

                    serviceCollection
                        .AddLogging(configure => applicationProgram.ConfigureLogging(configure))
                        .AddSingleton<IApplicationOptions>(options)
                        .AddSingleton<IApplicationSettings>(settings);

                    applicationProgram.ConfigureServices(serviceCollection);

                    returnCode = (Services = serviceCollection.BuildServiceProvider())
                        .GetService<IApplicationService>()
                        .Run();
                }
                catch (Exception ex)
                {
                    GetService<ILogger<Application>>()?.LogCritical(ex, ex.Message);
                    throw;
                }
            });

            return returnCode;
        }

        public static ILogger<T> GetLogger<T>()
        {
            return GetService<ILogger<T>>();
        }

        public static T GetService<T>()
        {
            return Services != null
                ? Services.GetService<T>()
                : default;
        }
    }
}

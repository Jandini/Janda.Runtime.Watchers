using Microsoft.Extensions.DependencyInjection;

namespace Janda.Runtime.Watchers
{
    public static class RuntimeExtensions
    {
        public static IServiceCollection AddMethodWatcher(this IServiceCollection services)
        {
            return services.AddSingleton<IMethodWatcherService, MethodWatcherService>();
        }  
    }
}

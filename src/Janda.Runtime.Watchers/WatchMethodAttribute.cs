using MethodDecorator.Fody.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;


namespace Janda.Runtime.Watchers
{
    /// <summary>
    /// Any attribute which provides OnEntry/OnExit/OnException with proper args
    /// This must be added to the beginning of [module: WatchMethod] 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Assembly | AttributeTargets.Module)]
    public class WatchMethodAttribute : Attribute, IMethodDecorator, IPartialDecoratorExit1
    {
        private MethodBase _method;
        private MethodWatcher _watcher;

        internal static ILogger _logger;

        public void Init(object instance, MethodBase method, object[] args)
        {
            _method = method;
            _watcher = MethodWatcherService.CreateWatcher(method, args);
            _logger?.LogTrace("Method {@method} called with {@args}", _method.Name, args);
        }

        public void OnEntry()
        {

        }

        public void OnExit()
        {

        }

        public void OnException(Exception exception)
        {
            _watcher.Return(exception);
            _logger?.LogTrace("Method {@method} thrown {@exception}", _method.Name, exception);
        }

        public void OnExit(object value)
        {
            _watcher.Return(value);
            _logger?.LogTrace("Method {@method} returned {@value}", _method.Name, value);
        }
    }
}

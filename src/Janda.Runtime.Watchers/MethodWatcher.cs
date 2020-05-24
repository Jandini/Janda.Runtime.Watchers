using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Janda.Runtime.Watchers
{
    internal class MethodWatcher
    {
        private Dictionary<MethodParameter, object> values = new Dictionary<MethodParameter, object>();
        private Dictionary<MethodParameters, object> calls = new Dictionary<MethodParameters, object>();

        public string MethodName { get; private set; }
        public string[] ArgumentNames { get; private set; }
        public Dictionary<MethodParameter, object> Values { get => values; set => values = value; }
        public Dictionary<MethodParameters, object> Calls { get => calls; set => calls = value; }

        private MethodParameters parameters = null;


        public MethodWatcher(MethodBase method)
        {
            var parameters = method.GetParameters();
            ArgumentNames = parameters.Select(a => a.Name).ToArray();
            MethodName = method.Name;
        }


        public void Call(object[] arguments)
        {
            parameters = new MethodParameters();

            for (int i = 0, c = ArgumentNames.Length; i < c; i++)
            {
                var value = arguments[i];
                var name = ArgumentNames[i];

                var parameter = MethodParameter.Create(name, value);
                values.TryAdd(parameter, value);

                parameters.Add(parameter);
            }
        }

        public T Return<T>(T o)
        {
            if (parameters != null)
            {
                //var call = new MethodCall()
                //{
                //    Parameters = parameters,
                //    ReturnValue = o
                //};

                if (!calls.TryAdd(parameters, o))
                {
                    T ret = (T)calls[parameters];

                    if (!ret.Equals(o))
                        throw new InvalidOperationException("The same call returned diffrent value");
                }
            }

            return o;
        }
    }
}

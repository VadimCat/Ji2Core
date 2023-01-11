using System;
using System.Collections.Generic;

namespace Ji2Core.Core
{
    public class Context
    {
        private readonly Dictionary<Type, object> services = new();

        public void Register<TContract>(TContract service)
        {
            if (services.ContainsKey(typeof(TContract)))
            {
                throw new Exception("Service already added by this type");
            }
            else
            {
                services[typeof(TContract)] = service;
            }
        }

        public TContract GetService<TContract>()
        {
            return (TContract)services[typeof(TContract)];
        }

        public void Remove<TContract>()
        {
            if (!services.ContainsKey(typeof(TContract)))
            {
                throw new Exception("Service already unregistered by this type");
            }
            else
            {
                services.Remove(typeof(TContract));
            }
        }
    }
}
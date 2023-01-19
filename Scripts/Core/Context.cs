using System;
using System.Collections.Generic;

namespace Ji2Core.Core
{
    public class Context
    {
        private static Context instance;
        private readonly Dictionary<Type, object> services = new();
        
        private Context()
        {
            instance = this;
        }

        public static Context GetInstance()
        {
            if (instance != null)
                return instance;

            return new Context();
        }

        public void Register<TContract>(TContract service) where TContract : class
        {
            if (services.ContainsKey(typeof(TContract)))
            {
                throw new Exception("Service already added by this type");
            }
            else if(!(service is TContract))
            {
                throw new Exception("Service type doesn't match contract type");
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

        public void Unregister<TContract>()
        {
            Unregister(typeof(TContract));
        }

        public void Unregister(object obj)
        {
            var contract = obj.GetType();
            
            Unregister(contract);
        }

        private void Unregister(Type contract)
        {
            if (!services.ContainsKey(contract))
            {
                throw new Exception("Service already unregistered by this type");
            }
            else
            {
                services.Remove(contract);
            }
        }
    }
}
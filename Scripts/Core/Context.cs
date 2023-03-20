using System;
using System.Collections.Generic;
using System.Linq;
using Client;
using Ji2.CommonCore.SaveDataContainer;
using Ji2Core.Core.ScreenNavigation;

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

        public void Register<TContract>(TContract service)
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
            if (!services.ContainsKey(typeof(TContract)))
                throw new Exception($"No service register by type {typeof(TContract)}");
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
        
        public LevelsLoopProgress LevelsLoopProgress => GetService<LevelsLoopProgress>();
        public ISaveDataContainer SaveDataContainer => GetService<ISaveDataContainer>();
        public ScreenNavigator ScreenNavigator => GetService<ScreenNavigator>();
    }
}
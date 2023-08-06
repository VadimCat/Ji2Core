using System;
using System.Collections.Generic;
using Client;
using Ji2.CommonCore.SaveDataContainer;

namespace Ji2.Context
{
    public class Context : IDependenciesProvider, IDependenciesController
    {
        private static Context _instance;
        private readonly Dictionary<Type, object> _services = new();

        private Context()
        {
            _instance = this;
        }

        public static Context GetOrCreateInstance()
        {
            if (_instance != null)
                return _instance;

            return new Context();
        }

        public void Register<TContract>(TContract service)
        {
            Register(typeof(TContract), service);
        }

        public void Register(Type type, object service)
        {
            if (_services.ContainsKey(type))
            {
                throw new Exception("Service already added by this type");
            }

            if (type.IsInstanceOfType(service) || service.GetType() == type)
            {
                _services[type] = service;
            }
            else
            {
                throw new Exception("Service type doesn't match contract type");
            }
        }

        public TContract GetService<TContract>()
        {
            if (!_services.ContainsKey(typeof(TContract)))
                throw new Exception($"No service register by type {typeof(TContract)}");
            return (TContract)_services[typeof(TContract)];
        }

        public void Unregister<TContract>()
        {
            Unregister(typeof(TContract));
        }

        public void Unregister(Type type)
        {
            if (!_services.ContainsKey(type))
            {
                throw new Exception("Service already unregistered by this type");
            }

            _services.Remove(type);
        }

        public LevelsLoopProgress LevelsLoopProgress => GetService<LevelsLoopProgress>();
        public ISaveDataContainer SaveDataContainer => GetService<ISaveDataContainer>();
    }

    public interface IDependenciesController
    {
        public void Register<TContract>(TContract service);
        public void Register(Type type, object service);
        public void Unregister(Type type);
        public void Unregister<TContract>();
    }

    public interface IDependenciesProvider
    {
        public TContract GetService<TContract>();
    }
}
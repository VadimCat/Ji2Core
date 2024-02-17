using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Ji2.Context
{
    public abstract class SceneInstaller : SerializedMonoBehaviour
    {
        private readonly IDependenciesController _dependenciesController = DiContext.GetOrCreateInstance();

        private IEnumerable<(Type type, object obj)> _dependencies; 
            
        protected abstract IEnumerable<(Type type, object obj)> GetDependencies();
            
        private void Awake()
        {
            _dependencies = GetDependencies();
            foreach (var dependency in _dependencies)
            {
                _dependenciesController.Register(dependency.type, dependency.obj);
            }
        }

        private void OnDestroy()
        {
            foreach (var dependency in _dependencies)
            {
                _dependenciesController.Unregister(dependency.type);
            }
        }
    }
}
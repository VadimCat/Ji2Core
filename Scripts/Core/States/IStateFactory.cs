using System;
using System.Collections.Generic;

namespace Ji2Core.Core.States
{
    public interface IStateFactory
    {
        public Dictionary<Type, IExitableState> GetStates(StateMachine stateMachine);
    }
}
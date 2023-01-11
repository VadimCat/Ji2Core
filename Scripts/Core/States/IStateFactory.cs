using System;
using System.Collections.Generic;

namespace Ji2Core.Core.States
{
    public interface IStateFactory
    {
        public Dictionary<Type, IState> GetStates(StateMachine stateMachine);
    }
}
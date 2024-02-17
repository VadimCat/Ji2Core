using System;
using System.Collections.Generic;

namespace Ji2.States
{
    public interface IStateFactory
    {
        public Dictionary<Type, IExitableState> GetStates(StateMachine stateMachine);
    }
}
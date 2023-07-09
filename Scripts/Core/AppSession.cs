using Ji2Core.Core.States;

namespace Ji2Core.Core
{
    public class AppSession
    {
        public StateMachine StateMachine { get; }

        public AppSession(StateMachine stateStateMachine)
        {
            StateMachine = stateStateMachine;
        }
    }
}
using Ji2.States;

namespace Ji2
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
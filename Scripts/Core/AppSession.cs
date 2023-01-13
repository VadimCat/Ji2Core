using Ji2Core.Core;
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

    public interface ISaveDataContainer
    {
        public void Load();
        public void Save();
        public void SaveValue(string key, object value);
    }
}
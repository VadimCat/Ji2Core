using System.Collections.Generic;
using System.Linq;
using Ji2.CommonCore.SaveDataContainer;

namespace Ji2.Ji2Core.Scripts.CommonCore
{
    public class LocalLeaderboard
    {
        private readonly ISaveDataContainer _saveDataContainer;
        private const string SAVE_KEY = "Leaderbord";
        private List<(string, int)> _records;

        public IReadOnlyList<(string, int)> Records => _records.AsReadOnly();

        public LocalLeaderboard(ISaveDataContainer saveDataContainer)
        {
            _saveDataContainer = saveDataContainer;
        }
        
        public void Load()
        {
            _records = _saveDataContainer.GetValue(SAVE_KEY, new List<(string, int)>());
        }
        
        public void AddRecord(string nick, int score)
        {
            _records.Add(new(nick, score));
            _records = _records.OrderByDescending(val => val.Item2).ToList();
            _saveDataContainer.SaveValue(SAVE_KEY, _records);
            while (_records.Count > 5)
            {
                _records.RemoveAt(5);
            }
        }
    }
}
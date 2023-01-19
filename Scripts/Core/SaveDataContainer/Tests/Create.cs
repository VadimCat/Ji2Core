using Ji2Core.Core.SaveDataContainer;

namespace Ji2Core.SaveDataContainer.Tests
{
    public static class Create
    {
        public static ISaveDataContainer PlayerPrefsSaveDataContainer()
        {
            PlayerPrefsSaveDataContainer saveDataContainer = new PlayerPrefsSaveDataContainer();
            return saveDataContainer;
        }
    }
}
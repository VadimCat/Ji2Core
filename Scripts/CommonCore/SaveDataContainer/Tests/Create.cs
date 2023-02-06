using Ji2.CommonCore.SaveDataContainer;

namespace Ji2.SaveDataContainer.Tests
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
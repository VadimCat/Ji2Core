using NUnit.Framework;

namespace Ji2.SaveDataContainer.Tests
{
    public class PlayerPrefsSaveDataContainerTests
    {
        [Test]
        public async void WhenSaveValues_AndLoadThem_ThenInputShouldBeEqualsToOutput()
        {
            // Arrange.
            string intKey = "intKey";
            var stringKey = "stringKey";
            var floatKey = "floatKey";

            int intSave = 777;
            var stringSave = "qwe";
            var floatSave = .25f;

            var saveDataContainer = Create.PlayerPrefsSaveDataContainer();

            // Act.
            saveDataContainer.Load();
            saveDataContainer.SaveValue(intKey, intSave);
            saveDataContainer.SaveValue(stringKey, stringSave);
            saveDataContainer.SaveValue(floatKey, floatSave);
            saveDataContainer.Save();
            saveDataContainer.Load();

            // Assert.
            Assert.AreEqual(intSave, saveDataContainer.GetValue<int>(intKey));
            Assert.AreEqual(stringSave, saveDataContainer.GetValue<string>(stringKey));
            Assert.AreEqual(floatSave, saveDataContainer.GetValue<float>(floatKey));
        }

        [Test]
        public void WhenNoDataWasSaved_AndGetValue_ThenNoErrorShouldThrow()
        {
            // Arrange.
            var saveDataContainer = Create.PlayerPrefsSaveDataContainer();

            // Act.
            saveDataContainer.Load();

            // Assert.
            Assert.AreEqual(0, saveDataContainer.GetValue<int>("randomKey"));
            Assert.DoesNotThrow(() => saveDataContainer.GetValue<string>("randomKey"));
        }

        [Test]
        public static void WhenNoDataSaved_AndGetValueWithDefaultParameter_ThenDefaultValueShouldBeReturned()
        {
            // Arrange.
            string defaultValue = "defaultValue";
            var saveDataContainer = Create.PlayerPrefsSaveDataContainer();
            saveDataContainer.Load();
            
            // Act.
            var outValue = saveDataContainer.GetValue("randomKey", defaultValue);
            
            // Assert.
            saveDataContainer.Load();
            Assert.AreEqual(defaultValue, outValue);
        }
    }
}

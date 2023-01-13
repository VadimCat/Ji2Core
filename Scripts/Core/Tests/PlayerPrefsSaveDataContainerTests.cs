using Ji2Core.Core;
using NUnit.Framework;

public class PlayerPrefsSaveDataContainerTests
{
    [Test]
    public void WhenSaveValues_AndLoadThem_ThenInputShoudBeEqualsToOutput()
    {
        // Arrange.
        string intKey = "intKey";
        var stringKey = "stringKey";
        var floatKey = "floatKey";
        
        int intSave = 777;
        var stringSave = "qwe";
        var floatSave = .25f;

        PlayerPrefsSaveDataContainer saveDataContainer = new PlayerPrefsSaveDataContainer();

        // Act.
        saveDataContainer.Load();
        saveDataContainer.SaveValue(intKey, intSave);
        saveDataContainer.SaveValue(stringKey, stringSave);
        saveDataContainer.SaveValue(floatKey, floatSave);
        
        // Assert.
        Assert.AreEqual(intSave, saveDataContainer.LoadValue<int>(intKey));
        Assert.AreEqual(stringSave, saveDataContainer.LoadValue<string>(stringKey));
        Assert.AreEqual(floatSave, saveDataContainer.LoadValue<float>(floatKey));

    }
}

using System.Collections.Generic;
using Client;
using Ji2.CommonCore.SaveDataContainer;
using NUnit.Framework;
using UnityEditor;

namespace Models.Progress.Tests
{
    public class Tests
    {
        [Test]
        public void WhenNoProgress_AndLoadLevel_ThenFirstLevelShouldBeLoaded()
        {
            // Arrange.
            var levelsConfig = LevelsConfig();
            var loopProgress = LoopProgress(levelsConfig);
            loopProgress.Reset();
            
            // Act.
            string levelId = loopProgress.GetNextLevelData().name;

            // Assert.
            Assert.AreEqual(levelsConfig.GetLevelsOrder()[0], levelId);
        }

        [Test]
        public void WhenIncLevel_AndRetry_ThenSameDataShouldBeReturned()
        {
            // Arrange.
            var levelsConfig = LevelsConfig();
            var loopProgress = LoopProgress(levelsConfig);

            // Act.
            string levelId = loopProgress.GetNextLevelData().name;
            loopProgress.IncLevel();
            string retryLevelId = loopProgress.GetRetryLevelData().name;

            // Assert.
            Assert.AreEqual(levelId, retryLevelId);
        }

        [Test]
        public void WhenIncLevel_AndIncAgain_ThenSecondLevelShouldBeReturned()
        {
            // Arrange.
            var levelsConfig = LevelsConfig();
            var loopProgress = LoopProgress(levelsConfig);
            loopProgress.Reset();

            // Act.
            loopProgress.IncLevel();
            string levelId = loopProgress.GetNextLevelData().name;

            // Assert.
            Assert.AreEqual(levelsConfig.GetLevelsOrder()[1], levelId);
        }

        [Test]
        public void WhenMainLoopDone_AndReloadApp_ThenSameRandomLevelsShouldBeLoaded()
        {
            // Arrange.
            var levelsConfig = LevelsConfig();
            var loopProgress = LoopProgress(levelsConfig);
            loopProgress.Reset();
            
            // Act.
            for (int i = 0; i < levelsConfig.GetLevelsOrder().Length; i++)
            {
                loopProgress.IncLevel();
            }

            var directOrderLevels = new List<string>();

            for (int i = 0; i < 5; i++)
            {
                var data = loopProgress.GetNextLevelData();
                loopProgress.IncLevel();

                directOrderLevels.Add(data.name);
            }

            var saveLoadOrderLevels = new List<string>();
            loopProgress = LoopProgress(levelsConfig);
            loopProgress.Reset();

            for (int i = 0; i < levelsConfig.GetLevelsOrder().Length; i++)
            {
                loopProgress.IncLevel();
            }
            
            for (int i = 0; i < 3; i++)
            {
                var data = loopProgress.GetNextLevelData();
                loopProgress.IncLevel();

                saveLoadOrderLevels.Add(data.name);
            }
            
            loopProgress = LoopProgress(levelsConfig);
            
            for (int i = 0; i < 2; i++)
            {
                var data = loopProgress.GetNextLevelData();
                loopProgress.IncLevel();

                saveLoadOrderLevels.Add(data.name);
            }

            // Assert.
            Assert.AreEqual(directOrderLevels[0], saveLoadOrderLevels[0]);
            Assert.AreEqual(directOrderLevels[1], saveLoadOrderLevels[1]);
            Assert.AreEqual(directOrderLevels[2], saveLoadOrderLevels[2]);
            Assert.AreEqual(directOrderLevels[3], saveLoadOrderLevels[3]);
            Assert.AreEqual(directOrderLevels[4], saveLoadOrderLevels[4]);
        }

        private static LevelsLoopProgress LoopProgress(LevelsConfig levelsConfig)
        {
            ISaveDataContainer saveDataContainer = new PlayerPrefsSaveDataContainer();
            saveDataContainer.Load();
            var loop = new LevelsLoopProgress(saveDataContainer, levelsConfig.GetLevelsOrder());
            loop.Load();
            return loop;
        }

        private static LevelsConfig LevelsConfig()
        {
            var levelDataStorage = AssetDatabase.FindAssets("t:LevelsConfig")[0];
            var levelsConfig =
                AssetDatabase.LoadAssetAtPath<LevelsConfig>(AssetDatabase.GUIDToAssetPath(levelDataStorage));
            return levelsConfig;
        }
    }
}
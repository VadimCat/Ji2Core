using System.Collections.Generic;
using Client;
using Ji2.CommonCore.SaveDataContainer;
using NUnit.Framework;

namespace Ji2.Models.Progress.Tests
{
    public class Tests
    {
        [Test]
        public void WhenNoProgress_AndLoadLevel_ThenFirstLevelShouldBeLoaded()
        {
            // Arrange.
            var levelOrder = LevelOrder();
            var loopProgress = LoopProgress();
            loopProgress.Reset();

            // Act.
            string levelId = loopProgress.GetNextLevelData().Name;

            // Assert.
            Assert.AreEqual(levelOrder[0], levelId);
        }

        [Test]
        public void WhenIncLevel_AndRetry_ThenSameDataShouldBeReturned()
        {
            // Arrange.
            var levelOrder = LevelOrder();
            var loopProgress = LoopProgress();

            // Act.
            string levelId = loopProgress.GetNextLevelData().Name;
            loopProgress.IncLevel();
            string retryLevelId = loopProgress.GetRetryLevelData().Name;

            // Assert.
            Assert.AreEqual(levelId, retryLevelId);
        }

        [Test]
        public void WhenIncLevel_AndIncAgain_ThenSecondLevelShouldBeReturned()
        {
            // Arrange.
            var levelOrder = LevelOrder();
            var loopProgress = LoopProgress();
            loopProgress.Reset();

            // Act.
            loopProgress.IncLevel();
            string levelId = loopProgress.GetNextLevelData().Name;

            // Assert.
            Assert.AreEqual(levelOrder[1], levelId);
        }

        [Test]
        public void WhenMainLoopDone_AndReloadApp_ThenSameRandomLevelsShouldBeLoaded()
        {
            // Arrange.
            var levelOrder = LevelOrder();
            var loopProgress = LoopProgress();
            loopProgress.Reset();

            // Act.
            for (int i = 0; i < levelOrder.Length; i++)
            {
                loopProgress.IncLevel();
            }

            var directOrderLevels = new List<string>();

            for (int i = 0; i < 5; i++)
            {
                var data = loopProgress.GetNextLevelData();
                loopProgress.IncLevel();

                directOrderLevels.Add(data.Name);
            }

            var saveLoadOrderLevels = new List<string>();
            loopProgress = LoopProgress();
            loopProgress.Reset();

            for (int i = 0; i < levelOrder.Length; i++)
            {
                loopProgress.IncLevel();
            }

            for (int i = 0; i < 3; i++)
            {
                var data = loopProgress.GetNextLevelData();
                loopProgress.IncLevel();

                saveLoadOrderLevels.Add(data.Name);
            }

            loopProgress = LoopProgress();

            for (int i = 0; i < 2; i++)
            {
                var data = loopProgress.GetNextLevelData();
                loopProgress.IncLevel();

                saveLoadOrderLevels.Add(data.Name);
            }

            // Assert.
            Assert.AreEqual(directOrderLevels[0], saveLoadOrderLevels[0]);
            Assert.AreEqual(directOrderLevels[1], saveLoadOrderLevels[1]);
            Assert.AreEqual(directOrderLevels[2], saveLoadOrderLevels[2]);
            Assert.AreEqual(directOrderLevels[3], saveLoadOrderLevels[3]);
            Assert.AreEqual(directOrderLevels[4], saveLoadOrderLevels[4]);
        }

        private static LevelsLoopProgress LoopProgress()
        {
            ISaveDataContainer saveDataContainer = new PlayerPrefsSaveDataContainer();
            saveDataContainer.Load();
            var order = LevelOrder();
            var loop = new LevelsLoopProgress(saveDataContainer, order);
            loop.Load();
            return loop;
        }

        private static string[] LevelOrder()
        {
            return new[]
            {
                "a", "b", "c", "d", "e", "f", "g", "o", "p", "r", "s", "t"
            };
        }
    }
}
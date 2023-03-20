using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Ji2.Utils.Shuffling.Tests
{
    public class ShuffleTests
    {
        [Test]
        public void WhenCreateShuffledArray_AndGetAllElements_ThenAllElementsShouldBeEqualAndIncluded()
        {
            // Arrange.
            int count = 9;
            var shuffled = Shufflling.CreateShuffledArray(count);

            // Act.
            int[] entryCounts = new int[shuffled.Length];
            foreach (var entry in shuffled)
            {
                entryCounts[entry]++;
            }

            // Assert.
            for (int i = 0; i < 9; i++)
            {
                var elem = i;
                int entryCount = shuffled.Count(el => el == elem);
                Assert.AreEqual(1, entryCount);
            }
        }

        [Test]
        public void WhenCreate2x2ShuffledVector2IntArray_AndGetAllElements_ThenAllElementsShouldBeEqualAndIncluded()
        {
            // Arrange.
            Vector2Int size = new Vector2Int(2, 2);
            var shuffledArray = Shufflling.CreatedShuffled2DimensionalArray(size);
            
            // Act.
            Dictionary<Vector2Int, int> expectedEntries = new Dictionary<Vector2Int, int>()
            {
                [new Vector2Int(0, 0)] = 0,
                [new Vector2Int(0, 1)] = 0,
                [new Vector2Int(1, 0)] = 0,
                [new Vector2Int(1, 1)] = 0,
            };

            foreach (var el in shuffledArray)
            {
                expectedEntries[el]++;
            }
            
            // Assert.
            Assert.AreEqual(size.x * size.y, expectedEntries.Keys.Count);
            foreach (var entry in expectedEntries)
            {
                Assert.AreEqual(1, entry.Value, $"Fail on {entry.Key}");
            }
        }

        [Test]
        public void WhenCreate3x4ShuffledVector2IntArray_AndGetAllElements_ThenAllElementsShouldBeEqualAndIncluded()
        {
            // Arrange.
            Vector2Int size = new Vector2Int(3, 4);
            var shuffledArray = Shufflling.CreatedShuffled2DimensionalArray(size);
            
            // Act.
            Dictionary<Vector2Int, int> expectedEntries = new Dictionary<Vector2Int, int>()
            {
                [new Vector2Int(0, 0)] = 0,
                [new Vector2Int(0, 1)] = 0,
                [new Vector2Int(0, 2)] = 0,
                [new Vector2Int(0, 3)] = 0,
                [new Vector2Int(1, 0)] = 0,
                [new Vector2Int(1, 1)] = 0,
                [new Vector2Int(1, 2)] = 0,
                [new Vector2Int(1, 3)] = 0,
                [new Vector2Int(2, 0)] = 0,
                [new Vector2Int(2, 1)] = 0,
                [new Vector2Int(2, 2)] = 0,
                [new Vector2Int(2, 3)] = 0,
            };

            foreach (var el in shuffledArray)
            {
                expectedEntries[el]++;
            }
            
            // Assert.
            Assert.AreEqual(size.x * size.y, expectedEntries.Keys.Count);
            foreach (var entry in expectedEntries)
            {
                Assert.AreEqual(1, entry.Value, $"Fail on {entry.Key}");
            }
        }

        [Test]
        public void WhenConvert0ToVector2Int_AndGetValue_ThenShouldReturnVector2IntZero()
        {
            // Arrange.
            int input = 0;
            Vector2Int expected = Vector2Int.zero;
            Vector2Int size = new Vector2Int(4, 3);

            // Act.
            var output = Shufflling.GetTwoDimensionalVector2Int(input, size);
            // Assert.
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void WhenConvert5In4x3ToVector2Int_AndGetValue_ThenShouldReturnVector1x1()
        {
            // Arrange.
            int input = 5;
            Vector2Int expected = new Vector2Int(1, 1);
            Vector2Int size = new Vector2Int(4, 3);

            // Act.
            var output = Shufflling.GetTwoDimensionalVector2Int(input, size);
            // Assert.
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void WhenConvert9In4x3ToVector2Int_AndGetValue_ThenShouldReturnVector1x1()
        {
            // Arrange.
            int input = 9;
            Vector2Int expected = new Vector2Int(1, 2);
            Vector2Int size = new Vector2Int(4, 3);

            // Act.
            var output = Shufflling.GetTwoDimensionalVector2Int(input, size);
            // Assert.
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void WhenConvert11In3x4ToVector2Int_AndGetValue_ThenShouldReturnVector2x3()
        {
            // Arrange.
            int input = 11;
            Vector2Int expected = new Vector2Int(2, 3);
            Vector2Int size = new Vector2Int(3, 4);

            // Act.
            var output = Shufflling.GetTwoDimensionalVector2Int(input, size);
            // Assert.
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void WhenConvert11In4x4ToVector2Int_AndGetValue_ThenShouldReturnVector2x3()
        {
            // Arrange.
            int input = 11;
            Vector2Int expected = new Vector2Int(3, 2);
            Vector2Int size = new Vector2Int(4, 4);

            // Act.
            var output = Shufflling.GetTwoDimensionalVector2Int(input, size);
            // Assert.
            Assert.AreEqual(expected, output);
        }
    }
}
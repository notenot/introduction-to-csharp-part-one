using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        /// <remarks>
        /// Эта функция уже реализована, она заработает, 
        /// как только вы выполните задачу в файле LeftBorderTask
        /// </remarks>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var start = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            var result = new List<string>();

            for (var i = start; i < start + count; ++i)
            {
                if (i == phrases.Count ||
                    !phrases[i].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    break;

                result.Add(phrases[i]);
            }

            return result.ToArray();
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var count = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count) -
                        LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) - 1;
            return Math.Max(0, count);
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        public void TestTopByPrefix(
            IReadOnlyList<string> phrases, string prefix, int count, string[] expectedTopWords)
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            CollectionAssert.AreEqual(actualTopWords, expectedTopWords);
        }
        
        public void TestCountByPrefix(
            IReadOnlyList<string> phrases, string prefix, int expectedCount)
        {
            var actualCount = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(actualCount, expectedCount);
        }

        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            TestTopByPrefix(new List<string>(), "a", 10, new string[] { });
            TestTopByPrefix(new List<string>(), "", 10, new string[] { });
            TestTopByPrefix(new List<string>(), "a", 0, new string[] { });
        }

        [Test]
        public void TopByPrefix_HasActualCountLength_WhenActualCountLessThanRequestedCount()
        {
            TestTopByPrefix(new List<string> { "aa", "ba", "bb", "bc" }, 
                "b", 10, new [] { "ba", "bb", "bc" });
            TestTopByPrefix(new List<string> { "aa" },
                "a", 100, new[] { "aa" });
        }

        [Test]
        public void TopByPrefix_IsEmpty_WhenNoWordsWithPrefix()
        {
            TestTopByPrefix(new List<string> { "aa", "ba", "bb", "bc" },
                "c", 10, new string[] { });
            TestTopByPrefix(new List<string> { "aa", "ba", "bb", "bc" },
                "aaa", 100, new string[] { });
        }

        [Test]
        public void TopByPrefix_IsCountFirstPhrases_WhenEmptyPrefix()
        {
            TestTopByPrefix(new List<string> { "aa", "ba", "bb", "bc" },
                "", 10, new [] { "aa", "ba", "bb", "bc" });
            TestTopByPrefix(new List<string> { "aa", "ba", "bb", "bc" },
                "", 2, new [] { "aa", "ba" });
        }

        [Test]
        public void TopByPrefix_IsEmpty_WhenCountIsZero()
        {
            TestTopByPrefix(new List<string> { "aa", "ba", "bb", "bc" },
                "b", 0, new string[] { });
            TestTopByPrefix(new List<string> { "aaa" },
                "aaa", 0, new string[] { });
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            TestCountByPrefix(new List<string> { "aa" },
                "", 1);
            TestCountByPrefix(new List<string> { "aa", "ba", "bb", "bc" }, 
                "", 4);
            TestCountByPrefix(new List<string> { "aa", "ba", "bb", "bc", "aa", "ba", "bb", "bc" },
                "", 8);
        }

        [Test]
        public void CountByPrefix_IsZero_WhenNoPhrases()
        {
            TestCountByPrefix(new List<string>(), "a", 0);
            TestCountByPrefix(new List<string>(), "", 0);
        }

        [Test]
        public void CountByPrefix_IsZero_WhenNoWordsWithPrefix()
        {
            TestCountByPrefix(new List<string> { "aa", "ba", "bb", "bc" },
                "c", 0);
            TestCountByPrefix(new List<string> { "aa", "ba", "bb", "bc" },
                "aaa", 0);
        }
    }
}

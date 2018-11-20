using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();

            var frequencyDictionary = GetFrequencyDictionary(text);
            foreach (var record in frequencyDictionary)
                result.Add(record.Key, GetMostFrequentKey(record.Value));

            return result;
        }

        private static Dictionary<string, Dictionary<string, int>> GetFrequencyDictionary(
            List<List<string>> text)
        {
            var result = new Dictionary<string, Dictionary<string, int>>();

            foreach (var sentence in text)
                for (var i = 0; i < sentence.Count - 1; ++i)
                {
                    // продолжение биграммы
                    AddValueToFrequencyDictionary(
                        sentence[i], sentence[i + 1], result);

                    // продолжение триграммы
                    if (i == sentence.Count - 2)
                        continue;

                    AddValueToFrequencyDictionary(
                        sentence[i] + " " + sentence[i + 1], 
                        sentence[i + 2], result);
                }

            return result;
        }

        private static void AddValueToFrequencyDictionary(string key, string value,
            Dictionary<string, Dictionary<string, int>> frequencyDictionary)
        {
            if (!frequencyDictionary.ContainsKey(key))
            {
                frequencyDictionary.Add(key, new Dictionary<string, int>());
                frequencyDictionary[key].Add(value, 1);
            }
            else if (frequencyDictionary[key].ContainsKey(value))
                ++frequencyDictionary[key][value];
            else
                frequencyDictionary[key].Add(value, 1);
        }

        private static string GetMostFrequentKey(Dictionary<string, int> frequencies)
        {
            var maxFrequency = 0;
            var maxFrequencyKey = "";
            foreach (var record in frequencies)
            {
                if (record.Value > maxFrequency)
                {
                    maxFrequency = record.Value;
                    maxFrequencyKey = record.Key;
                    continue;
                }

                if (record.Value == maxFrequency)
                    maxFrequencyKey =
                        string.CompareOrdinal(maxFrequencyKey, record.Key) > 0 ? 
                            record.Key : 
                            maxFrequencyKey;
            }

            return maxFrequencyKey;
        }
    }
}
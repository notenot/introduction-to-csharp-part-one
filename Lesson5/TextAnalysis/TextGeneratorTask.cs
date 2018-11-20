using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            if (nextWords.Count == 0)
                return phraseBeginning;

            var lastWords = phraseBeginning.Split(' ').Length > 2 ? 
                TakeTwoLastWords(phraseBeginning) : 
                phraseBeginning;
            var phraseBuilder = new StringBuilder(phraseBeginning);
            for (var i = 0; i < wordsCount; ++i)
            {
                var nextWord = ChooseNextWord(nextWords, lastWords);

                if (string.IsNullOrEmpty(nextWord))
                    break;

                phraseBuilder.Append($" {nextWord}");
                lastWords = TakeTwoLastWords(phraseBuilder.ToString());
            }
            return phraseBuilder.ToString();
        }

        private static string ChooseNextWord(Dictionary<string, string> nextWords, string lastWords)
        {
            if (lastWords.Split(' ').Length > 1 && nextWords.ContainsKey(lastWords))
                return nextWords[lastWords];

            var lastWord = TakeLastWord(lastWords);
            return nextWords.ContainsKey(lastWord) ? nextWords[lastWord] : null;
        }

        // Считается, что входная строка 'phrase' всегда содержит минимум 2 слова.
        private static string TakeTwoLastWords(string phrase)
        {
            var words = phrase.Split(' ');
            return words[words.Length - 2] + " " + words[words.Length - 1];
        }

        private static string TakeLastWord(string phrase) =>
            phrase.Substring(phrase.LastIndexOf(' ') + 1);
    }
}
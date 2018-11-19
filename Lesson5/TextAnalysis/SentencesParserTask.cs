using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var possibleSentences = text.Split('.', '!', '?', ';', ':', '(', ')');
            foreach (var possibleSentence in possibleSentences)
            {
                var sentence = ParseSentenceToWords(possibleSentence);
                if (sentence == null)
                    continue;
                sentencesList.Add(sentence);
            }

            return sentencesList;
        }

        private static List<string> ParseSentenceToWords(string sentence)
        {
            var sentenceList = new List<string>();
            var wordBuilder = new StringBuilder();
            foreach (var character in sentence)
            {
                if (char.IsLetter(character) || character == '\'')
                    wordBuilder.Append(char.ToLower(character));
                else
                {
                    if (wordBuilder.Length == 0)
                        continue;

                    sentenceList.Add(wordBuilder.ToString());
                    wordBuilder.Clear();
                }
            }

            if (wordBuilder.Length != 0)
                sentenceList.Add(wordBuilder.ToString());

            if (sentenceList.Count == 0)
                return null;
            return sentenceList;
        }
    }
}
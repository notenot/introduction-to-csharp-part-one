using System.Collections.Generic;
using System.Linq;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private static readonly char[] delimiters;
        // word <id, positions>
        private Dictionary<int, string[]> notIndexedWords; 
        private Dictionary<string, Dictionary<int, List<int>>> indexedWords;

        static Indexer()
        {
            delimiters = new[] { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
        }

        public Indexer()
        {
            notIndexedWords = new Dictionary<int, string[]>();
            indexedWords = new Dictionary<string, Dictionary<int, List<int>>>();
        }

        public void Add(int id, string documentText)
        {
            var words = documentText.Split(delimiters);
            notIndexedWords.Add(id, words);

            var position = 0;
            foreach (var word in words)
            {
                if (indexedWords.ContainsKey(word))
                    if (indexedWords[word].ContainsKey(id))
                        indexedWords[word][id].Add(position);
                    else
                        indexedWords[word].Add(id, new List<int> { position });
                else
                {
                    var positionsById = new Dictionary<int, List<int>>
                        { {id, new List<int> { position } } };
                    indexedWords.Add(word, positionsById);
                }

                position += word.Length + 1;
            }
        }

        public List<int> GetIds(string word) =>
            indexedWords.ContainsKey(word) 
                ? indexedWords[word].Keys.ToList() 
                : new List<int>();

        public List<int> GetPositions(int id, string word) =>
            indexedWords.ContainsKey(word) && indexedWords[word].ContainsKey(id)
                ? indexedWords[word][id]
                : new List<int>();

        public void Remove(int id)
        {
            if (!notIndexedWords.ContainsKey(id))
                return;

            foreach (var word in notIndexedWords[id])
                indexedWords[word].Remove(id);

            notIndexedWords.Remove(id);
        }
    }
}
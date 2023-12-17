using Serilog;

namespace SearchEngine
{
    public class SearchWordEngine
    {
        private Dictionary<string, HashSet<int>> indexDictionary;
        private List<string> sentences;
        private ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchWordEngine"/> class.
        /// </summary>
        /// <param name="dataset">The dataset of sentences.</param>
        /// <param name="logger">The logger for recording log messages.</param>
        public SearchWordEngine(IEnumerable<string> dataset, ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            CreateIndexDictionary(dataset);
        }

        /// <summary>
        /// Creates the index dictionary based on the provided dataset.
        /// </summary>
        /// <param name="input">The dataset of sentences.</param>
        public void CreateIndexDictionary(IEnumerable<string> input)
        {
            if (input != null)
            {

                indexDictionary = new Dictionary<string, HashSet<int>>();
                sentences = new List<string>();

                for (int i = 0; i < input.Count(); i++)
                {
                    sentences.Add(input.ElementAt(i));
                    string[] words = input.ElementAt(i).Split();

                    foreach (string word in words)
                    {
                        if (!indexDictionary.ContainsKey(word))
                        {
                            indexDictionary[word] = new HashSet<int>();
                        }

                        indexDictionary[word].Add(i);
                    }
                }

                logger.Information($"Index initialized with {sentences.Count} sentences and {indexDictionary.Count} unique words.");
            }
            else
            {
                logger.Warning($"Input is null.");
            }
        }

            /// <summary>
            /// Searches for sentences containing the specified word.
            /// </summary>
            /// <param name="word">The word to search for.</param>
            /// <returns>
            /// A list of sentences containing the specified word. 
            /// If the word is not found, an empty list is returned.
            /// </returns>
            public List<string> Search(string word)
            {
            if (indexDictionary != null && indexDictionary.TryGetValue(word, out var result))
            {
                List<string> res = new List<string>();
                foreach (int i in result)
                {
                    res.Add(sentences[i]);
                }
                return res;
            }
            else
            {
                logger.Information("Word was not found in any sentence");
                return new List<string>();
            }
        }
    }

}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Assignment8
{
    public class SpellChecker
    {
        private HashSet<string> dictionary;
        private List<string> allWordsInText;
        private HashSet<string> uniqueWordsInText;
        private HashSet<string> correctlySpelledWords;
        private HashSet<string> misspelledWords;
        private Dictionary<string, int> wordFrequency;
        private string currentFileName;

        public SpellChecker()
        {
            dictionary = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            allWordsInText = new List<string>();
            uniqueWordsInText = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            correctlySpelledWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            misspelledWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            wordFrequency = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            currentFileName = "";
        }

        public int DictionarySize => dictionary.Count;
        public bool HasAnalyzedText => !string.IsNullOrEmpty(currentFileName);
        public string CurrentFileName => currentFileName;

        public (int totalWords, int uniqueWords, int correctWords, int misspelledWords) GetTextStats()
        {
            return (
                allWordsInText.Count,
                uniqueWordsInText.Count,
                correctlySpelledWords.Count,
                misspelledWords.Count
            );
        }

        // ✅ 1. Load Dictionary
        public bool LoadDictionary(string filename)
        {
            try
            {
                if (!File.Exists(filename)) return false;
                var lines = File.ReadAllLines(filename);
                foreach (var line in lines)
                {
                    var word = NormalizeWord(line);
                    if (!string.IsNullOrEmpty(word))
                        dictionary.Add(word);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ✅ 2. Analyze Text File
        public bool AnalyzeTextFile(string filename)
        {
            try
            {
                if (!File.Exists(filename)) return false;
                string content = File.ReadAllText(filename);
                content = Regex.Replace(content, @"[^\w\s]", ""); // Remove punctuation
                var tokens = content.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                allWordsInText.Clear();
                uniqueWordsInText.Clear();
                wordFrequency.Clear();

                foreach (var token in tokens)
                {
                    var word = NormalizeWord(token);
                    if (!string.IsNullOrEmpty(word))
                    {
                        allWordsInText.Add(word);
                        uniqueWordsInText.Add(word);

                        if (!wordFrequency.ContainsKey(word))
                            wordFrequency[word] = 0;
                        wordFrequency[word]++;
                    }
                }

                currentFileName = filename;
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ✅ 3. Categorize Words
        public void CategorizeWords()
        {
            correctlySpelledWords.Clear();
            misspelledWords.Clear();

            foreach (var word in uniqueWordsInText)
            {
                if (dictionary.Contains(word))
                    correctlySpelledWords.Add(word);
                else
                    misspelledWords.Add(word);
            }
        }

        // ✅ 4. Check Individual Word
        public (bool inDictionary, bool inText, int occurrences) CheckWord(string word)
        {
            var normalizedWord = NormalizeWord(word);
            bool inDict = dictionary.Contains(normalizedWord);
            bool inText = uniqueWordsInText.Contains(normalizedWord);
            int occurrences = inText ? allWordsInText.Count(w => w.Equals(normalizedWord, StringComparison.OrdinalIgnoreCase)) : 0;
            return (inDict, inText, occurrences);
        }

        // ✅ 5. Get Misspelled Words
        public List<string> GetMisspelledWords(int maxResults = 50)
        {
            if (!HasAnalyzedText) return new List<string>();
            return misspelledWords.OrderBy(w => w).Take(maxResults).ToList();
        }

        // ✅ 6. Get Unique Words Sample
        public List<string> GetUniqueWordsSample(int maxResults = 20)
        {
            if (!HasAnalyzedText) return new List<string>();
            return uniqueWordsInText.OrderBy(w => w).Take(maxResults).ToList();
        }

        // Helper for normalization
        private string NormalizeWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return "";
            word = Regex.Replace(word.Trim(), @"[^\w]", "");
            return word.ToLowerInvariant();
        }

        // ✅ Enhanced Suggestions
        public Dictionary<string, List<string>> GetSuggestionsForMisspelledWords(int maxSuggestions = 3, int maxDistance = 3)
        {
            var suggestions = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            foreach (var misspelled in misspelledWords)
            {
                var ranked = dictionary
                    .Select(dictWord => new
                    {
                        Word = dictWord,
                        Distance = ComputeLevenshteinDistance(misspelled, dictWord),
                        Frequency = wordFrequency.ContainsKey(dictWord) ? wordFrequency[dictWord] : 0
                    })
                    .OrderBy(x => x.Distance)
                    .ThenByDescending(x => x.Frequency)
                    .ThenBy(x => x.Word.Length)
                    .ToList();

                // ✅ Filter by maxDistance or fallback to closest matches
                var filtered = ranked.Where(x => x.Distance <= maxDistance).Take(maxSuggestions).ToList();
                if (!filtered.Any())
                {
                    filtered = ranked.Take(maxSuggestions).ToList(); // Fallback
                }

                // ✅ Include edit distance in output
                suggestions[misspelled] = filtered.Select(x => $"{x.Word} (dist={x.Distance})").ToList();
            }

            return suggestions;
        }

        // Helper: Compute Levenshtein Distance
        private int ComputeLevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s)) return t.Length;
            if (string.IsNullOrEmpty(t)) return s.Length;

            int[,] dp = new int[s.Length + 1, t.Length + 1];

            for (int i = 0; i <= s.Length; i++) dp[i, 0] = i;
            for (int j = 0; j <= t.Length; j++) dp[0, j] = j;

            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    dp[i, j] = Math.Min(
                        Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                        dp[i - 1, j - 1] + cost
                    );
                }
            }

            return dp[s.Length, t.Length];
        }
    }
}
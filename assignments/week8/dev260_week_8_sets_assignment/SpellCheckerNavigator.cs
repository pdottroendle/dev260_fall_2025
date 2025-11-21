using System;
using System.Linq;

namespace Assignment8
{
    public class SpellCheckerNavigator
    {
        private readonly SpellChecker spellChecker;
        private bool isRunning;

        public SpellCheckerNavigator(SpellChecker spellChecker)
        {
            this.spellChecker = spellChecker ?? throw new ArgumentNullException(nameof(spellChecker));
            this.isRunning = true;
        }

        public void Run()
        {
            Console.WriteLine("📚 Spell Checker & Vocabulary Explorer");
            Console.WriteLine("======================================");
            Console.WriteLine("Welcome to the interactive spell checking application!");
            Console.WriteLine("Test your HashSet<T> implementations with real text analysis.\n");

            while (isRunning)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";
                Console.WriteLine($"You selected: {choice}");

                try
                {
                    ProcessCommand(choice.Trim());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }

                if (isRunning)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private void DisplayMainMenu()
        {
            int dictionarySize = spellChecker.DictionarySize;
            bool hasAnalysis = spellChecker.HasAnalyzedText;
            string fileName = hasAnalysis ? spellChecker.CurrentFileName : "None";

            Console.WriteLine("┌─ Spell Checker & Vocabulary Explorer ────────────┐");
            Console.WriteLine("│ 1. Load Dictionary │ 2. Analyze Text │ 3. Categorize │");
            Console.WriteLine("│ 4. Check Word │ 5. Misspelled │ 6. Unique │");
            Console.WriteLine("│ 7. Statistics │ 8. Exit │ 9. Suggestions │");
            Console.WriteLine("└───────────────────────────────────────────────────┘");
            Console.WriteLine($"Dictionary: {(dictionarySize > 0 ? $"{dictionarySize:N0} words" : "Not loaded")}\nAnalysis: {fileName}");
            Console.Write("\nChoose operation (number or name): ");
        }

        private void ProcessCommand(string input)
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return;

            string command = parts[0].ToLower();
            string[] args = parts.Skip(1).ToArray();

            switch (command)
            {
                case "1":
                case "load":
                    HandleLoadDictionaryCommand();
                    break;
                case "2":
                case "analyze":
                    HandleAnalyzeCommand(args);
                    break;
                case "3":
                case "categorize":
                    HandleCategorizeCommand();
                    break;
                case "4":
                case "check":
                    HandleCheckCommand(args);
                    break;
                case "5":
                case "misspelled":
                    HandleListMisspelledCommand();
                    break;
                case "6":
                case "unique":
                    HandleListUniqueCommand();
                    break;
                case "7":
                case "stats":
                    HandleStatsCommand();
                    break;
                case "8":
                case "exit":
                case "quit":
                    isRunning = false;
                    ShowGoodbye();
                    break;
                case "9":
                case "suggestions":
                    HandleSuggestionsCommand();
                    break;
                default:
                    Console.WriteLine($"❌ Unknown command: '{command}'. Please try again.\n");
                    break;
            }
        }

        private void HandleLoadDictionaryCommand()
        {
            Console.WriteLine("\n=== Load Dictionary ===");
            if (spellChecker.LoadDictionary("dictionary.txt"))
            {
                Console.WriteLine($"✓ SUCCESS: Dictionary loaded! Words: {spellChecker.DictionarySize:N0}");
            }
            else
            {
                Console.WriteLine("✗ FAILED: Could not load dictionary.");
            }
            Console.WriteLine();
        }

        private void HandleAnalyzeCommand(string[] args)
        {
            Console.WriteLine("\n=== Analyze Text ===");
            string filename;

            if (args.Length == 0)
            {
                Console.Write("Enter filename (sample.txt) :");
                filename = Console.ReadLine()?.Trim();
            }
            else
            {
                filename = args[0];
            }

            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("No filename provided.");
                return;
            }

            if (spellChecker.AnalyzeTextFile(filename))
            {
                Console.WriteLine($"✓ SUCCESS: Analyzed '{filename}'");
                var stats = spellChecker.GetTextStats();
                Console.WriteLine($" - Total words: {stats.totalWords}");
                Console.WriteLine($" - Unique words: {stats.uniqueWords}");
            }
            else
            {
                Console.WriteLine($"✗ FAILED: Could not analyze '{filename}'.");
            }
            Console.WriteLine();
        }

        private void HandleCategorizeCommand()
        {
            Console.WriteLine("\n=== Categorize Words ===");
            if (!spellChecker.HasAnalyzedText || spellChecker.DictionarySize == 0)
            {
                Console.WriteLine("⚠️ Please load dictionary and analyze text first.");
                return;
            }

            spellChecker.CategorizeWords();
            var stats = spellChecker.GetTextStats();
            Console.WriteLine($"✓ SUCCESS: Words categorized!");
            Console.WriteLine($" - Correctly spelled: {stats.correctWords}");
            Console.WriteLine($" - Misspelled: {stats.misspelledWords}");
            if (stats.uniqueWords > 0)
            {
                double accuracy = (double)stats.correctWords / stats.uniqueWords * 100;
                Console.WriteLine($" - Spelling accuracy: {accuracy:F1}%");
            }

            if (stats.misspelledWords > 0)
            {
                Console.WriteLine("\n🔍 Suggestions for misspelled words:");
                var suggestions = spellChecker.GetSuggestionsForMisspelledWords();
                foreach (var entry in suggestions)
                {
                    Console.WriteLine($"Misspelled: {entry.Key} → {string.Join(", ", entry.Value)}");
                }
            }
            Console.WriteLine();
        }

        private void HandleCheckCommand(string[] args)
        {
            Console.WriteLine("\n=== Check Word ===");
            string word;

            if (args.Length == 0)
            {
                Console.Write("Enter a word to check: ");
                word = Console.ReadLine()?.Trim();
            }
            else
            {
                word = args[0];
            }

            if (string.IsNullOrEmpty(word))
            {
                Console.WriteLine("No word provided.");
                return;
            }

            var result = spellChecker.CheckWord(word);
            Console.WriteLine($"In dictionary: {(result.inDictionary ? "YES" : "NO")}");
            Console.WriteLine($"In text: {(result.inText ? "YES" : "NO")}");
            if (result.inText) Console.WriteLine($"Occurrences: {result.occurrences}");
            Console.WriteLine();
        }

        private void HandleListMisspelledCommand()
        {
            Console.WriteLine("\n=== Misspelled Words ===");
            var words = spellChecker.GetMisspelledWords();
            if (words.Count == 0)
            {
                Console.WriteLine("🎉 No misspelled words found!");
            }
            else
            {
                Console.WriteLine($"Found {words.Count} misspelled words:");
                Console.WriteLine(string.Join(", ", words));
            }
            Console.WriteLine();
        }

        private void HandleListUniqueCommand()
        {
            Console.WriteLine("\n=== Unique Words Sample ===");
            var words = spellChecker.GetUniqueWordsSample();
            Console.WriteLine($"Sample ({words.Count}): {string.Join(", ", words)}");
            Console.WriteLine();
        }

        private void HandleStatsCommand()
        {
            Console.WriteLine("\n=== Statistics ===");
            var stats = spellChecker.GetTextStats();
            Console.WriteLine($"Total words: {stats.totalWords}");
            Console.WriteLine($"Unique words: {stats.uniqueWords}");
            Console.WriteLine($"Correct: {stats.correctWords}");
            Console.WriteLine($"Misspelled: {stats.misspelledWords}");
            Console.WriteLine();
        }

        private void HandleSuggestionsCommand()
        {
            Console.WriteLine("\n=== Suggestions for Misspelled Words ===");
            if (!spellChecker.HasAnalyzedText)
            {
                Console.WriteLine("⚠️ Analyze text first.");
                return;
            }
            var suggestions = spellChecker.GetSuggestionsForMisspelledWords();
            if (suggestions.Count == 0)
            {
                Console.WriteLine("🎉 No misspelled words to suggest corrections for!");
            }
            else
            {
                foreach (var entry in suggestions)
                {
                    Console.WriteLine($"Misspelled: {entry.Key} → {string.Join(", ", entry.Value)}");
                }
            }
            Console.WriteLine();
        }

        private void ShowGoodbye()
        {
            Console.WriteLine("\n📚 Thank you for using the Spell Checker!");
        }
    }
}
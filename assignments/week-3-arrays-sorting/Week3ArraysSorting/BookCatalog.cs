using System;
using System.Collections.Generic;
using System.IO;

namespace Week3ArraysSorting
{
    /// <summary>
    /// Book Catalog implementation for Assignment 2 Part B
    /// Demonstrates recursive sorting and multi-dimensional indexing for fast lookups
    /// 
    /// Learning Focus:
    /// - Recursive sorting algorithms (QuickSort or MergeSort)
    /// - Multi-dimensional array indexing for performance
    /// - String normalization and binary search
    /// - File I/O and CLI interaction
    /// </summary>
    public class BookCatalog
    {
        #region Data Structures

        // Book storage arrays - parallel arrays that stay synchronized
        private string[] originalTitles; // Original book titles for display
        private string[] normalizedTitles; // Normalized titles for sorting/searching

        // Multi-dimensional index for O(1) lookup by first two letters (A-Z x A-Z = 26x26)
        private int[,] startIndex; // Starting position for each letter pair in sorted array
        private int[,] endIndex; // Ending position for each letter pair in sorted array

        // Book count tracker
        private int bookCount;
        private int instructionCount = 0; // Count of executed instructions
        #endregion

        /// <summary>
        /// Constructor - Initialize the book catalog
        /// Sets up data structures for book storage and multi-dimensional indexing
        /// </summary>
        public BookCatalog()
        {
            // Initialize arrays (will be resized when books are loaded)
            originalTitles = Array.Empty<string>();
            normalizedTitles = Array.Empty<string>();

            // Initialize multi-dimensional index arrays (26x26 for A-Z x A-Z)
            startIndex = new int[26, 26];
            endIndex = new int[26, 26];

            // Initialize all index ranges as empty (-1 indicates no books for that letter pair)
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    startIndex[i, j] = -1; // -1 means no books start with this letter pair
                    endIndex[i, j] = -1; // -1 means no books end with this letter pair
                }
            }

            // Reset book count
            bookCount = 0;

            Console.WriteLine("BookCatalog initialized - Ready to load books and build index");
        }

        /// <summary>
        /// Load books from file and build sorted index
        /// </summary>
        /// <param name="filePath">Path to books.txt file</param>
        public void LoadBooks(string filePath)
        {
            try
            {
                Console.WriteLine($"Loading books from: {filePath}");

                // Step 1 - Load books from file
                LoadBooksFromFile(filePath);

                // TODO: Step 2 - Sort using recursive algorithm
                SortBooksRecursively();

                // TODO: Step 3 - Build multi-dimensional index
                BuildMultiDimensionalIndex();

                Console.WriteLine($"Successfully loaded and indexed {bookCount} books.");
                Console.WriteLine("Index built for A-Z x A-Z (26x26) letter pairs.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading books: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Start interactive lookup session
        /// TODO: Implement the CLI loop
        /// </summary>
        public void StartLookupSession()
        {
            //Console.Clear();
            Console.WriteLine("=== BOOK CATALOG LOOKUP (Part B) ===");
            Console.WriteLine();

            // TODO: Check if books are loaded
            if (bookCount == 0)
            {
                Console.WriteLine("No books loaded! Please load a book file first.");
                return;
            }

            DisplayLookupInstructions();

            // TODO: Implement lookup loop
            bool keepLooking = true;

            while (keepLooking)
            {
                Console.WriteLine();
                Console.Write("Enter a book title (or 'exit'): ");
                string? query = Console.ReadLine();

                // TODO: Handle exit condition
                if (string.IsNullOrEmpty(query) || query.ToLowerInvariant() == "exit")
                {
                    keepLooking = false;
                    continue;
                }

                // TODO: Perform lookup
                PerformLookup(query);
            }

            Console.WriteLine("Returning to main menu...");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Load book titles from text file
        /// </summary>
        /// <param name="filePath">Path to the books file</param>
        private void LoadBooksFromFile(string filePath)
        {
            // Check if file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Book file not found: {filePath}");
            }

            Console.WriteLine($"Reading book titles from: {filePath}");

            try
            {
                // Read all lines from file
                string[] lines = File.ReadAllLines(filePath);

                // Filter out empty lines and whitespace-only lines
                var validLines = new List<string>();
                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();
                    if (!string.IsNullOrEmpty(trimmedLine))
                    {
                        validLines.Add(trimmedLine);
                    }
                }

                // Initialize arrays with the correct size
                bookCount = validLines.Count;
                originalTitles = new string[bookCount];
                normalizedTitles = new string[bookCount];

                // Store both original and normalized versions
                for (int i = 0; i < bookCount; i++)
                {
                    originalTitles[i] = validLines[i]; // Keep original formatting for display
                    normalizedTitles[i] = NormalizeTitle(originalTitles[i]); // Normalized for sorting/indexing
                }

                Console.WriteLine($"Successfully loaded {bookCount} book titles.");
            }
            catch (IOException ex)
            {
                throw new IOException($"Error reading file '{filePath}': {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Unexpected error loading books from '{filePath}': {ex.Message}",
                    ex
                );
            }
        }

        /// <summary>
        /// Normalize book title for consistent sorting and indexing
        /// </summary>
        /// <param name="title">Original book title</param>
        /// <returns>Normalized title for sorting/indexing</returns>
        private string NormalizeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return "";
            }

            // Step 1: Trim whitespace and convert to uppercase
            string normalized = title.Trim().ToUpperInvariant();

            // Step 2: Optional - Remove leading articles for better sorting
            // This helps group books by their main title rather than article
            string[] articles = { "THE ", "A ", "AN " };

            foreach (string article in articles)
            {
                if (normalized.StartsWith(article))
                {
                    normalized = normalized.Substring(article.Length).Trim();
                    break; // Only remove the first article found
                }
            }

            // Step 3: Handle edge case where title was only articles
            if (string.IsNullOrEmpty(normalized))
            {
                return title.Trim().ToUpperInvariant(); // Return original if normalization results in empty
            }

            return normalized;
        }

        private void DisplayLookupInstructions()
        {
            Console.WriteLine("BOOK LOOKUP INSTRUCTIONS:");
            Console.WriteLine("- Enter any book title to search");
            Console.WriteLine("- Exact matches will be shown if found");
            Console.WriteLine("- Suggestions provided for partial/no matches");
            Console.WriteLine("- Type 'exit' to return to main menu");
            Console.WriteLine();
            Console.WriteLine(
                $"Catalog contains {bookCount} books, sorted and indexed for fast lookup."
            );
        }

        private void SortBooksRecursively()
        {
            instructionCount = 0;
            Console.WriteLine("Sorting books using QuickSort...");
            QuickSort(normalizedTitles, originalTitles, 0, bookCount - 1);
            Console.WriteLine(
                $"QuickSort Code instructions executed: >>>>>>>>>>>>>>{instructionCount}"
            );

            instructionCount = 0;
            Console.WriteLine("Sorting books using MergeSort...");
            MergeSort(normalizedTitles, originalTitles, 0, bookCount - 1);
            Console.WriteLine(
                $"MergeSort Code instructions executed:  >>>>>>>>>>>>>>{instructionCount}"
            );
        }
        private void MergeSort(string[] norm, string[] orig, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                MergeSort(norm, orig, left, mid);
                MergeSort(norm, orig, mid + 1, right);
                Merge(norm, orig, left, mid, right);
                instructionCount++;
                instructionCount++;
                instructionCount++;
                instructionCount++;
            }
        }

        private void Merge(string[] norm, string[] orig, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            string[] leftNorm = new string[n1];
            string[] rightNorm = new string[n2];
            string[] leftOrig = new string[n1];
            string[] rightOrig = new string[n2];

            for (int i = 0; i < n1; i++)
            {
                instructionCount++;
                instructionCount++;
                leftNorm[i] = norm[left + i];
                leftOrig[i] = orig[left + i];
            }
            for (int j = 0; j < n2; j++)
            {
                instructionCount++;
                instructionCount++;
                rightNorm[j] = norm[mid + 1 + j];
                rightOrig[j] = orig[mid + 1 + j];
            }

            int i1 = 0,
                i2 = 0,
                k = left;
            while (i1 < n1 && i2 < n2)
            {
                if (string.Compare(leftNorm[i1], rightNorm[i2]) <= 0)
                {
                    norm[k] = leftNorm[i1];
                    orig[k] = leftOrig[i1];
                    i1++;
                    instructionCount++;
                    instructionCount++;
                    instructionCount++;
                }
                else
                {
                    norm[k] = rightNorm[i2];
                    orig[k] = rightOrig[i2];
                    i2++;
                    instructionCount++;
                    instructionCount++;
                    instructionCount++;
                }
                k++;
                instructionCount++;
            }

            while (i1 < n1)
            {
                norm[k] = leftNorm[i1];
                orig[k] = leftOrig[i1];
                i1++;
                instructionCount++;
                instructionCount++;
                k++;
                instructionCount++;
            }

            while (i2 < n2)
            {
                norm[k] = rightNorm[i2];
                orig[k] = rightOrig[i2];
                i2++;
                instructionCount++;
                instructionCount++;
                k++;
                instructionCount++;
                instructionCount++;
            }
        }
        private void QuickSort(string[] norm, string[] orig, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(norm, orig, low, high);
                QuickSort(norm, orig, low, pivotIndex - 1);
                QuickSort(norm, orig, pivotIndex + 1, high);
                instructionCount++;
                instructionCount++;
                instructionCount++;
            }
        }

        private int Partition(string[] norm, string[] orig, int low, int high)
        {
            string pivot = norm[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (string.Compare(norm[j], pivot) <= 0)
                {
                    i++;
                    instructionCount++;
                    instructionCount++;
                    instructionCount++;
                    Swap(norm, i, j);
                    Swap(orig, i, j);
                }
            }

            Swap(norm, i + 1, high);
            instructionCount++;
            Swap(orig, i + 1, high);
            instructionCount++;
            instructionCount++;
            return i + 1;
        }

        private void Swap(string[] array, int i, int j)
        {
            string temp = array[i];
            instructionCount++;
            array[i] = array[j];
            instructionCount++;
            array[j] = temp;
            instructionCount++;
        }

        private void BuildMultiDimensionalIndex()
        {
            for (int i = 0; i < 26; i++)
                for (int j = 0; j < 26; j++)
                {
                    startIndex[i, j] = -1;
                    endIndex[i, j] = -1;
                }

            for (int i = 0; i < bookCount; i++)
            {
                string title = normalizedTitles[i];
                if (title.Length < 2)
                    continue;

                int first = GetLetterIndex(title[0]);
                int second = GetLetterIndex(title[1]);

                if (first < 0 || second < 0)
                    continue;

                if (startIndex[first, second] == -1)
                    startIndex[first, second] = i;

                endIndex[first, second] = i + 1;
            }
        }

        private int GetLetterIndex(char c)
        {
            c = char.ToUpperInvariant(c);
            return (c >= 'A' && c <= 'Z') ? c - 'A' : -1;
        }

        private void PerformLookup(string query)
        {
            string normalizedQuery = NormalizeTitle(query);
            if (normalizedQuery.Length < 2)
            {
                Console.WriteLine("Query too short for indexed lookup.");
                ShowSuggestions(normalizedQuery);
                return;
            }

            int first = GetLetterIndex(normalizedQuery[0]);
            int second = GetLetterIndex(normalizedQuery[1]);

            if (first < 0 || second < 0 || startIndex[first, second] == -1)
            {
                Console.WriteLine("No exact match found. Suggestions:");
                ShowSuggestions(normalizedQuery);
                return;
            }

            int start = startIndex[first, second];
            int end = endIndex[first, second];

            int foundIndex = BinarySearchInRange(normalizedQuery, start, end);
            if (foundIndex != -1)
            {
                Console.WriteLine($"✅ Found: {originalTitles[foundIndex]}");
            }
            else
            {
                Console.WriteLine("No exact match found. Suggestions:");
                ShowSuggestions(normalizedQuery, start, end);
            }
        }

        private int BinarySearchInRange(string query, int start, int end)
        {
            int low = start;
            int high = end - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                int cmp = string.Compare(normalizedTitles[mid], query);
                if (cmp == 0)
                    return mid;
                else if (cmp < 0)
                    low = mid + 1;
                else
                    high = mid - 1;
            }

            return -1;
        }

        private void ShowSuggestions(string query, int start = 0, int end = -1)
        {
            int maxSuggestions = 5;
            List<string> suggestions = new List<string>();

            if (end == -1 || start >= end)
            {
                for (int i = 0; i < bookCount && suggestions.Count < maxSuggestions; i++)
                {
                    if (normalizedTitles[i].StartsWith(query))
                        suggestions.Add(originalTitles[i]);
                }
            }
            else
            {
                for (int i = start; i < end && suggestions.Count < maxSuggestions; i++)
                {
                    if (normalizedTitles[i].StartsWith(query))
                        suggestions.Add(originalTitles[i]);
                }
            }

            if (suggestions.Count == 0)
            {
                Console.WriteLine("No suggestions found.");
            }
            else
            {
                foreach (var title in suggestions)
                    Console.WriteLine($"• {title}");
            }
        }
    }
}

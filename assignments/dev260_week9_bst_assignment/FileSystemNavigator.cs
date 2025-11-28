
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystemNavigator
{
    public class FileSystemNavigator
    {
        private readonly FileSystemBST fileSystem;
        private bool isRunning;

        public FileSystemNavigator(FileSystemBST fileSystem)
        {
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            this.isRunning = true;
        }

        public void Run()
        {
            Console.WriteLine("BST File System Navigator");
            Console.WriteLine("=========================");
            Console.WriteLine("Test your Binary Search Tree implementations with file operations.\n");

            while (isRunning)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";
                Console.WriteLine($"You selected: {choice}");

                try
                {
                    ProcessCommand(choice);
                }
                catch (NotImplementedException ex)
                {
                    Console.WriteLine($"\nError: Method not implemented yet: {ex.Message}");
                    Console.WriteLine("Implement the TODO method to use this feature.\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}\n");
                }

                if (isRunning)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
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
                case "create":
                case "file":
                    HandleCreateFileCommand(args);
                    break;

                case "2":
                case "mkdir":
                case "directory":
                    HandleCreateDirectoryCommand(args);
                    break;

                case "3":
                case "find":
                case "search":
                    HandleFindFileCommand(args);
                    break;

                case "4":
                case "extension":
                case "filter":
                    HandleFindByExtensionCommand(args);
                    break;

                case "5":
                case "size":
                case "range":
                    HandleFindBySizeCommand(args);
                    break;

                case "6":
                case "largest":
                case "biggest":
                    HandleFindLargestCommand(args);
                    break;

                case "7":
                case "total":
                case "calculate":
                    HandleCalculateTotalCommand();
                    break;

                case "8":
                case "delete":
                case "remove":
                    HandleDeleteCommand(args);
                    break;

                case "9":
                case "tree":
                case "display":
                    HandleDisplayTreeCommand();
                    break;

                case "10":
                case "stats":
                case "statistics":
                    HandleStatsCommand();
                    break;

                case "11":
                case "sample":
                case "demo":
                    HandleLoadSampleCommand();
                    break;

                case "12":
                case "exit":
                case "quit":
                    isRunning = false;
                    ShowGoodbye();
                    break;

                // NEW: Regex command
                case "13":
                case "regex":
                case "pattern":
                    HandleFindByRegexCommand(args);
                    break;

                // NEW: Wildcard command
                case "14":
                case "wildcard":
                case "glob":
                    HandleFindByWildcardCommand(args);
                    break;

                default:
                    Console.WriteLine($"Unknown command: '{command}'. Please try again.\n");
                    break;
            }
        }

        private void DisplayMainMenu()
        {
            var stats = fileSystem.GetStatistics();

            Console.WriteLine("┌─ BST File System Navigator ───────────────────────────┐");
            Console.WriteLine("│ 1. Create File │ 2. Create Dir │ 3. Find File        │");
            Console.WriteLine("│ 4. Find by Ext │ 5. Find by Size │ 6. Largest        │");
            Console.WriteLine("│ 7. Total Size  │ 8. Delete Item  │ 9. Show Tree       │");
            Console.WriteLine("│ 10. Statistics │ 11. Load Sample │ 12. Exit           │");
            Console.WriteLine("│ 13. Regex Find (extra)                                │");
            Console.WriteLine("│ 14. Wildcard Find (extra)                             │");
            Console.WriteLine("└───────────────────────────────────────────────────────┘");

            Console.WriteLine($"Status: {stats.TotalFiles} files, {stats.TotalDirectories} directories");
            Console.WriteLine($"Storage: {stats.FormatSize(stats.TotalSize)}");
            Console.WriteLine($"Operations: {stats.TotalOperations}");
            Console.WriteLine();
            Console.WriteLine("Tip: Use numbers (1-14) or keywords (create, find, regex, wildcard, etc.)");
            Console.Write("Enter your choice: ");
        }

        private void ShowGoodbye()
        {
            Console.WriteLine("\nThank you for using BST File System Navigator!");
            Console.WriteLine("==============================================");
            var stats = fileSystem.GetStatistics();
            if (!fileSystem.IsEmpty())
            {
                Console.WriteLine("Session Summary:");
                Console.WriteLine($" Files Created: {stats.TotalFiles}");
                Console.WriteLine($" Directories Created: {stats.TotalDirectories}");
                Console.WriteLine($" Total Operations: {stats.TotalOperations}");
                Console.WriteLine($" Session Duration: {stats.SessionDuration:mm\\:ss}");
            }
            Console.WriteLine("\nYou've practiced essential Binary Search Tree concepts!");
            Console.WriteLine("These algorithms power file systems, databases, and search engines.");
        }

        // ---------- Handlers ----------
        private void HandleCreateFileCommand(string[] args)
        {
            string fileName;
            long size = 1024;
            if (args.Length == 0)
            {
                Console.Write("Enter file name (e.g., readme.txt, app.cs): ");
                fileName = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("Invalid file name provided.");
                    return;
                }
                Console.Write("Enter file size in bytes (default 1024): ");
                var sizeInput = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(sizeInput) && long.TryParse(sizeInput, out long parsedSize))
                    size = parsedSize;
            }
            else
            {
                fileName = args[0];
                if (args.Length > 1 && long.TryParse(args[1], out long parsedSize))
                    size = parsedSize;
            }

            Console.WriteLine();
            bool success = fileSystem.CreateFile(fileName, size);
            if (success)
            {
                Console.WriteLine($"File created successfully: {fileName}");
            }
            else
            {
                Console.WriteLine($"Failed to create file: {fileName}");
                Console.WriteLine("This usually means the file already exists in the BST.");
            }
        }

        private void HandleCreateDirectoryCommand(string[] args)
        {
            string dirName;
            if (args.Length == 0)
            {
                Console.Write("Enter directory name (e.g., Documents, Projects): ");
                dirName = Console.ReadLine()?.Trim() ?? "";
            }
            else
            {
                dirName = args[0];
            }
            if (string.IsNullOrEmpty(dirName))
            {
                Console.WriteLine("Invalid directory name provided.");
                return;
            }

            Console.WriteLine();
            bool success = fileSystem.CreateDirectory(dirName);
            if (success)
            {
                Console.WriteLine($"Directory created successfully: {dirName}");
            }
            else
            {
                Console.WriteLine($"Failed to create directory: {dirName}");
                Console.WriteLine("This usually means the directory already exists in the BST.");
            }
        }

        private void HandleFindFileCommand(string[] args)
        {
            string fileName;
            if (args.Length == 0)
            {
                Console.Write("Enter file name to search for: ");
                fileName = Console.ReadLine()?.Trim() ?? "";
            }
            else
            {
                fileName = args[0];
            }
            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("Invalid file name provided.");
                return;
            }

            Console.WriteLine();
            var result = fileSystem.FindFile(fileName);
            if (result != null)
            {
                Console.WriteLine($"File found: {result}");
                Console.WriteLine("O(log n) BST search completed successfully!");
            }
            else
            {
                Console.WriteLine($"File not found: {fileName}");
                Console.WriteLine("BST search traversed the tree but no matching file exists.");
            }
        }

        private void HandleFindByExtensionCommand(string[] args)
        {
            string extension;
            if (args.Length == 0)
            {
                Console.Write("Enter file extension (e.g., .txt, .cs): ");
                extension = Console.ReadLine()?.Trim() ?? "";
            }
            else
            {
                extension = args[0];
            }
            if (string.IsNullOrEmpty(extension))
            {
                Console.WriteLine("Invalid extension provided.");
                return;
            }
            if (!extension.StartsWith(".")) extension = "." + extension;

            Console.WriteLine();
            var results = fileSystem.FindFilesByExtension(extension);
            Console.WriteLine($"\nFound {results.Count} files with extension '{extension}':");
            Console.WriteLine("=" + new string('=', 60));
            if (results.Count == 0)
            {
                Console.WriteLine(" (No files found)");
            }
            else
            {
                foreach (var file in results.Take(10))
                    Console.WriteLine($" {file}");
                if (results.Count > 10)
                    Console.WriteLine($" ... and {results.Count - 10} more files");
            }
        }

        private void HandleFindBySizeCommand(string[] args)
        {
            long minSize, maxSize;
            if (args.Length < 2)
            {
                Console.Write("Enter minimum size in bytes: ");
                if (!long.TryParse(Console.ReadLine(), out minSize))
                {
                    Console.WriteLine("Invalid minimum size provided.");
                    return;
                }
                Console.Write("Enter maximum size in bytes: ");
                if (!long.TryParse(Console.ReadLine(), out maxSize))
                {
                    Console.WriteLine("Invalid maximum size provided.");
                    return;
                }
            }
            else
            {
                if (!long.TryParse(args[0], out minSize) || !long.TryParse(args[1], out maxSize))
                {
                    Console.WriteLine("Invalid size parameters. Usage: size <min> <max>");
                    return;
                }
            }

            Console.WriteLine();
            var results = fileSystem.FindFilesBySize(minSize, maxSize);
            Console.WriteLine($"\nFound {results.Count} files between {FormatSize(minSize)} and {FormatSize(maxSize)}:");
            Console.WriteLine("=" + new string('=', 70));
            if (results.Count == 0)
            {
                Console.WriteLine(" (No files found in size range)");
            }
            else
            {
                foreach (var file in results.Take(10))
                    Console.WriteLine($" {file}");
                if (results.Count > 10)
                    Console.WriteLine($" ... and {results.Count - 10} more files");
            }
        }

        private void HandleFindLargestCommand(string[] args)
        {
            int count;
            if (args.Length == 0)
            {
                Console.Write("Enter number of largest files to find: ");
                if (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
                {
                    Console.WriteLine("Invalid count provided.");
                    return;
                }
            }
            else
            {
                if (!int.TryParse(args[0], out count) || count <= 0)
                {
                    Console.WriteLine("Invalid count parameter. Usage: largest <count>");
                    return;
                }
            }

            Console.WriteLine();
            var results = fileSystem.FindLargestFiles(count);
            Console.WriteLine($"\nTop {Math.Min(count, results.Count)} largest files:");
            Console.WriteLine("=" + new string('=', 50));
            if (results.Count == 0)
            {
                Console.WriteLine(" (No files found)");
            }
            else
            {
                for (int i = 0; i < results.Count; i++)
                    Console.WriteLine($" #{i + 1}: {results[i]}");
            }
        }

        private void HandleCalculateTotalCommand()
        {
            Console.WriteLine();
            long totalSize = fileSystem.CalculateTotalSize();
            Console.WriteLine($"Total file system size: {FormatSize(totalSize)}");
            Console.WriteLine($"Raw bytes: {totalSize:N0}");
        }

        private void HandleDeleteCommand(string[] args)
        {
            string itemName;
            if (args.Length == 0)
            {
                Console.Write("Enter name of file or directory to delete: ");
                itemName = Console.ReadLine()?.Trim() ?? "";
            }
            else
            {
                itemName = args[0];
            }
            if (string.IsNullOrEmpty(itemName))
            {
                Console.WriteLine("Invalid item name provided.");
                return;
            }

            Console.WriteLine();
            bool success = fileSystem.DeleteItem(itemName);
            if (success)
            {
                Console.WriteLine($"Item deleted successfully: {itemName}");
            }
            else
            {
                Console.WriteLine($"Failed to delete item: {itemName}");
                Console.WriteLine("Item not found in BST - nothing to delete.");
            }
        }

        private void HandleDisplayTreeCommand()
        {
            Console.WriteLine("\nCurrent File System Tree Structure:");
            Console.WriteLine("=====================================");
            fileSystem.DisplayTree();
            Console.WriteLine("\nThis shows your BST structure - directories before files, then alphabetical!");
        }

        private void HandleStatsCommand()
        {
            Console.WriteLine("\nFile System Statistics:");
            Console.WriteLine("===========================");
            var stats = fileSystem.GetStatistics();
            Console.WriteLine(stats.ToString());
            if (stats.TotalFiles + stats.TotalDirectories > 0)
            {
                Console.WriteLine("\nBST Advantages:");
                Console.WriteLine($" - O(log n) search time for {stats.TotalFiles + stats.TotalDirectories} items");
                Console.WriteLine(" - Automatic sorting maintained during insertions");
                Console.WriteLine(" - Efficient range queries and filtering operations");
            }
        }

        private void HandleLoadSampleCommand()
        {
            Console.WriteLine("\nLoading sample file system data...");
            fileSystem.LoadSampleData();
            Console.WriteLine("\nNow you can test search, filter, and analysis operations!");
        }

        // ---------- NEW: Regex handler ----------
        private void HandleFindByRegexCommand(string[] args)
        {
            string pattern;
            string target = "name";
            bool includeDirs = false;

            if (args.Length == 0)
            {
                Console.Write("Enter regex pattern (e.g., ^app.*\\.cs$): ");
                pattern = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Target [name|extension|both] (default name): ");
                var t = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(t)) target = t!;

                Console.Write("Include directories? [y/N]: ");
                var inc = Console.ReadLine()?.Trim();
                includeDirs = string.Equals(inc, "y", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                // CLI form: regex <pattern> [target] [includeDirs]
                pattern = args[0];
                if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1])) target = args[1];
                if (args.Length > 2) includeDirs = args[2].Equals("y", StringComparison.OrdinalIgnoreCase);
            }

            Console.WriteLine();
            var results = fileSystem.FindFilesByRegex(pattern, target, includeDirs);

            Console.WriteLine($"\nRegex: '{pattern}'  Target: {target}  Include Dirs: {includeDirs}");
            Console.WriteLine(new string('=', 70));

            if (results.Count == 0)
            {
                Console.WriteLine(" (No matches)");
                Console.WriteLine("In-order traversal completed - no items matched the regex.");
            }
            else
            {
                foreach (var file in results.Take(10))
                    Console.WriteLine($" {file}");

                if (results.Count > 10)
                    Console.WriteLine($" ... and {results.Count - 10} more matches");

                Console.WriteLine("Regex-based tree traversal completed successfully!");
            }
        }

        // ---------- NEW: Wildcard handler ----------
        private void HandleFindByWildcardCommand(string[] args)
        {
            string pattern;
            string target = "name";
            bool includeDirs = false;

            if (args.Length == 0)
            {
                Console.Write("Enter wildcard pattern (e.g., *.txt, file?.cs): ");
                pattern = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Target [name|extension|both] (default name): ");
                var t = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(t)) target = t!;

                Console.Write("Include directories? [y/N]: ");
                var inc = Console.ReadLine()?.Trim();
                includeDirs = string.Equals(inc, "y", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                // CLI form: wildcard <pattern> [target] [includeDirs]
                pattern = args[0];
                if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1])) target = args[1];
                if (args.Length > 2) includeDirs = args[2].Equals("y", StringComparison.OrdinalIgnoreCase);
            }

            Console.WriteLine();
            var results = fileSystem.FindFilesByWildcard(pattern, target, includeDirs);

            Console.WriteLine($"\nWildcard: '{pattern}'  Target: {target}  Include Dirs: {includeDirs}");
            Console.WriteLine(new string('=', 70));

            if (results.Count == 0)
            {
                Console.WriteLine(" (No matches)");
                Console.WriteLine("In-order traversal completed - no items matched the wildcard.");
            }
            else
            {
                foreach (var file in results.Take(10))
                    Console.WriteLine($" {file}");

                if (results.Count > 10)
                    Console.WriteLine($" ... and {results.Count - 10} more matches");

                Console.WriteLine("Wildcard-based tree traversal completed successfully!");
            }
        }

        private static string FormatSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes} B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024:F1} KB";
            if (bytes < 1024 * 1024 * 1024) return $"{bytes / (1024 * 1024):F1} MB";
            return $"{bytes / (1024 * 1024 * 1024):F1} GB";
        }
    }
}
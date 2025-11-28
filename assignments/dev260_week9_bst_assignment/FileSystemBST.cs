
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;                      // Path.GetFileName, Path.GetExtension
using System.Text.RegularExpressions; // Regex

namespace FileSystemNavigator
{
    public class FileSystemBST
    {
        private TreeNode? root;
        private int operationCount;
        private DateTime sessionStart;

        public FileSystemBST()
        {
            root = null;
            operationCount = 0;
            sessionStart = DateTime.Now;
        }

        // =========================================================
        // Public API
        // =========================================================

        public bool CreateFile(string fileName, long size = 1024)
        {
            operationCount++;

            if (string.IsNullOrWhiteSpace(fileName) || size < 0)
                return false;

            var nameOnly = Path.GetFileName(fileName);

            if (Exists(root, new FileNode(nameOnly, FileType.File)))
                return false;

            var nodeData = new FileNode(nameOnly, FileType.File, size);
            root = InsertNode(root, nodeData);
            return true;
        }

        public bool CreateDirectory(string directoryName)
        {
            operationCount++;

            if (string.IsNullOrWhiteSpace(directoryName))
                return false;

            var nameOnly = Path.GetFileName(directoryName);

            if (Exists(root, new FileNode(nameOnly, FileType.Directory)))
                return false;

            var nodeData = new FileNode(nameOnly, FileType.Directory);
            root = InsertNode(root, nodeData);
            return true;
        }

        public FileNode? FindFile(string fileName)
        {
            operationCount++;

            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            var nameOnly = Path.GetFileName(fileName);
            return SearchNode(root, nameOnly);
        }

        public List<FileNode> FindFilesByExtension(string extension)
        {
            operationCount++;

            if (string.IsNullOrWhiteSpace(extension))
                return new List<FileNode>();

            if (!extension.StartsWith(".")) extension = "." + extension;

            var results = new List<FileNode>();
            TraverseAndCollect(
                root,
                results,
                f => f.Type == FileType.File &&
                     string.Equals(f.Extension ?? "", extension, StringComparison.OrdinalIgnoreCase));

            return results;
        }

        public List<FileNode> FindFilesBySize(long minSize, long maxSize)
        {
            operationCount++;

            if (minSize > maxSize) (minSize, maxSize) = (maxSize, minSize);

            var results = new List<FileNode>();
            TraverseAndCollect(
                root,
                results,
                f => f.Type == FileType.File && f.Size >= minSize && f.Size <= maxSize);

            return results;
        }

        public List<FileNode> FindLargestFiles(int count)
        {
            operationCount++;

            if (count <= 0) return new List<FileNode>();

            var files = new List<FileNode>();
            TraverseAndCollect(root, files, f => f.Type == FileType.File);

            return files
                .OrderByDescending(f => f.Size)
                .Take(count)
                .ToList();
        }

        public long CalculateTotalSize()
        {
            operationCount++;

            long Sum(TreeNode? node)
            {
                if (node == null) return 0;
                long here = (node.FileData.Type == FileType.File) ? node.FileData.Size : 0;
                return here + Sum(node.Left) + Sum(node.Right);
            }

            return Sum(root);
        }

        public bool DeleteItem(string fileOrDirName)
        {
            operationCount++;

            if (string.IsNullOrWhiteSpace(fileOrDirName))
                return false;

            var nameOnly = Path.GetFileName(fileOrDirName);

            bool removed = false;

            // Try delete File
            root = DeleteNode(root, new FileNode(nameOnly, FileType.File), ref removed);
            if (removed) return true;

            // Try delete Directory
            root = DeleteNode(root, new FileNode(nameOnly, FileType.Directory), ref removed);
            return removed;
        }

        // =========================================================
        // EXTRA CREDIT: Regex + Wildcard (MODEL methods only)
        // =========================================================

        private static string WildcardToRegex(string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return "^$";
            string escaped = Regex.Escape(pattern);
            escaped = escaped.Replace(@"\*", ".*").Replace(@"\?", ".");
            return "^" + escaped + "$";
        }

        /// <summary>
        /// Regex-based search over Name/Extension/Both.
        /// Returns matches in in-order traversal order.
        /// </summary>
        public List<FileNode> FindFilesByRegex(
            string pattern,
            string target = "name",
            bool includeDirectories = false,
            RegexOptions options = RegexOptions.IgnoreCase)
        {
            operationCount++;

            var results = new List<FileNode>();
            if (string.IsNullOrWhiteSpace(pattern))
                return results;

            Regex? rx;
            try { rx = new Regex(pattern, options); }
            catch (ArgumentException) { return results; }

            bool Match(FileNode f)
            {
                if (!includeDirectories && f.Type == FileType.Directory) return false;

                string name = f.Name ?? "";
                string ext = f.Extension ?? "";

                return target.ToLowerInvariant() switch
                {
                    "extension" => rx!.IsMatch(ext),
                    "both" => rx!.IsMatch(name) || rx!.IsMatch(ext), // <-- IMPORTANT
                    _ => rx!.IsMatch(name),                     // default: name
                };
            }

            TraverseAndCollect(root, results, Match);
            return results;
        }

        /// <summary>Wildcard-based search mapped to Regex internally.</summary>
        public List<FileNode> FindFilesByWildcard(
            string wildcard,
            string target = "name",
            bool includeDirectories = false,
            RegexOptions options = RegexOptions.IgnoreCase)
        {
            operationCount++;
            var regex = WildcardToRegex(wildcard);
            return FindFilesByRegex(regex, target, includeDirectories, options);
        }

        // =========================================================
        // BST Core Helpers
        // =========================================================

        private TreeNode? InsertNode(TreeNode? node, FileNode fileData)
        {
            if (node == null)
                return new TreeNode(fileData);

            int cmp = CompareFileNodes(fileData, node.FileData);
            if (cmp < 0)
            {
                node.Left = InsertNode(node.Left, fileData);
            }
            else if (cmp > 0)
            {
                node.Right = InsertNode(node.Right, fileData);
            }
            else
            {
                // Duplicate (Type + Name) -> ignore
            }
            return node;
        }

        private FileNode? SearchNode(TreeNode? node, string fileName)
        {
            var key = new FileNode(fileName, FileType.File);

            while (node != null)
            {
                int cmp = CompareFileNodes(key, node.FileData);
                if (cmp < 0) node = node.Left;
                else if (cmp > 0) node = node.Right;
                else return node.FileData; // found
            }
            return null;
        }

        private void TraverseAndCollect(TreeNode? node, List<FileNode> collection, Func<FileNode, bool> filter)
        {
            if (node == null) return;

            TraverseAndCollect(node.Left, collection, filter);

            if (filter(node.FileData))
                collection.Add(node.FileData);

            TraverseAndCollect(node.Right, collection, filter);
        }

        private TreeNode? DeleteNode(TreeNode? node, FileNode key, ref bool removed)
        {
            if (node == null) return null;

            int cmp = CompareFileNodes(key, node.FileData);
            if (cmp < 0)
            {
                node.Left = DeleteNode(node.Left, key, ref removed);
                return node;
            }
            else if (cmp > 0)
            {
                node.Right = DeleteNode(node.Right, key, ref removed);
                return node;
            }

            // Found node to delete
            removed = true;

            // Case 1: no child or one child
            if (node.Left == null) return node.Right;
            if (node.Right == null) return node.Left;

            // Case 2: two children -> replace with inorder successor (min of right)
            TreeNode successor = FindMin(node.Right);
            node.FileData = successor.FileData;

            bool dummy = false;
            node.Right = DeleteNode(node.Right, successor.FileData, ref dummy);

            return node;
        }

        private TreeNode FindMin(TreeNode node)
        {
            while (node.Left != null) node = node.Left;
            return node;
        }

        private bool Exists(TreeNode? node, FileNode key)
        {
            while (node != null)
            {
                int cmp = CompareFileNodes(key, node.FileData);
                if (cmp < 0) node = node.Left;
                else if (cmp > 0) node = node.Right;
                else return true;
            }
            return false;
        }

        private int CompareFileNodes(FileNode a, FileNode b)
        {
            // Directories before files, then alphabetical (case-insensitive)
            if (a.Type != b.Type)
                return a.Type == FileType.Directory ? -1 : 1;

            return string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase);
        }

        // =========================================================
        // Display
        // =========================================================

        public void DisplayTree()
        {
            if (root == null) return;
            DisplayTreeEnhanced(root, "", true, true);
            DisplayTreeByLevels();
        }

        private void DisplayTreeEnhanced(TreeNode? node, string prefix, bool isLast, bool isRoot)
        {
            if (node == null) return;

            string connector = isRoot ? "* " : (isLast ? "└── " : "├── ");
            string nodeInfo = $"{node.FileData.Name}{(node.FileData.Type == FileType.Directory ? "/" : $" ({FormatSize(node.FileData.Size)})")}";

            Console.WriteLine(prefix + connector + nodeInfo);

            string childPrefix = prefix + (isRoot ? "" : (isLast ? "    " : "│   "));

            bool hasLeft = node.Left != null;
            bool hasRight = node.Right != null;
            if (hasRight)
            {
                DisplayTreeEnhanced(node.Right, childPrefix, !hasLeft, false);
            }
            if (hasLeft)
            {
                DisplayTreeEnhanced(node.Left, childPrefix, true, false);
            }
        }

        private void DisplayTreeByLevels()
        {
            if (root == null) return;
            var queue = new Queue<(TreeNode? node, int level)>();
            queue.Enqueue((root, 0));
            int currentLevel = 0;

            Console.WriteLine("\n(Level-order view)");
            Console.Write($"L{currentLevel}: ");

            while (queue.Count > 0)
            {
                var (node, level) = queue.Dequeue();

                if (level != currentLevel)
                {
                    currentLevel = level;
                    Console.WriteLine();
                    Console.Write($"L{currentLevel}: ");
                }

                if (node != null)
                {
                    var label = node.FileData.Type == FileType.Directory
                        ? $"{node.FileData.Name}/"
                        : $"{node.FileData.Name}({FormatSize(node.FileData.Size)})";
                    Console.Write(label + "  ");

                    queue.Enqueue((node.Left, level + 1));
                    queue.Enqueue((node.Right, level + 1));
                }
            }
            Console.WriteLine();
        }

        private string FormatSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes}B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024}KB";
            return $"{bytes / (1024 * 1024)}MB";
        }

        // =========================================================
        // Statistics
        // =========================================================

        public FileSystemStats GetStatistics()
        {
            var stats = new FileSystemStats
            {
                TotalOperations = operationCount,
                SessionDuration = DateTime.Now - sessionStart
            };

            if (root != null)
            {
                CalculateStats(root, stats);
                stats.MostCommonExtension = ComputeMostCommonExtension(root);
            }
            return stats;
        }

        private void CalculateStats(TreeNode? node, FileSystemStats stats)
        {
            if (node == null) return;

            var file = node.FileData;
            if (file.Type == FileType.File)
            {
                stats.TotalFiles++;
                stats.TotalSize += file.Size;

                if (file.Size > stats.LargestFileSize)
                {
                    stats.LargestFileSize = file.Size;
                    stats.LargestFile = file.Name;
                }
            }
            else
            {
                stats.TotalDirectories++;
            }

            CalculateStats(node.Left, stats);
            CalculateStats(node.Right, stats);
        }

        private string ComputeMostCommonExtension(TreeNode? node)
        {
            var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            void Walk(TreeNode? n)
            {
                if (n == null) return;
                Walk(n.Left);

                if (n.FileData.Type == FileType.File)
                {
                    var ext = n.FileData.Extension ?? "";
                    if (!string.IsNullOrEmpty(ext))
                    {
                        counts[ext] = counts.TryGetValue(ext, out int c) ? c + 1 : 1;
                    }
                }

                Walk(n.Right);
            }

            Walk(node);

            if (counts.Count == 0) return "";

            var best = counts
                .OrderByDescending(kv => kv.Value)
                .ThenBy(kv => kv.Key, StringComparer.OrdinalIgnoreCase)
                .First();

            return best.Key;
        }

        // =========================================================
        // Convenience
        // =========================================================

        public bool IsEmpty() => root == null;

        public void LoadSampleData()
        {
            var sampleDirs = new[]
            {
                "Documents", "Pictures", "Videos", "Music", "Downloads",
                "Projects", "Code", "Images", "Archive"
            };

            var sampleFiles = new[]
            {
                ("readme.txt", 2048L), ("config.json", 1024L), ("app.cs", 5120L),
                ("photo.jpg", 2048000L), ("song.mp3", 4096000L), ("video.mp4", 52428800L),
                ("document.pdf", 1048576L), ("presentation.pptx", 3145728L),
                ("spreadsheet.xlsx", 512000L), ("archive.zip", 10485760L)
            };

            try
            {
                foreach (var dir in sampleDirs.Take(6))
                    CreateDirectory(dir);

                foreach (var (fileName, size) in sampleFiles.Take(8))
                    CreateFile(fileName, size);
            }
            catch
            {
                // Keep demo resilient
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;

// ============================================
// 📚 QUICK REFERENCE GUIDE
// ============================================
/*
🌳 BINARY SEARCH TREE CHEAT SHEET:
BST Property:
- Left subtree values < Root value < Right subtree values
- This property enables O(log n) search performance!

Tree Construction:
var bst = new EmployeeManagementSystem();
bst.Insert(new Employee(50, "Root")); // Root node
bst.Insert(new Employee(30, "Left")); // Goes left (30 < 50)
bst.Insert(new Employee(70, "Right")); // Goes right (70 > 50)

Tree Search - O(log n):
var found = bst.Search(30); // Navigate tree efficiently

Tree Traversal:
bst.InOrderTraversal(); // Outputs: sorted sequence automatically!
 // Left -> Root -> Right = 20, 30, 40, 50, 60, 70, 80

Tree Analysis:
var minimum = bst.FindMinimum(); // Leftmost node
var maximum = bst.FindMaximum(); // Rightmost node
var count = bst.Count();         // Total nodes

🚀 WHY BINARY SEARCH TREES ROCK:
- O(log n) search vs O(n) linear search
- Automatic sorting through in-order traversal
- Perfect for dynamic data with frequent searches
- Natural hierarchical organization
- Foundation for advanced tree structures

🌐 REAL-WORLD USES:
- Database indexing systems
- File system organization
- Expression parsing in compilers
- Game engine spatial partitioning
- Symbol tables in programming languages
- Priority queues and scheduling systems
*/

namespace Lab9_BST
{
    /// <summary>
    /// Lab 9: Binary Search Tree - Employee Management System
    ///
    /// This lab demonstrates BST fundamentals through employee management scenarios!
    /// You'll build systems that work like real database indexing and search APIs.
    /// </summary>
    public class EmployeeManagementSystem
    {
        // 🌳 THE CORE: BST root for O(log n) operations and automatic organization
        private TreeNode? root;

        // 📊 System tracking (real systems do this too!)
        private int totalOperations = 0;
        private DateTime systemStartTime = DateTime.Now;

        public EmployeeManagementSystem()
        {
            root = null;
            Console.WriteLine("🏢 Employee Management System Initialized!");
            Console.WriteLine("📊 System ready for BST operations.\n");
        }

        // ============================================
        // 🚀 TODO METHODS — NOW IMPLEMENTED
        // ============================================

        /// <summary>
        /// TODO #1: Insert employee into BST maintaining ordering property
        /// </summary>
        public void Insert(Employee employee)
        {
            totalOperations++;
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            // Use recursive helper to link subtrees properly
            root = InsertRecursive(root, employee);
        }

        /// <summary>
        /// TODO #2: Search for employee by ID using BST navigation
        /// </summary>
        public Employee? Search(int employeeId)
        {
            totalOperations++;
            return SearchRecursive(root, employeeId);
        }

        /// <summary>
        /// TODO #3: Perform in-order traversal to display employees in sorted order
        /// </summary>
        public void InOrderTraversal()
        {
            totalOperations++;
            Console.WriteLine("👥 Employee Directory (sorted by ID):");
            if (root == null)
            {
                Console.WriteLine(" (No employees in system)");
                return;
            }
            InOrderRecursive(root);
        }

        /// <summary>
        /// TODO #4: Find employee with minimum ID (leftmost node)
        /// </summary>
        public Employee? FindMinimum()
        {
            totalOperations++;
            if (root == null) return null;

            var current = root;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current.Employee;
        }

        /// <summary>
        /// TODO #5: Find employee with maximum ID (rightmost node)
        /// </summary>
        public Employee? FindMaximum()
        {
            totalOperations++;
            if (root == null) return null;

            var current = root;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current.Employee;
        }

        /// <summary>
        /// TODO #6: Count total employees in system using recursion
        /// </summary>
        public int Count()
        {
            totalOperations++;
            return CountRecursive(root);
        }

        // ============================================
        // 🔧 HELPER METHODS FOR TODO IMPLEMENTATION
        // ============================================

        private TreeNode? InsertRecursive(TreeNode? node, Employee employee)
        {
            // Base case: empty spot -> create node
            if (node == null) return new TreeNode(employee);

            int key = employee.EmployeeId;
            int current = node.Employee.EmployeeId;

            if (key < current)
            {
                node.Left = InsertRecursive(node.Left, employee);
            }
            else if (key > current)
            {
                node.Right = InsertRecursive(node.Right, employee);
            }
            else
            {
                // Duplicate key policy: update existing employee record
                node.Employee = employee;
            }
            return node; // Return subtree root to preserve links
        }

        private Employee? SearchRecursive(TreeNode? node, int employeeId)
        {
            if (node == null) return null;

            int current = node.Employee.EmployeeId;
            if (employeeId == current) return node.Employee;

            if (employeeId < current)
                return SearchRecursive(node.Left, employeeId);
            else
                return SearchRecursive(node.Right, employeeId);
        }

        private void InOrderRecursive(TreeNode? node)
        {
            if (node == null) return;

            // Left -> Root -> Right
            InOrderRecursive(node.Left);

            var e = node.Employee;
            Console.WriteLine($" - ID:{e.EmployeeId}  Name:{e.Name}  Dept:{e.Department}");

            InOrderRecursive(node.Right);
        }

        private int CountRecursive(TreeNode? node)
        {
            if (node == null) return 0;
            return 1 + CountRecursive(node.Left) + CountRecursive(node.Right);
        }

        // ============================================
        // 🎯 UTILITY METHODS (PROVIDED)
        // ============================================

        public bool IsEmpty()
        {
            return root == null;
        }

        public void DisplayTree()
        {
            Console.WriteLine("🌳 Tree Structur Visualization:");
            if (root == null)
            {
                Console.WriteLine(" (Empty tree)");
                return;
            }

            Console.WriteLine("\n📊 Enhanced Tree Structure:");
            DisplayTreeEnhanced(root, "", true, true);

            Console.WriteLine("\n🎯 Level-by-Level View:");
            DisplayTreeByLevels();
        }

        private void DisplayTreeEnhanced(TreeNode? node, string prefix, bool isLast, bool isRoot)
        {
            if (node == null) return;

            string connector = isRoot ? "🌟 " : (isLast ? "└── " : "├── ");
            string nodeInfo = $"ID:{node.Employee.EmployeeId} ({node.Employee.Name})";
            Console.WriteLine(prefix + connector + nodeInfo);

            string childPrefix = prefix + (isRoot ? "" : (isLast ? "    " : "│   "));
            bool hasLeft = node.Left != null;
            bool hasRight = node.Right != null;

            if (hasRight)
            {
                Console.WriteLine(childPrefix + "│");
                Console.WriteLine(childPrefix + "├─(R)─┐");
                DisplayTreeEnhanced(node.Right, childPrefix + "│   ", !hasLeft, false);
            }

            if (hasLeft)
            {
                Console.WriteLine(childPrefix + "│");
                Console.WriteLine(childPrefix + "└─(L)─┐");
                DisplayTreeEnhanced(node.Left, childPrefix + "    ", true, false);
            }
        }

        private void DisplayTreeByLevels()
        {
            if (root == null) return;

            var queue = new Queue<(TreeNode?, int)>();
            queue.Enqueue((root, 0));
            int currentLevel = -1;

            while (queue.Count > 0)
            {
                var (node, level) = queue.Dequeue();

                if (level > currentLevel)
                {
                    if (currentLevel >= 0) Console.WriteLine();
                    Console.Write($"Level {level}: ");
                    currentLevel = level;
                }

                if (node != null)
                {
                    Console.Write($"[{node.Employee.EmployeeId}] ");
                    queue.Enqueue((node.Left, level + 1));
                    queue.Enqueue((node.Right, level + 1));
                }
                else
                {
                    Console.Write("[null] ");
                }
            }
            Console.WriteLine();
        }

        public SystemStats GetSystemStats()
        {
            var uptime = DateTime.Now - systemStartTime;
            return new SystemStats
            {
                TotalOperations = totalOperations,
                Uptime = uptime,
                EmployeeCount = IsEmpty() ? 0 : Count(), // Calls YOUR Count()
                TreeHeight = CalculateHeight(root),
                IsEmpty = IsEmpty()
            };
        }

        private int CalculateHeight(TreeNode? node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(CalculateHeight(node.Left), CalculateHeight(node.Right));
        }

        public void RunInteractiveMenu()
        {
            LabSupport.RunInteractiveMenu(this);
        }
    }
}

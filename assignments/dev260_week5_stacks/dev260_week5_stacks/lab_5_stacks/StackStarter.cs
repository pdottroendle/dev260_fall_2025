using System;
using System.Collections.Generic;
// PP TROENDLE BELLEVUE COLLEGE DEV 260 FALL 2025 SUPERVISOR ZACK
namespace StackLab
{
    class Program
    {
        static Stack<string> actionHistory = new Stack<string>();
        static Stack<string> undoHistory = new Stack<string>();
        static int totalOperations = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Interactive Stack Demo ===");
            Console.WriteLine("Building an action history system with undo/redo\n");

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";
                switch (choice)
                {
                    case "1":
                    case "push":
                        HandlePush();
                        break;
                    case "2":
                    case "pop":
                        HandlePop();
                        break;
                    case "3":
                    case "peek":
                    case "top":
                        HandlePeek();
                        break;
                    case "4":
                    case "display":
                        HandleDisplay();
                        break;
                    case "5":
                    case "clear":
                        HandleClear();
                        break;
                    case "6":
                    case "undo":
                        HandleUndo();
                        break;
                    case "7":
                    case "redo":
                        HandleRedo();
                        break;
                    case "8":
                    case "stats":
                        ShowStatistics();
                        break;
                    case "9":
                    case "exit":
                        running = false;
                        ShowSessionSummary();
                        break;
                    default:
                        Console.WriteLine("❌ Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("┌─ Stack Operations Menu ─────────────────────────┐");
            Console.WriteLine("│ 1. Push │ 2. Pop │ 3. Peek/Top │");
            Console.WriteLine("│ 4. Display │ 5. Clear │ 6. Undo │");
            Console.WriteLine("│ 7. Redo │ 8. Stats │ 9. Exit │");
            Console.WriteLine("└─────────────────────────────────────────────────┘");
            Console.WriteLine($"ℹ️ Stack Size: {actionHistory.Count} | Total Ops: {totalOperations}");
            Console.Write("\nChoose operation (number or name): ");
        }

        static void HandlePush()
        {
            Console.Write("Enter action to push: ");
            string input = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("❌ Action cannot be empty.\n");
                return;
            }

            actionHistory.Push(input);
            undoHistory.Clear();
            totalOperations++;
            Console.WriteLine($"✅ Pushed: {input}\n");
        }

        static void HandlePop()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("❌ Cannot pop - stack is empty.\n");
                return;
            }

            string popped = actionHistory.Pop();
            undoHistory.Push(popped);
            totalOperations++;
            Console.WriteLine($"✅ Popped: {popped}");
            if (actionHistory.Count > 0)
                Console.WriteLine($"👀 New top: {actionHistory.Peek()}\n");
            else
                Console.WriteLine("ℹ️ Stack is now empty.\n");
        }

        static void HandlePeek()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("👀 Stack is empty - nothing to peek.\n");
                return;
            }

            Console.WriteLine($"👀 Top action: {actionHistory.Peek()}\n");
        }

        static void HandleDisplay()
        {
            Console.WriteLine("📋 Current Stack Contents:");
            if (actionHistory.Count == 0)
            {
                Console.WriteLine(" (Stack is empty)\n");
                return;
            }

            int index = 1;
            foreach (var action in actionHistory)
            {
                string marker = index == 1 ? "🔝" : "   ";
                Console.WriteLine($"{marker} {index}. {action}");
                index++;
            }
            Console.WriteLine($"Total actions: {actionHistory.Count}\n");
        }

        static void HandleClear()
        {
            if (actionHistory.Count == 0 && undoHistory.Count == 0)
            {
                Console.WriteLine("ℹ️ Nothing to clear - both stacks are empty.\n");
                return;
            }

            int cleared = actionHistory.Count + undoHistory.Count;
            actionHistory.Clear();
            undoHistory.Clear();
            totalOperations++;
            Console.WriteLine($"✅ Cleared {cleared} items from history.\n");
        }

        static void HandleUndo()
        {
            if (undoHistory.Count == 0)
            {
                Console.WriteLine("ℹ️ Nothing to undo.\n");
                return;
            }

            string restored = undoHistory.Pop();
            actionHistory.Push(restored);
            totalOperations++;
            Console.WriteLine($"✅ Restored: {restored}\n");
        }

        static void HandleRedo()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("ℹ️ Nothing to redo.\n");
                return;
            }

            string redone = actionHistory.Pop();
            undoHistory.Push(redone);
            totalOperations++;
            Console.WriteLine($"✅ Redone: {redone}\n");
        }

        static void ShowStatistics()
        {
            Console.WriteLine("📊 Session Statistics:");
            Console.WriteLine($" Stack Size: {actionHistory.Count}");
            Console.WriteLine($" Undo Stack Size: {undoHistory.Count}");
            Console.WriteLine($" Total Operations: {totalOperations}");
            Console.WriteLine($" Stack Empty: {(actionHistory.Count == 0 ? "✅ Yes" : "❌ No")}");
            if (actionHistory.Count > 0)
                Console.WriteLine($" Top Action: {actionHistory.Peek()}");
            Console.WriteLine();
        }

        static void ShowSessionSummary()
        {
            Console.WriteLine("\n📋 Final Session Summary:");
            Console.WriteLine($" Total Operations: {totalOperations}");
            Console.WriteLine($" Final Stack Size: {actionHistory.Count}");
            if (actionHistory.Count > 0)
            {
                Console.WriteLine(" Remaining Actions:");
                foreach (var action in actionHistory)
                {
                    Console.WriteLine($" - {action}");
                }
            }
            else
            {
                Console.WriteLine(" No remaining actions.");
            }
            Console.WriteLine("\nThanks for using the Interactive Stack Demo!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
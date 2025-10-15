using System;

namespace Week3ArraysSorting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Week 3: Arrays & Sorting Assignment ===");
            Console.WriteLine("by Peter-Paul Troendle");            
			Console.WriteLine("DEV 260 Arrays  - Bellevue College - BAS Applications - Fall 2025");
            Console.WriteLine("Superviser: Zack");
            Console.WriteLine();

            bool keepRunning = true;

            while (keepRunning)
            {
                Console.Clear();
                DisplayMainMenu();

                Console.Write("Enter your choice (1-3): ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var game = new BoardGame();
                        game.StartGame();
                        break;

                    case "2":
                        var catalog = new BookCatalog();
                        catalog.LoadBooks("books.txt");
                        catalog.StartLookupSession();
                        break;

                    case "3":
                        keepRunning = false;
                        Console.WriteLine("Thanks for testing my Arrays & Sorting assignment!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("=== MAIN MENU ===");
            Console.WriteLine();
            Console.WriteLine("Choose an option:");
            Console.WriteLine();
            Console.WriteLine("1. Play Board Game (Part A)");
            Console.WriteLine("   → Experience multi-dimensional arrays with Tic-Tac-Toe");
            Console.WriteLine();
            Console.WriteLine("2. Book Catalog Lookup (Part B)");
            Console.WriteLine("   → Search through sorted book titles using recursive algorithms");
            Console.WriteLine();
            Console.WriteLine("3. Exit Program");
            Console.WriteLine();
        }
    }
}
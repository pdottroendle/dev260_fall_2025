
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Week 2 Foundations assignment: complexity predictions, demos, benchmarks, observations ===");
        Console.WriteLine("by Peter-Paul Troendle");
        Console.WriteLine("DEV 260 Arrays - Bellevue College - BAS Applications - Fall 2025");
        Console.WriteLine("Superviser: Zack");
        Console.WriteLine();

        RunArrayDemo();
        RunListDemo();
        RunStackDemo();
        RunQueueDemo();
        RunDictionaryDemo();
        RunHashSetDemo();
        RunBenchmarks();
    }

    static void RunBenchmarks()
    {
        Console.WriteLine("⏱️ Benchmarking Membership Checks");
        foreach (int N in new[] { 1000, 10000, 100000 })
        {
            Console.WriteLine($"\nN = {N}");
            var list = new List<int>();
            var set = new HashSet<int>();
            var dict = new Dictionary<int, bool>();
            for (int i = 0; i < N; i++)
            {
                list.Add(i);
                set.Add(i);
                dict[i] = true;
            }
            int[] targets = { N - 1, -1 };
            foreach (int target in targets)
            {
                var sw = Stopwatch.StartNew();
                list.Contains(target);
                sw.Stop();
                Console.WriteLine($"List.Contains({target}): {sw.Elapsed.TotalMilliseconds:F3} ms");
            }
            foreach (int target in targets)
            {
                var sw = Stopwatch.StartNew();
                set.Contains(target);
                sw.Stop();
                Console.WriteLine($"HashSet.Contains({target}): {sw.Elapsed.TotalMilliseconds:F3} ms");
            }
            foreach (int target in targets)
            {
                var sw = Stopwatch.StartNew();
                dict.ContainsKey(target);
                sw.Stop();
                Console.WriteLine($"Dict.ContainsKey({target}): {sw.Elapsed.TotalMilliseconds:F3} ms");
            }
        }
    }

    static void RunArrayDemo()
    {
        Console.WriteLine(" Array Demo");
        int[] numbers = new int[10];
        numbers[0] = 42;
        numbers[2] = 99;
        numbers[5] = 17;
        Console.WriteLine($"Value at index 2: {numbers[2]}");
        int target = 17;
        bool found = false;
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] == target)
            {
                found = true;
                break;
            }
        }
        Console.WriteLine($"Search for {target}: {(found ? "Found" : "Not Found")}");
        Console.WriteLine();
    }

    static void RunListDemo()
    {
        Console.WriteLine(" List<T> Demo");
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        numbers.Insert(2, 99);
        numbers.Remove(99);
        Console.WriteLine($"Final Count after insert and remove: {numbers.Count}");
        Console.WriteLine();
    }

    static void RunStackDemo()
    {
        Console.WriteLine(" Stack<T> Demo");
        Stack<string> pageHistory = new Stack<string>();
        pageHistory.Push("https://www.bbc.com/news/world");
        pageHistory.Push("https://www.bbc.com/sports");
        pageHistory.Push("https://www.bbc.com/");
        Console.WriteLine($"Current page (Peek): {pageHistory.Peek()}");
        Console.WriteLine("Navigating back through history:");
        while (pageHistory.Count > 0)
        {
            Console.WriteLine($"Visited: {pageHistory.Pop()}");
        }
        Console.WriteLine();
    }

    static void RunQueueDemo()
    {
        Console.WriteLine(" Queue<T> Demo");
        Queue<string> printJobs = new Queue<string>();
        printJobs.Enqueue("Job 1");
        printJobs.Enqueue("Job 2");
        printJobs.Enqueue("Job 3");
        Console.WriteLine($"Next job in queue (Peek): {printJobs.Peek()}");
        Console.WriteLine("Processing print jobs:");
        while (printJobs.Count > 0)
        {
            Console.WriteLine($"Processed: {printJobs.Dequeue()}");
        }
        Console.WriteLine();
    }

    static void RunDictionaryDemo()
    {
        Console.WriteLine(" Dictionary<TKey, TValue> Demo");
        Dictionary<string, int> inventory = new Dictionary<string, int>
        {
            { "HELLO", 10 },
            { "HI", 5 },
            { "BONJOUR", 20 }
        };
        inventory["HI THERE"] = 8;
        if (inventory.TryGetValue("N0", out int missingQty))
        {
            Console.WriteLine($"Found N0 with quantity: {missingQty}");
        }
        else
        {
            Console.WriteLine("N0 not found in inventory.");
        }
        Console.WriteLine();
    }

    static void RunHashSetDemo()
    {
        Console.WriteLine(" HashSet<T> Demo");
        HashSet<int> numbers = new HashSet<int>();
        bool added1 = numbers.Add(1);
        bool added2 = numbers.Add(2);
        bool addedDuplicate = numbers.Add(2);
        Console.WriteLine($"Added 1: {added1}");
        Console.WriteLine($"Added 2: {added2}");
        Console.WriteLine($"Tried adding duplicate 2: {addedDuplicate} (should be false)");
        HashSet<int> otherSet = new HashSet<int> { 3, 4, 5 };
        numbers.UnionWith(otherSet);
        Console.WriteLine($"Final Count after UnionWith: {numbers.Count}");
        Console.WriteLine();
    }
}

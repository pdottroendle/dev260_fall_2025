// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Sum using for loop: " + SumUsingFor());
        Console.WriteLine("Sum using while loop: " + SumUsingWhile());
        Console.WriteLine("Sum using foreach loop: " + SumUsingForeach());

        int sum = SumUsingFor();
        CheckSum(sum);

        Console.WriteLine("Grade (if/else) for 85: " + GetLetterGradeIfElse(85));
        Console.WriteLine("Grade (switch) for 85: " + GetLetterGradeSwitch(85));
    }

    static int SumUsingFor()
    {
        int sum = 0;
        for (int i = 1; i <= 100; i++)
            if (i % 2 == 0) sum += i;
        return sum;
    }

    static int SumUsingWhile()
    {
        int sum = 0, i = 1;
        while (i <= 100)
        {
            if (i % 2 == 0) sum += i;
            i++;
        }
        return sum;
    }

    static int SumUsingForeach()
    {
        int sum = 0;
        var numbers = Enumerable.Range(1, 100).ToList();
        foreach (var num in numbers)
            if (num % 2 == 0) sum += num;
        return sum;
    }

    static void CheckSum(int sum)
    {
        if (sum > 2000)
            Console.WriteLine("That’s a big number!");
        else
            Console.WriteLine("Sum is within range.");

        Console.WriteLine(sum > 2000 ? "That’s a big number!" : "Sum is within range.");
    }

    static string GetLetterGradeIfElse(int score)
    {
        if (score >= 90) return "A";
        else if (score >= 80) return "B";
        else if (score >= 70) return "C";
        else if (score >= 60) return "D";
        else return "F";
    }

    static string GetLetterGradeSwitch(int score) =>
        score switch
        {
            >= 90 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",
            _ => "F"
        };
}


using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // ==========================================
        // 1. Uncorrected FOR loop (Shared variable capture)
        // ==========================================
        List<Action> uncorrectedActions = new List<Action>();

        for (int i = 0; i < 3; i++)
        {
            // Captures the outer loop variable 'i' by reference. 
            // A single instance of 'i' exists across all iterations.
            uncorrectedActions.Add(() => Console.WriteLine($"Uncorrected: {i}"));
        }

        Console.WriteLine("--- Uncorrected For Loop Output ---");
        foreach (var action in uncorrectedActions)
        {
            action(); // Output: 3, 3, 3 (Because 'i' equals 3 when the loop terminates)
        }


        // 2. Corrected FOR loop (Local variable copy)
        List<Action> correctedActions = new List<Action>();

        for (int i = 0; i < 3; i++)
        {
            // Create a fresh local variable for each iteration
            int currentIndex = i;
            correctedActions.Add(() => Console.WriteLine($"Corrected: {currentIndex}"));
        }

        Console.WriteLine("\n--- Corrected For Loop Output ---");
        foreach (var action in correctedActions)
        {
            action(); // Output: 0, 1, 2 (Each lambda captures its own unique local variable)
        }


        // 3. FOREACH loop (Iteration-scoped variable)
        List<Action> foreachActions = new List<Action>();
        int[] numbers = { 0, 1, 2 };

        foreach (int num in numbers)
        {
            // In modern C#, foreach iteration variables are scoped inside the loop body.
            // A new variable 'num' is implicitly created per iteration.
            foreachActions.Add(() => Console.WriteLine($"Foreach: {num}"));
        }

        Console.WriteLine("\n--- Foreach Loop Output ---");
        foreach (var action in foreachActions)
        {
            action(); // Output: 0, 1, 2
        }
    }
}
using System;
using Microsoft.CSharp.RuntimeBinder;

namespace Lab1VarExplicitDynamic
{
    public class Program
    {
        public static void Main()
        {
            // 1. var vs Explicit Types vs dynamic
            var count = 10;
            int countExplicit = 10;
            dynamic countDynamic = 10;

            Console.WriteLine($"count (var): {count} | Type: {count.GetType()}");
            Console.WriteLine($"countExplicit (int): {countExplicit} | Type: {countExplicit.GetType()}");
            Console.WriteLine($"countDynamic (dynamic): {countDynamic} | Type: {countDynamic.GetType()}");
            Console.WriteLine();

            // 2. Dynamic Reassignment and Runtime Exception Handling
            countDynamic = "now text";
            Console.WriteLine($"countDynamic updated: \"{countDynamic}\" | Type: {countDynamic.GetType()}");

            try
            {
                // In C#, "string" + 5 results in string concatenation ("now text5").
                // Subtraction (-) is strictly arithmetic and triggers a RuntimeBinderException.
                var result = countDynamic - 5;
                Console.WriteLine($"Result: {result}");
            }
            catch (RuntimeBinderException ex)
            {
                Console.WriteLine($"Caught Runtime Exception: {ex.GetType().Name} -> {ex.Message}");
            }
            Console.WriteLine();

            // 3. Anonymous Types (Read-Only Properties)
            var point = new { X = 3, Y = 7 };
            Console.WriteLine($"Anonymous Point: X = {point.X}, Y = {point.Y}");

            // point.X = 10;
            // COMPILER ERROR (CS0200): Property or indexer '<anonymous type: int X, int Y>.X' cannot be assigned to -- it is read only.
        }
    }
}


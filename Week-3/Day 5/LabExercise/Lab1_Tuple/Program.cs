using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1Tuples
{
    public class Program
    {
        public static void Main()
        {
            // 2. Call GetStats and Deconstruct
            var data = new List<double> { 12.5, 4.0, 99.1, 45.3, 2.8 };

            var (avg, min, max) = GetStats(data);

            Console.WriteLine($"Stats -> Avg: {avg:F2}, Min: {min}, Max: {max}\n");

            // 3. TryParseAge Demo
            string[] testCases = { "25", "-5", "abc", "150", "" };

            Console.WriteLine("Age Validation Results:");
            foreach (var test in testCases)
            {
                var (success, errorMessage) = TryParseAge(test);
                Console.WriteLine($"Input: \"{test}\" -> Success: {success}, Error: {errorMessage ?? "None"}");
            }
            Console.WriteLine();

         
            var board = new Dictionary<(int Row, int Col), string>
            {
                [(0, 0)] = "X",
                [(1, 1)] = "O",
                [(0, 2)] = "X",
                [(2, 2)] = "O"
            };

            Console.WriteLine("Tic-Tac-Toe Board:");
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    // Look up cell with fallback to "-"
                    string cell = board.GetValueOrDefault((row, col), "-");
                    Console.Write($"{cell} ");
                }
                Console.WriteLine();
            }
        }

        // 1. GetStats returning a named ValueTuple

        public static (double Average, double Min, double Max) GetStats(IEnumerable<double> values)
        {
            if (values == null || !values.Any())
            {
                throw new ArgumentException("Collection must contain at least one element.", nameof(values));
            }

            double sum = 0;
            double min = double.MaxValue;
            double max = double.MinValue;
            int count = 0;

            foreach (var val in values)
            {
                sum += val;
                if (val < min) min = val;
                if (val > max) max = val;
                count++;
            }

            return (Average: sum / count, Min: min, Max: max);
        }

    
        // 3. TryParseAge Result Pattern
               public static (bool Success, string? ErrorMessage) TryParseAge(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return (false, "Age cannot be empty.");
            }

            if (!int.TryParse(input, out int age))
            {
                return (false, "Age must be a valid integer.");
            }

            if (age < 0 || age > 130)
            {
                return (false, "Age must be between 0 and 130.");
            }

            return (true, null);
        }
    }
}
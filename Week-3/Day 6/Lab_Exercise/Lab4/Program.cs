using System;
using System.Collections.Generic;
using System.Linq;

    public class Program
    {
        // 4. Repeat method accepting an Action delegate
        public static void Repeat(int times, Action action)
        {
            for (int i = 0; i < times; i++)
            {
                action();
            }
        }

        public static void Main()
        {
            // 1. Func<int, int, int> for Addition and Multiplication
            Console.WriteLine("=== 1. Func<int, int, int> ===");
            Func<int, int, int> add = (a, b) => a + b;
            Func<int, int, int> multiply = (a, b) => a * b;

            Console.WriteLine($"Add(5, 7):        {add(5, 7)}");
            Console.WriteLine($"Multiply(5, 7):   {multiply(5, 7)}");
            Console.WriteLine();

            // 2. Action<string> Logging with Timestamp Prefix
            Console.WriteLine("=== 2. Action<string> ===");
            Action<string> logWithTimestamp = message =>
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");

            logWithTimestamp("Application initialized successfully.");
            logWithTimestamp("Processing batch transaction.");
            Console.WriteLine();

            // 3. Predicate<int> for Prime Checking & List Filtering
            Console.WriteLine("=== 3. Predicate<int> (Primes from 1–50) ===");
            Predicate<int> isPrime = n =>
            {
                if (n <= 1) return false;
                if (n == 2) return true;
                if (n % 2 == 0) return false;

                for (int i = 3; i * i <= n; i += 2)
                {
                    if (n % i == 0) return false;
                }
                return true;
            };

            List<int> numbers = Enumerable.Range(1, 50).ToList();
            List<int> primes = numbers.FindAll(isPrime);

            Console.WriteLine($"Primes ({primes.Count}): {string.Join(", ", primes)}");
            Console.WriteLine();

            // 4. Repeat Execution via Action
            Console.WriteLine("=== 4. Repeat Action ===");
            Repeat(4, () => Console.WriteLine("Tick"));
        }
    }

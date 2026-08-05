using System;

class Program
{
    static ulong Factorial(ulong n)
    {
        // Base case
        if (n <= 1) return 1;

        return n * Factorial(n - 1);
    }

    static void Main()
    {
        ulong result = Factorial(5);
        Console.WriteLine($"Factorial of 5: {result}");
    }
}
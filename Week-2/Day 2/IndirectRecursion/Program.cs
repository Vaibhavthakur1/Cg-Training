using System;
using System.Reflection.Metadata.Ecma335;

class Program
{
    static void FunctionA(int n)
    {
        if (n <= 0) return;
        Console.WriteLine($"Function A: {n}");

        FunctionB(n - 1);
    }
    static void FunctionB(int n)
    {
        if (n <= 0) return;

        Console.WriteLine($"Function B: {n}");

        // Indirect recursive call back to Function A
        FunctionA(n - 1);
    }

    static void Main(string[] args)
    {
        FunctionA(5);
    }
}
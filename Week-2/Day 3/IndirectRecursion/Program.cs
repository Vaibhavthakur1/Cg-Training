using System;

class Program
{
    
    static void IsPositiveChain(int n)
    {
        if (n == 0)
        {
            return;
        }
        Console.WriteLine($"Positive Chain: {n}");
        //n = n - 1;
        //calling other function to show indirect recursion
        IsNegativeChain(n - 1);
    }
    static void IsNegativeChain(int n)
    {
        if (n == 0)
            return;

        Console.WriteLine($"Negative Chain: {n}");
        // recursive call to IsPositiveChain to show indirect recursion
        IsPositiveChain(n - 1);
    }


    static void Main(string[] args)
    {
        IsPositiveChain(5);

    }
}
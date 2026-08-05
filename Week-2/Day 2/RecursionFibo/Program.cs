using System;
class Program
{
        
    static int Fibonacci(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 1;

        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }
    static void Main(string[] args)
    {
       long result= Fibonacci(6);
        Console.WriteLine($"Fibonacci of 6 is: {result}");
    }
}

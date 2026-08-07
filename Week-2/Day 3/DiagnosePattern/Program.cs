using System;

class Program
{
    // Snippet 1
    static void Method1(int n)
    {
        if (n == 0) return;
        Method1(n - 1); // Recursive call before processing
        Console.Write(n + " ");
    }

    // Snippet 2
    static void Method2(int n)
    {
        if (n == 0) return;
        Console.Write(n + " ");
        Method2(n - 1); // Recursive call is the last statement
    }

    // Snippet 3
    static int Method3(int n)
    {
        if (n <= 1) return 1;
        return Method3(n - 1) + Method3(n - 2); // Two recursive calls
    }

    // Snippet 4
    static void Method4A(int n)
    {
        if (n <= 0) return;
        Method4B(n - 1); // Calls another recursive method
    }

    static void Method4B(int n)
    {
        if (n <= 0) return;
        Method4A(n - 1); // Calls back to Method4A
    }

    static void Main()
    {
        // Identify each recursion pattern and justify by the comment above the recursive call.
    }
}
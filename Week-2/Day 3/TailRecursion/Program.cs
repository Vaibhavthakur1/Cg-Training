using System;

class Program
{
    
    static int TailRecursion(int n,int acc)
    {
        if(n== 0)
        {
            return acc;
        }
        
        return TailRecursion( n-1, acc*n);


    }
    
    static void Main(string[] args)
    {
        int n = 5;
        Console.WriteLine($"factorial of {n}: "+TailRecursion(n, 1));
    }
}
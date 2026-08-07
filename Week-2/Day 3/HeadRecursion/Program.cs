using System;

class Program
{
    static void SumDigitsReversed(int n)
    {
        if (n == 0)
        {
            return;
        }

        SumDigitsReversed(n/10);
        Console.WriteLine(n%10);
    }
    static void Main(string[] args)
    {
        int number = 51232;
        SumDigitsReversed(number);

    }
}
using System;

class Program
{
    static int FibonacciSearch(int[] arr, int target)
    {
        int n = arr.Length;

        // Initialize Fibonacci numbers
        int fibMMm2 = 0;
        int fibMMm1 = 1;
        int fibM = fibMMm2 + fibMMm1;

        // Find the smallest Fibonacci number >= n
        while (fibM < n)
        {
            fibMMm2 = fibMMm1;
            fibMMm1 = fibM;
            fibM = fibMMm2 + fibMMm1;
        }

        int offset = -1;

        while (fibM > 1)
        {
            int i = Math.Min(offset + fibMMm2, n - 1);

            if (arr[i] < target)
            {
                // Search in the right part
                fibM = fibMMm1;
                fibMMm1 = fibMMm2;
                fibMMm2 = fibM - fibMMm1;
                offset = i;
            }
            else if (arr[i] > target)
            {
                // Search in the left part
                fibM = fibMMm2;
                fibMMm1 = fibMMm1 - fibMMm2;
                fibMMm2 = fibM - fibMMm1;
            }
            else
            {
                return i;
            }
        }

        // Check the last remaining element
        if (fibMMm1 == 1 && offset + 1 < n && arr[offset + 1] == target)
            return offset + 1;

        return -1;
    }

    static void Main()
    {
        int[] arr = { 10, 20, 30, 40, 50, 60, 70, 80, 90 };

        int target = 60;

        int index = FibonacciSearch(arr, target);

        if (index != -1)
            Console.WriteLine("Found at index " + index);
        else
            Console.WriteLine("Not Found");
    }
}
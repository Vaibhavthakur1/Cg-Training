using System;
using System.Diagnostics;

class Program
{
    static void HeapSort(int[] arr)
    {
        int n = arr.Length;

        // Build Max Heap
        for (int i = n / 2 - 1; i >= 0; i--)
        {
            Heapify(arr, n, i);
        }

        // Extract elements one by one
        for (int i = n - 1; i > 0; i--)
        {
            // Move current root to end
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;

            // Heapify the reduced heap
            Heapify(arr, i, 0);
        }
    }

    static void Heapify(int[] arr, int n, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        // Check left child
        if (left < n && arr[left] > arr[largest])
            largest = left;

        // Check right child
        if (right < n && arr[right] > arr[largest])
            largest = right;

        // If largest is not root, swap and continue heapifying
        if (largest != i)
        {
            int temp = arr[i];
            arr[i] = arr[largest];
            arr[largest] = temp;

            Heapify(arr, n, largest);
        }
    }

    static void Main()
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();
        int[] arr = { 12, 11, 13, 5, 6, 7 };

        HeapSort(arr);


        Console.WriteLine("Sorted Array:");
        foreach (int num in arr)
        {
            Console.Write(num + " ");
        }
        sw.Stop();

        Console.WriteLine("Execution Time: " + sw.ElapsedMilliseconds + " ms");
    }
}
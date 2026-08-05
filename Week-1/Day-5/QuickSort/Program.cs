using System;
using System.Collections.Concurrent;

class Program
{
    static void QuickSort(int[] arr,int low,int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high);

            QuickSort(arr, low, pivotIndex - 1);

            QuickSort(arr, pivotIndex + 1, high);
        }
        
    }

    static int Partition(int[] arr,int low,int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for(int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;

                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
        int t = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = t;

        return i + 1;

    }

    static void Main()
    {
        int[] arr = { 10, 7, 8, 4, 9, 1, 5 };

        Console.WriteLine("Sorted Array: ");
        QuickSort(arr, 0, arr.Length - 1);
        Console.WriteLine(string.Join(" ", arr));
    }
}
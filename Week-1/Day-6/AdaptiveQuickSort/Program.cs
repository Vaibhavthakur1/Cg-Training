using System;

class Program
{
    static void AdaptiveQuickSort(int[] arr, int low, int high)
    {
        // Use Insertion Sort for small partitions
        if (high - low + 1 <= 10)
        {
            InsertionSort(arr, low, high);
            return;
        }

        if (low < high)
        {
            int pivot = Partition(arr, low, high);

            AdaptiveQuickSort(arr, low, pivot - 1);
            AdaptiveQuickSort(arr, pivot + 1, high);
        }
    }

    static int Partition(int[] arr, int low, int high)
    {
        // Median-of-three pivot selection
        int mid = (low + high) / 2;

        if (arr[low] > arr[mid]) Swap(arr, low, mid);
        if (arr[low] > arr[high]) Swap(arr, low, high);
        if (arr[mid] > arr[high]) Swap(arr, mid, high);

        Swap(arr, mid, high);

        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                Swap(arr, i, j);
            }
        }

        Swap(arr, i + 1, high);
        return i + 1;
    }

    static void InsertionSort(int[] arr, int low, int high)
    {
        for (int i = low + 1; i <= high; i++)
        {
            int key = arr[i];
            int j = i - 1;

            while (j >= low && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }

            arr[j + 1] = key;
        }
    }

    static void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    static void Main()
    {
        int[] arr = { 10, 7, 8, 9, 1, 5, 2, 3, 6, 4 };

        AdaptiveQuickSort(arr, 0, arr.Length - 1);

        Console.WriteLine("Sorted Array:");
        foreach (int num in arr)
            Console.Write(num + " ");
    }
}
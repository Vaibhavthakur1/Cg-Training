using System;

class Program
{
    static void IntroSort(int[] arr)
    {
        int depthLimit = 2 * (int)Math.Log(arr.Length, 2);
        IntroSortUtil(arr, 0, arr.Length - 1, depthLimit);
    }

    static void IntroSortUtil(int[] arr, int low, int high, int depthLimit)
    {
        int size = high - low + 1;

        // Use Insertion Sort for small arrays
        if (size <= 16)
        {
            InsertionSort(arr, low, high);
            return;
        }

        // Switch to Heap Sort if recursion gets too deep
        if (depthLimit == 0)
        {
            HeapSort(arr, low, high);
            return;
        }

        // Otherwise continue with Quick Sort
        int pivot = Partition(arr, low, high);

        IntroSortUtil(arr, low, pivot - 1, depthLimit - 1);
        IntroSortUtil(arr, pivot + 1, high, depthLimit - 1);
    }

    static int Partition(int[] arr, int low, int high)
    {
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

    // Heap Sort for a subarray
    static void HeapSort(int[] arr, int low, int high)
    {
        int n = high - low + 1;

        // Build max heap
        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(arr, n, i, low);

        // Extract elements
        for (int i = n - 1; i > 0; i--)
        {
            Swap(arr, low, low + i);
            Heapify(arr, i, 0, low);
        }
    }

    static void Heapify(int[] arr, int n, int i, int offset)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < n && arr[offset + left] > arr[offset + largest])
            largest = left;

        if (right < n && arr[offset + right] > arr[offset + largest])
            largest = right;

        if (largest != i)
        {
            Swap(arr, offset + i, offset + largest);
            Heapify(arr, n, largest, offset);
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
        int[] arr = { 10, 7, 8, 9, 1, 5, 20, 15, 3, 2 };

        IntroSort(arr);

        Console.WriteLine("Sorted Array:");
        foreach (int num in arr)
            Console.Write(num + " ");
    }
}
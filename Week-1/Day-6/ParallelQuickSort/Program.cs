using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int[] arr = { 9, 4, 7, 2, 8, 1, 5, 3, 6 };

        ParallelQuickSort(arr, 0, arr.Length - 1);

        Console.WriteLine("Sorted Array:");
        foreach (int num in arr)
        {
            Console.Write(num + " ");
        }
    }

    static void ParallelQuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivot = Partition(arr, low, high);

            // For small arrays, avoid creating extra threads
            if (high - low < 1000)
            {
                ParallelQuickSort(arr, low, pivot - 1);
                ParallelQuickSort(arr, pivot + 1, high);
            }
            else
            {
                Parallel.Invoke(
                    () => ParallelQuickSort(arr, low, pivot - 1),
                    () => ParallelQuickSort(arr, pivot + 1, high)
                );
            }
        }
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

    static void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}
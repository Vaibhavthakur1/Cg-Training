using System;

class Program
{
    const int RUN = 32;

    static void InsertionSort(int[] arr, int left, int right)
    {
        for (int i = left + 1; i <= right; i++)
        {
            int temp = arr[i];
            int j = i - 1;

            while (j >= left && arr[j] > temp)
            {
                arr[j + 1] = arr[j];
                j--;
            }

            arr[j + 1] = temp;
        }
    }

    static void Merge(int[] arr, int l, int m, int r)
    {
        int len1 = m - l + 1;
        int len2 = r - m;

        int[] left = new int[len1];
        int[] right = new int[len2];

        for (int i = 0; i < len1; i++)
            left[i] = arr[l + i];

        for (int i = 0; i < len2; i++)
            right[i] = arr[m + 1 + i];

        int x = 0, y = 0, k = l;

        while (x < len1 && y < len2)
        {
            if (left[x] <= right[y])
                arr[k++] = left[x++];
            else
                arr[k++] = right[y++];
        }

        while (x < len1)
            arr[k++] = left[x++];

        while (y < len2)
            arr[k++] = right[y++];
    }

    static void TimSort(int[] arr)
    {
        int n = arr.Length;

        // Sort small blocks using Insertion Sort
        for (int i = 0; i < n; i += RUN)
        {
            InsertionSort(arr, i, Math.Min(i + RUN - 1, n - 1));
        }

        // Merge the sorted blocks
        for (int size = RUN; size < n; size *= 2)
        {
            for (int left = 0; left < n; left += 2 * size)
            {
                int mid = left + size - 1;
                int right = Math.Min(left + 2 * size - 1, n - 1);

                if (mid < right)
                    Merge(arr, left, mid, right);
            }
        }
    }

    static void Main()
    {
        int[] arr = { 5, 21, 7, 23, 19, 10, 3, 2 };

        TimSort(arr);

        Console.WriteLine("Sorted Array:");
        foreach (int num in arr)
            Console.Write(num + " ");
    }
}
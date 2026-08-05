using System;

class Program
{
    static void MergeSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;

           
            MergeSort(arr, left, mid);

            MergeSort(arr, mid + 1, right);
            Merge(arr, left, mid, right);
        }
    }

    static void Merge(int[] arr, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        int[] L = new int[n1];
        int[] R = new int[n2];

       
        for (int i = 0; i < n1; i++)
            L[i] = arr[left + i];

        for (int j = 0; j < n2; j++)
            R[j] = arr[mid + 1 + j];

        int x = 0, y = 0;
        int k = left;

        // Merge the temp arrays
        while (x < n1 && y < n2)
        {
            if (L[x] <= R[y])
            {
                arr[k] = L[x];
                x++;
            }
            else
            {
                arr[k] = R[y];
                y++;
            }
            k++;
        }

        // Copy remaining elements of L[]
        while (x < n1)
        {
            arr[k] = L[x];
            x++;
            k++;
        }

        // Copy remaining elements of R[]
        while (y < n2)
        {
            arr[k] = R[y];
            y++;
            k++;
        }
    }

    static void Main()
    {
        int[] arr = { 38, 27, 43, 3, 9, 82, 10 };

        MergeSort(arr, 0, arr.Length - 1);

        Console.WriteLine("Sorted Array:");
        Console.WriteLine(string.Join(" ", arr));
    }
}
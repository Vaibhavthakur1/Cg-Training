using System;

public class Program
{   
    public static void InsertionSort(int[] arr)
    {
        if (arr == null || arr.Length <= 1)
            return;

        for(int i = 1; i < arr.Length; i++)
        {
            int key = arr[i];
            int j = i - 1;

            while(j>=0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }
            arr[j + 1] = key;
        }

    }
    static void Main(string[] arg)
    { 
        int[] array1 = { 3, 1, 5, 2, 7, 6, 8, 9 };

        InsertionSort(array1);
        Console.WriteLine("Sorted Array: " + string.Join(",",array1));
    }
}
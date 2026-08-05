using System;
 public class Program
{

    static void BubbleSort(int[] arr)
    {
        for(int i = 0; i < arr.Length - 1; i++)
        {
            for(int j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        int[] array1 = { 4, 3, 8, 6, 9, 7, 5, 2, 1 };

        BubbleSort(array1);
        Console.WriteLine("Sorted Array: " + string.Join(",", array1));

    }
}
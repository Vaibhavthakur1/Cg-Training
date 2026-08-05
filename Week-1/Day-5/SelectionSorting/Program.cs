using System;
    

class Program
{   
    static void SelectionSort(int[] array1)
    {   
        for(int i = 0; i < array1.Length; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < array1.Length; j++)
            {
                if (array1[j] < array1[minIndex])
                {
                    minIndex = j;
                }

            }
            int temp = array1[i];
            array1[i] = array1[minIndex];
            array1[minIndex] = temp;

        }


    }
    static void Main(string[] args)
    {
        int[] arr = { 3, 4, 2, 7, 5, 9, 1 };

        SelectionSort(arr);
        Console.Write("Selection sort: ");
        foreach(int num in arr)
        {
            Console.Write(num + " ");
        }
    }
}
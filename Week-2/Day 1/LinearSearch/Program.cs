
using System;
class Program
{
    static int LinearSearch(int[] arr,int t)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == t)
            {
                return i;
            }
           
        }
        return -1;
    }
    static void Main(string[] args)
    {
        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        Console.WriteLine("Enter the number to search");
        int target = Convert.ToInt32(Console.ReadLine());

        int element=LinearSearch(array,target);

        if (element != -1)
        {
            Console.WriteLine($"Element found it index: {element}");
        }
        else
        {
            Console.WriteLine("Element not found");
        }


    }
}
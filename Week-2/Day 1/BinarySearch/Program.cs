using System;
class Program
{   
    static int BinarySearch(int[] arr,int target)
    {
        int low = 0;
        int high = arr.Length - 1;
        while (low <= high)
        {
            int mid = (low + high) / 2;
            if (arr[mid] == target)
            {
                return mid;
            }
            else if (target < arr[mid])
            {
                high = mid - 1;
            }
            else
            {
                low = mid + 1;
            }
        }

        return -1;
    }
    static void Main(string[] args)
    {
        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        Console.WriteLine("Enter the target Element to search");
        int target = Convert.ToInt32(Console.ReadLine());

        int index = BinarySearch(array, target);

        if (index != -1)
        {
            Console.WriteLine($"Element found at index {index}");
        }
        else
        {
            Console.WriteLine("Element not found");
        }

    }
}
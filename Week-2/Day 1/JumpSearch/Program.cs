using System;

class Program
{
    static int JumpSearch(int[] arr, int target)
    {
        int n = arr.Length;

        // Step 1: Find jump size
        int step = (int)Math.Sqrt(n);

        int prev = 0;

        // Step 2: Jump until target may be in current block
        while (prev < n && arr[Math.Min(step, n) - 1] < target)
        {
            prev = step;
            step += (int)Math.Sqrt(n);

            if (prev >= n)
                return -1;
        }

        // Step 3: Linear search inside the block
        while (prev < Math.Min(step, n))
        {
            if (arr[prev] == target)
                return prev;

            prev++;
        }

        return -1;
    }

    static void Main()
    {
        int[] arr = { 2, 5, 8, 12, 16, 23, 38, 56, 72, 91 };

        int target = 38;

        int index = JumpSearch(arr, target);

        if (index != -1)
            Console.WriteLine("Found at index " + index);
        else
            Console.WriteLine("Not Found");
    }
}
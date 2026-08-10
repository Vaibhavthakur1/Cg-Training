using System;
using System.Text;
using System.Diagnostics;

class Program
{
    static string BuildString(int count)
    {
        string result = "";
        for(int i = 0; i < count; i++)
        {
            result += i.ToString();
        }
        return result;
    }
    
    
    static string BuildStringBuilder(int count)
    {
        StringBuilder result = new StringBuilder(count * 5);

        for(int i=0;i<count; i++)
        {
            result.Append(i.ToString());
        }
        return result.ToString();
    }


    // Builds a string using List<string> and string.Join
    static string BuildWithList(int count)
    {
        // list to store each string separately
        List<string> parts = new List<string>(count);


        for (int i = 0; i < count; i++)
        {
            parts.Add(i.ToString());
        }

        // Join all strings together into one final string
        return string.Join("", parts);
    }



    static void RunBenchmark(int count)
    {
        Stopwatch stopwatch = new Stopwatch();

        // Start measuring normal string concatenation
        stopwatch.Start();
        string stringResult = BuildString(count);
        stopwatch.Stop();

        long stringTime = stopwatch.ElapsedMilliseconds;

        // Reset the stopwatch before measuring StringBuilder
        stopwatch.Reset();

        stopwatch.Start();
        string listResult = BuildWithList(count);
        stopwatch.Stop();

        long listTime = stopwatch.ElapsedMilliseconds;

        stopwatch.Reset();

        // Start measuring StringBuilder
        stopwatch.Start();
        string builderResult = BuildStringBuilder(count);
        stopwatch.Stop();

        long builderTime = stopwatch.ElapsedMilliseconds;
        double ratio = builderTime == 0
           ? 0
           : (double)stringTime / builderTime;

        // Print the results
        Console.WriteLine($"String concatenation ({count:N0} items): {stringTime} ms");
        Console.WriteLine($"StringBuilder ({count:N0} items): {builderTime} ms");
        Console.WriteLine($"List + string.Join ({count:N0} items): {listTime} ms");

        // Print the performance ratio
        if (builderTime > 0)
        {
            double stringRatio = (double)stringTime / builderTime;
            Console.WriteLine($"StringBuilder is roughly {stringRatio:F1}x faster than string");

        }
      if(builderTime>0)
        {
            double listRatio = (double)listTime / builderTime;
            Console.WriteLine($"List + Join is roughly {listRatio:F1}x the time of StringBuilder");
        }

        Console.WriteLine();

    }

    static void Main()
    {
        // Run the benchmark with 50,000 items
        RunBenchmark(50000);

        // Run the benchmark again with 200,000 items
        RunBenchmark(200000);
    }


}
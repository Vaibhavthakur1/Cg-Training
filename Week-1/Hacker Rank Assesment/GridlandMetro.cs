using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'gridlandMetro' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER m
     *  3. INTEGER k
     *  4. 2D_INTEGER_ARRAY track
     */

    public static long gridlandMetro(int n, int m, int k, List<List<int>> track)
    {
 Dictionary<int, List<(int start, int end)>> rows =
            new Dictionary<int, List<(int start, int end)>>();

        foreach (var t in track)
        {
            int row = t[0];
            int start = t[1];
            int end = t[2];

            if (!rows.ContainsKey(row))
            {
                rows[row] = new List<(int start, int end)>();
            }

            rows[row].Add((start, end));
        }

        long occupiedCells = 0;

        // Process every row containing tracks
        foreach (var row in rows)
        {
            // Sort tracks by starting column
            var tracks = row.Value.OrderBy(x => x.start).ToList();

            int currentStart = tracks[0].start;
            int currentEnd = tracks[0].end;

            for (int i = 1; i < tracks.Count; i++)
            {
                int nextStart = tracks[i].start;
                int nextEnd = tracks[i].end;

                // If tracks overlap or touch, merge them
                if (nextStart <= currentEnd + 1)
                {
                    currentEnd = Math.Max(currentEnd, nextEnd);
                }
                else
                {
                    // Add the current merged track
                    occupiedCells += (long)currentEnd - currentStart + 1;

                    // Start a new track
                    currentStart = nextStart;
                    currentEnd = nextEnd;
                }
            }

            // Add the last merged track
            occupiedCells += (long)currentEnd - currentStart + 1;
        }

        // Total cells in the grid
        long totalCells = (long)n * m;

        // Cells where we can build lamps
        return totalCells - occupiedCells;
    }
    }



class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int m = Convert.ToInt32(firstMultipleInput[1]);

        int k = Convert.ToInt32(firstMultipleInput[2]);

        List<List<int>> track = new List<List<int>>();

        for (int i = 0; i < k; i++)
        {
            track.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(trackTemp => Convert.ToInt32(trackTemp)).ToList());
        }

        long result = Result.gridlandMetro(n, m, k, track);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}

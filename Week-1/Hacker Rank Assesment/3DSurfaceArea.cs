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
     * Complete the 'surfaceArea' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts 2D_INTEGER_ARRAY A as parameter.
     */

    public static int surfaceArea(List<List<int>> A)
    {
        int rows =A.Count;
        int cols=A[0].Count;
        
        int area=0;
        for(int i=0;i<rows;i++){
            for(int j=0;j<cols;j++){
                int height=A[i][j];
                area+=2;
                    
                //left side
                 if (j == 0)
                    area += height;
                else
                    area += Math.Max(0, height - A[i][j - 1]);
                
                //right side
                if(j==cols-1)
                    area+=height;
                else    
                    area+=Math.Max(0,height-A[i][j+1]);
                    
                 // Top side
            if (i == 0)
                area += height;
            else
                area += Math.Max(0, height - A[i - 1][j]);
                    
            // Bottom side
            if (i == rows - 1)
                area += height;
            else
                area += Math.Max(0, height - A[i + 1][j]);
        }
    }

    return area;
                
                
            
    }  

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int H = Convert.ToInt32(firstMultipleInput[0]);

        int W = Convert.ToInt32(firstMultipleInput[1]);

        List<List<int>> A = new List<List<int>>();

        for (int i = 0; i < H; i++)
        {
            A.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(ATemp => Convert.ToInt32(ATemp)).ToList());
        }

        int result = Result.surfaceArea(A);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}

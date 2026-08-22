using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{

    static void Main(string[] args)
    {

        const int iterations = 2_000_000;
        Stopwatch sw = new Stopwatch();

        ArrayList ls = new ArrayList();
        sw.Start();
        ls.Add(10);
        ls.Add("Twenty");
        ls.Add(30.5);
        ls.Add(true);
        double sum = 0;
        foreach(var item in ls)
        {
            if (item is int or double)
            {
                sum += Convert.ToDouble(item);
            }
        }
        sw.Stop();
        long arrayListTime = sw.ElapsedMilliseconds;
        Console.WriteLine(arrayListTime);
        

        Console.WriteLine(sum);


        //compile time error
        //List<int> list = new List<int>
        //{
        //    10,"Twenty",30,true
        //};
        //foreach(var item in list)
        //{
        //    Console.Write(item);
        //}


    }
}
using System;

public struct Point
{
    public int X;
    public int Y;
    public Point(int x,int y)
    {
        X = x;
        Y = y;

    }
    public override string ToString()
    {
        return $"{X} {Y}";
    }
}
class Program
{
    static void Main(string[] args)
    {
        Point a = new Point(22, 55);
        Point b = a;
        b.X = 99;
        Console.WriteLine(a);
        Console.WriteLine(b);
    }
}
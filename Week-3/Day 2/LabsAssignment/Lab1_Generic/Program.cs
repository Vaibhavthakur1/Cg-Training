using System;
using System.Collections.Generic;



public class Box<T>
{
    private T _value;
    public Box(T value)
    {
        _value = value;
    }

    public T GetValue()
    {
        return _value;
    }

    void Replace(T newValue)
    {
        _value = newValue;

    }
    public static Box<T2> CreateEmpty<T2>() where T2 : new()
    {
        return new Box<T2>(new T2());
    }

}
public class Pair<TFirst, TSecond>
{
    public TFirst First{ get; set; }
    public TSecond Second{ get; set; }

    public Pair(TFirst _first,TSecond _second){
      First=_first;
        Second = _second;
    }

    public override string ToString()
    {
        return $"Pair: ({First}, {Second})";
    }

}
public class SortedBox<T>where T : IComparable<T>
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
        items.Sort();
    }

    public IEnumerable<T> Items
    {
        get
        {
            return items;
        }
    }

}

class Program
{
    static void Main(string[] args)
    {
        var number = new Box<int>(10);
        Console.WriteLine($"Box<int>: {number.GetValue()}");
        Box<string> name = new Box<string>("Vaibhav");
        Console.WriteLine($"Box<string>: {name.GetValue()}");

        Box<DateTime> dateBox =
            new Box<DateTime>(new DateTime(2026, 8, 12));


        var time = new Box<DateTime>(new DateTime(2001, 9, 24));
        Console.WriteLine($"Box<DateTime>: {time.GetValue()}");


        Pair<int, string> pair = new Pair<int, string>(24, "Age");
        Console.WriteLine(pair);


        SortedBox<int> list = new SortedBox<int>();
        list.Add(10);
        list.Add(6);
        list.Add(23);
        list.Add(5);
        list.Add(17);
        list.Add(9);
        Console.Write("Sorted Box after adding: ");
        foreach(var item in list.Items)
        {
            Console.Write(item+",");
        }

        Console.WriteLine();

    }
}
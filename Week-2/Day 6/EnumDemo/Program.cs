using System;
public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered
}

class Program
{
    static void Main(string[] args)
    {
        OrderStatus status = OrderStatus.Shipped;

        Console.WriteLine(status);
        Console.WriteLine((int)status);
        Console.WriteLine(Enum.GetName(typeof(OrderStatus),2));
        
    }
}
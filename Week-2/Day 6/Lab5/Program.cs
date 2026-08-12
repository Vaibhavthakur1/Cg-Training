using System;

public class Formatter
{
    // Method overloading: same method name, different parameters

    public string Format(int value)
    {
        return value.ToString();
    }

    public string Format(double value)
    {
        return value.ToString("F2");
    }

    public string Format(int numerator, int denominator)
    {
        return $"{numerator}/{denominator}";
    }
}


public class Notifier
{
    // Virtual method - can be overridden
    public virtual void Send()
    {
        Console.WriteLine("Notifier: generic send");
    }

    // Non-virtual method
    public void Log()
    {
        Console.WriteLine("Notifier: generic log");
    }
}


public class EmailNotifier : Notifier
{
    // Override the virtual method
    public override void Send()
    {
        Console.WriteLine("EmailNotifier: sending email");
    }

    // Hide the parent Log() method
    public new void Log()
    {
        Console.WriteLine("EmailNotifier: logging to email log");
    }
}


public struct Vector2
{
    public double X, Y;

    public Vector2(double x, double y)
    {
        X = x;
        Y = y;
    }

    // Operator +
    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X + b.X, a.Y + b.Y);
    }

    // Operator * for scalar multiplication
    public static Vector2 operator *(Vector2 vector, double scalar)
    {
        return new Vector2(
            vector.X * scalar,
            vector.Y * scalar
        );
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}


public class Program
{
    public static void Main()
    {
      

        Formatter formatter = new Formatter();

        Console.WriteLine(
            $"Format(7) -> \"{formatter.Format(7)}\""
        );

        Console.WriteLine(
            $"Format(3.5) -> \"{formatter.Format(3.5)}\""
        );

        Console.WriteLine(
            $"Format(3, 4) -> \"{formatter.Format(3, 4)}\""
        );


       

        EmailNotifier email = new EmailNotifier();

        Console.WriteLine();
        Console.WriteLine("-- through EmailNotifier variable --");

        email.Send();
        email.Log();


       
        Notifier notifier = email;

        Console.WriteLine();
        Console.WriteLine("-- through Notifier variable, same object --");

        notifier.Send();
        notifier.Log();


       

        Vector2 v1 = new Vector2(1, 2);
        Vector2 v2 = new Vector2(3, 4);

        Vector2 sum = v1 + v2;

        Console.WriteLine();
        Console.WriteLine($"{v1} + {v2} = {sum}");


        Vector2 v3 = new Vector2(2, 2);

        Vector2 scaled = v3 * 3;

        Console.WriteLine($"{v3} * 3 = {scaled}");
    }
}
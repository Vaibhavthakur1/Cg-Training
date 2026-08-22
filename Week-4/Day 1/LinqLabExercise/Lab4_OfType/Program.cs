using System;
using System.Collections.Generic;
using System.Linq;

// Product class from Lab 3
public class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }

    public Product(string name, string category, decimal price, bool inStock)
    {
        Name = name;
        Category = category;
        Price = price;
        InStock = inStock;
    }
}

// Shape hierarchy
public abstract class Shape { }

public class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }
}

public class Program
{
    public static void Main()
    {
        // Task 1: Extract subsets from a mixed List<object>
        var mixedList = new List<object>
        {
            10,
            "Keyboard",
            3.14,
            new Product("Mouse", "Electronics", 499m, true),
            42,
            "Monitor",
            99.99,
            new Product("Desk Lamp", "Furniture", 850m, false)
        };

        var integers = mixedList.OfType<int>();
        var strings = mixedList.OfType<string>();
        var products = mixedList.OfType<Product>();

        Console.WriteLine($"Integers: {string.Join(", ", integers)}");
        Console.WriteLine($"Strings: {string.Join(", ", strings)}");
        Console.WriteLine($"Products: {string.Join(", ", products.Select(p => p.Name))}\n");

        // Task 2: Shape hierarchy & area computations
        var shapes = new List<Shape>
        {
            new Circle(5.0),
            new Rectangle(4.0, 6.0),
            new Circle(2.5),
            new Rectangle(10.0, 2.0)
        };

        double totalCircleArea = shapes
            .OfType<Circle>()
            .Sum(c => Math.PI * c.Radius * c.Radius);

        double totalRectangleArea = shapes
            .OfType<Rectangle>()
            .Sum(r => r.Width * r.Height);

        Console.WriteLine($"Total Circle Area: {totalCircleArea:F2}");
        Console.WriteLine($"Total Rectangle Area: {totalRectangleArea:F2}\n");

        // Task 3: OfType<T> vs Cast<T>

        // OfType<Rectangle> silently skips non-matching types (Circle)
        var ofTypeRectangles = shapes.OfType<Rectangle>().ToList();
        Console.WriteLine($"OfType<Rectangle> found {ofTypeRectangles.Count} rectangles safely.");

        // Cast<Rectangle> attempts an explicit cast on every item and throws on mismatch
        try
        {
            var castRectangles = shapes.Cast<Rectangle>().ToList();
        }
        catch (InvalidCastException ex)
        {
            Console.WriteLine($"Cast<Rectangle>() threw expected exception: {ex.Message}");
        }
    }
}
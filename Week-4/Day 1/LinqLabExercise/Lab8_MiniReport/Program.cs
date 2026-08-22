using System;
using System.Collections.Generic;
using System.Linq;

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

public class CategorySummary
{
    public string Category { get; set; }
    public int ItemCount { get; set; }
    public decimal TotalValue { get; set; }
    public string TopProduct { get; set; }

    public override string ToString() =>
        $"Category    : {Category}\n" +
        $"  Item Count : {ItemCount}\n" +
        $"  Total Value: Rs.{TotalValue:F2}\n" +
        $"  Top Product: {TopProduct}";
}

public class Program
{
    public static void Main()
    {
        var products = new List<Product>
        {
            new Product("Notebook", "Stationery", 120m, true),
            new Product("Pen Set", "Stationery", 450m, false), // Out of stock
            new Product("Sticky Notes", "Stationery", 80m, true),
            new Product("Highlighter", "Stationery", 60m, true),
            new Product("Wireless Mouse", "Electronics", 799m, true),
            new Product("USB-C Cable", "Electronics", 299m, true),
            new Product("Mechanical Keyboard", "Electronics", 2499m, false), // Out of stock
            new Product("4K Monitor", "Electronics", 21999m, true),
            new Product("Desk Mat", "Accessories", 350m, true),
            new Product("Wrist Rest", "Accessories", 250m, false), // Out of stock
            new Product("Laptop Stand", "Accessories", 1200m, true)
        };

        // -------------------------------------------------------------
        // Approach A: Method Syntax
        // -------------------------------------------------------------
        var methodSyntaxReport = products
            .Where(p => p.InStock)
            .GroupBy(p => p.Category)
            .Select(g => new
            {
                Group = g,
                OrderedProducts = g.OrderByDescending(p => p.Price),
                Total = g.Sum(p => p.Price)
            })
            .OrderByDescending(x => x.Total)
            .Select(x => new CategorySummary
            {
                Category = x.Group.Key,
                ItemCount = x.Group.Count(),
                TotalValue = x.Total,
                TopProduct = x.OrderedProducts.First().Name
            })
            .ToList();

        // -------------------------------------------------------------
        // Approach B: Query Syntax
        // -------------------------------------------------------------
        var querySyntaxReport = (from p in products
                                 where p.InStock
                                 group p by p.Category into catGroup
                                 let totalValue = catGroup.Sum(item => item.Price)
                                 let sortedItems = from item in catGroup
                                                   orderby item.Price descending
                                                   select item
                                 orderby totalValue descending
                                 select new CategorySummary
                                 {
                                     Category = catGroup.Key,
                                     ItemCount = catGroup.Count(),
                                     TotalValue = totalValue,
                                     TopProduct = sortedItems.First().Name
                                 }).ToList();

        // -------------------------------------------------------------
        // Verification & Output
        // -------------------------------------------------------------
        Console.WriteLine("==================================================");
        Console.WriteLine("METHOD SYNTAX REPORT (In-Stock Categories by Value)");
        Console.WriteLine("==================================================");
        foreach (var summary in methodSyntaxReport)
        {
            Console.WriteLine(summary);
            Console.WriteLine(new string('-', 50));
        }

        Console.WriteLine("\n==================================================");
        Console.WriteLine("QUERY SYNTAX REPORT (In-Stock Categories by Value)");
        Console.WriteLine("==================================================");
        foreach (var summary in querySyntaxReport)
        {
            Console.WriteLine(summary);
            Console.WriteLine(new string('-', 50));
        }

        // Compare results across both implementations
        bool reportsMatch = methodSyntaxReport.Zip(querySyntaxReport, (m, q) =>
            m.Category == q.Category &&
            m.ItemCount == q.ItemCount &&
            m.TotalValue == q.TotalValue &&
            m.TopProduct == q.TopProduct
        ).All(isEqual => isEqual);

        Console.WriteLine($"\nBoth reports produce identical results: {reportsMatch}");
    }
}
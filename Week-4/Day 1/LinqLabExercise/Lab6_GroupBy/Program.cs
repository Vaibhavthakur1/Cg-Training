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

public class Program
{
    public static void Main()
    {
        var products = new List<Product>
        {
            new Product("Notebook", "Stationery", 120m, true),
            new Product("Pen Set", "Stationery", 450m, false),
            new Product("Sticky Notes", "Stationery", 80m, true),
            new Product("Highlighter", "Stationery", 60m, true),
            new Product("Wireless Mouse", "Electronics", 799m, true),
            new Product("USB-C Cable", "Electronics", 299m, true),
            new Product("Mechanical Keyboard", "Electronics", 2499m, false),
            new Product("Monitor", "Electronics", 12999m, true),
            new Product("Desk Mat", "Accessories", 350m, true),
            new Product("Wrist Rest", "Accessories", 250m, false)
        };

        // Task 1: Basic GroupBy (Category -> Count)
        Console.WriteLine("--- 1. Product Count by Category ---");
        var basicGroups = products.GroupBy(p => p.Category);

        foreach (var group in basicGroups)
        {
            Console.WriteLine($"Category: {group.Key} | Count: {group.Count()}");
        }
        Console.WriteLine();

        // Task 2: Query Syntax with 'into' (Filter count >= 3, Order by Sum DESC)
        Console.WriteLine("--- 2. Filtered & Sorted Groups (into) ---");
        var filteredGroups = from p in products
                             group p by p.Category into catGroup
                             where catGroup.Count() >= 3
                             orderby catGroup.Sum(p => p.Price) descending
                             select catGroup;

        foreach (var group in filteredGroups)
        {
            Console.WriteLine($"Category: {group.Key,-12} | Items: {group.Count()} | Total Value: Rs.{group.Sum(p => p.Price),9:F2}");
        }
        Console.WriteLine();

        // Task 3: Chained Group Aggregations (Count, Total, Avg, Max Item)
        Console.WriteLine("--- 3. Detailed Category Metrics ---");
        var categoryMetrics = products
            .GroupBy(p => p.Category)
            .Select(g => new
            {
                CategoryName = g.Key,
                Count = g.Count(),
                TotalValue = g.Sum(p => p.Price),
                AveragePrice = g.Average(p => p.Price),
                MostExpensive = g.OrderByDescending(p => p.Price).First().Name
            });

        foreach (var metric in categoryMetrics)
        {
            Console.WriteLine($"Category: {metric.CategoryName}");
            Console.WriteLine($"  - Total Items   : {metric.Count}");
            Console.WriteLine($"  - Total Value   : Rs.{metric.TotalValue:F2}");
            Console.WriteLine($"  - Average Price : Rs.{metric.AveragePrice:F2}");
            Console.WriteLine($"  - Top Product   : {metric.MostExpensive}");
        }
        Console.WriteLine();

        // Task 4: Composite Key Grouping (Category, InStock)
        Console.WriteLine("--- 4. Composite Key Grouping (Category, InStock) ---");
        var compositeGroups = products
            .GroupBy(p => new { p.Category, p.InStock });

        foreach (var group in compositeGroups)
        {
            string stockStatus = group.Key.InStock ? "In Stock" : "Out of Stock";
            Console.WriteLine($"Key: [Category: {group.Key.Category,-12}, Status: {stockStatus,-12}] -> Count: {group.Count()}");
        }
    }
}
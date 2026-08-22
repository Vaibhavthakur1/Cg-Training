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

    public override string ToString() =>
        $"[{Category,-12}] {Name,-20} | Rs.{Price,7:F2} | InStock: {InStock}";
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
            new Product("Wireless Mouse", "Electronics", 799m, true),
            new Product("USB-C Cable", "Electronics", 299m, true),
            new Product("Mechanical Keyboard", "Electronics", 2499m, false),
            new Product("Desk Mat", "Accessories", 350m, true),
            new Product("Wrist Rest", "Accessories", 250m, false)
        };

        // ---------------------------------------------------------------------
        // 1 & 2: The Buggy Multi-Key Sort (.OrderBy().OrderBy())
        // ---------------------------------------------------------------------
        // WHY THIS FAILS:
        // Calling .OrderBy() a second time completely resets and overwrites the 
        // previous ordering logic. The second .OrderBy() initiates an entirely new 
        // primary sort by Price. While LINQ's OrderBy is a stable sort (it preserves 
        // original order for elements with equal prices), it does NOT group by Category; 
        // it sorts the entire sequence strictly by Price.
        var buggySort = products
            .OrderBy(p => p.Category)
            .OrderBy(p => p.Price)
            .ToList();

        // ---------------------------------------------------------------------
        // 3: The Fixed Multi-Key Sort (.OrderBy() + .ThenByDescending())
        // ---------------------------------------------------------------------
        // OrderBy returns an IOrderedEnumerable<T>. Calling .ThenBy() or 
        // .ThenByDescending() registers a secondary/tie-breaking comparer 
        // without discarding the primary sorting key.
        var fixedSort = products
            .OrderBy(p => p.Category)
            .ThenByDescending(p => p.Price)
            .ToList();

        // Display Side-by-Side Comparison
        Console.WriteLine("=========================================================================================");
        Console.WriteLine("BUGGY SORT (.OrderBy -> .OrderBy)      | FIXED SORT (.OrderBy -> .ThenByDescending)   ");
        Console.WriteLine("=========================================================================================");
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{buggySort[i]} | {fixedSort[i]}");
        }
        Console.WriteLine();

        // 4: 3-Key Sort (InStock first -> Category ASC -> Name ASC)
        var threeKeySort = products
            .OrderByDescending(p => p.InStock)
            .ThenBy(p => p.Category)
            .ThenBy(p => p.Name)
            .ToList();

        Console.WriteLine("=========================================================================================");
        Console.WriteLine("3-KEY SORT (InStock First -> Category ASC -> Name ASC)");
        Console.WriteLine("=========================================================================================");
        foreach (var p in threeKeySort)
        {
            Console.WriteLine(p);
        }
    }
}
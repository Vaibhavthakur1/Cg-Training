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
            new Product("Wireless Mouse", "Electronics", 799m, true),
            new Product("Desk Mat", "Accessories", 350m, true),
            new Product("USB-C Cable", "Electronics", 299m, true),
            new Product("Mechanical Keyboard", "Electronics", 2499m, true)
        };

        // 1. Filter products under Rs. 500
        var under500 = products.Where(p => p.Price < 500m);

        // 2. Filter products that are BOTH in a specific category AND in stock
        var inStockElectronics = products.Where(p => p.Category == "Electronics" && p.InStock);

        // 3. Index-aware Where overload (even index positions: 0, 2, 4, ...)
        var evenPositionProducts = products.Where((p, index) => index % 2 == 0);

        // 4. Chained .Where() vs Single .Where() with &&
        var chainedWhere = products
            .Where(p => p.Price < 500m)
            .Where(p => p.InStock);

        var singleWhere = products
            .Where(p => p.Price < 500m && p.InStock);

        // Verify equivalence
        bool areIdentical = chainedWhere.SequenceEqual(singleWhere);
        Console.WriteLine($"Both approaches yield identical results: {areIdentical}");
    }
}
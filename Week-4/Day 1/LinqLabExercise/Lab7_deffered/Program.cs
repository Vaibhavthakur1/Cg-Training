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
        // Task 1: Deferred Execution (Picks up modifications)
        Console.WriteLine("Task 1: Deferred Execution");

        var products1 = new List<Product>
        {
            new Product("Notebook", "Stationery", 120m, true),
            new Product("Wireless Mouse", "Electronics", 799m, true)
        };

        // Query definition only - NO execution happens here
        var cheapProductsQuery = products1.Where(p => p.Price < 500m);
        Console.WriteLine("Query built: Filter products under Rs. 500");

        // Mutate source list AFTER query definition
        products1.Add(new Product("Gel Pen", "Stationery", 25m, true));
        Console.WriteLine("Added 'Gel Pen' (Rs. 25) to the source list.");

        // Enumeration triggers evaluation
        Console.WriteLine("Enumerating query now:");
        foreach (var p in cheapProductsQuery)
        {
            Console.WriteLine($" -> {p.Name} (Rs. {p.Price})");
        }
        Console.WriteLine();

        // Task 2: Immediate Execution (Materialized Snapshot)
        Console.WriteLine("==================================================");
        Console.WriteLine("Task 2: Immediate Execution (.ToList())");
        Console.WriteLine("==================================================");

        var products2 = new List<Product>
        {
            new Product("Notebook", "Stationery", 120m, true),
            new Product("Wireless Mouse", "Electronics", 799m, true)
        };

        // Immediate materialization into a separate List<Product>
        var cheapProductsSnapshot = products2.Where(p => p.Price < 500m).ToList();
        Console.WriteLine("Query built & materialized with .ToList()");

        // Mutate source list AFTER snapshot is created
        products2.Add(new Product("Sticky Notes", "Stationery", 50m, true));
        Console.WriteLine("Added 'Sticky Notes' (Rs. 50) to the source list.");

        // Enumerating the cached snapshot
        Console.WriteLine("Enumerating snapshot list:");
        foreach (var p in cheapProductsSnapshot)
        {
            Console.WriteLine($" -> {p.Name} (Rs. {p.Price})");
        }
        Console.WriteLine();

        // Task 3: Repeated Predicate Execution vs. Single Materialization
        Console.WriteLine("==================================================");
        Console.WriteLine("Task 3A: Deferred query enumerated TWICE (Side-Effects Re-run)");
        Console.WriteLine("==================================================");

        var products3 = new List<Product>
        {
            new Product("Notebook", "Stationery", 120m, true),
            new Product("Monitor", "Electronics", 12999m, true),
            new Product("Desk Mat", "Accessories", 350m, true)
        };

        // Side-effect inside the predicate simulates an expensive evaluation
        var deferredFilter = products3.Where(p =>
        {
            Console.WriteLine($"  [Evaluating Filter for: {p.Name}]");
            return p.Price < 500m;
        });

        Console.WriteLine("--> Running Loop 1 (Deferred):");
        foreach (var p in deferredFilter)
        {
            Console.WriteLine($"Matched: {p.Name}");
        }

        Console.WriteLine("--> Running Loop 2 (Deferred):");
        foreach (var p in deferredFilter)
        {
            Console.WriteLine($"Matched: {p.Name}");
        }
        Console.WriteLine();

        Console.WriteLine("==================================================");
        Console.WriteLine("Task 3B: Fixed with single .ToList() materialization");
        Console.WriteLine("==================================================");

        // Materialize once; the predicate executes only during this call
        Console.WriteLine("--> Materializing with .ToList()...");
        var cachedFilter = products3.Where(p =>
        {
            Console.WriteLine($"  [Evaluating Filter for: {p.Name}]");
            return p.Price < 500m;
        }).ToList();

        Console.WriteLine("--> Running Loop 1 (Cached):");
        foreach (var p in cachedFilter)
        {
            Console.WriteLine($"Matched: {p.Name}");
        }

        Console.WriteLine("--> Running Loop 2 (Cached):");
        foreach (var p in cachedFilter)
        {
            Console.WriteLine($"Matched: {p.Name}");
        }
    }
}
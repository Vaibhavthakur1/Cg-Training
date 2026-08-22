using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    class Program
    {
        static void Main()
        {
            var products = new List<Product>
        {
            new Product { Id = 1,  Name = "Wireless Mouse",     Category = "Electronics", Price = 650m,   InStock = true },
            new Product { Id = 2,  Name = "Mechanical Keyboard", Category = "Electronics", Price = 2400m,  InStock = true },
            new Product { Id = 3,  Name = "USB-C Cable",        Category = "Electronics", Price = 299m,   InStock = false },
            new Product { Id = 4,  Name = "Notebook A5",        Category = "Stationery",  Price = 150m,   InStock = true },
            new Product { Id = 5,  Name = "Fountain Pen",       Category = "Stationery",  Price = 850m,   InStock = true },
            new Product { Id = 6,  Name = "Desk Organizer",     Category = "Stationery",  Price = 1200m,  InStock = false },
            new Product { Id = 7,  Name = "Ceramic Mug",        Category = "Home",        Price = 450m,   InStock = true },
            new Product { Id = 8,  Name = "Water Bottle 1L",    Category = "Home",        Price = 750m,   InStock = true },
            new Product { Id = 9,  Name = "LED Desk Lamp",      Category = "Home",        Price = 1800m,  InStock = true },
            new Product { Id = 10, Name = "Espresso Beans 500g",Category = "Grocery",     Price = 950m,   InStock = true },
            new Product { Id = 11, Name = "Green Tea Box",      Category = "Grocery",     Price = 320m,   InStock = false },
            new Product { Id = 12, Name = "Olive Oil 1L",       Category = "Grocery",     Price = 1400m,  InStock = true }
        };

            // (a) Fully in method syntax
            var queryA = products
                .Where(p => p.Price < 1000m)
                .OrderBy(p => p.Name)
                .ToList();

            // (b) Fully in query syntax
            var queryB = (from p in products
                          where p.Price < 1000m
                          orderby p.Name
                          select p).ToList();

            // (c) Query syntax for 'where', piped into method-syntax '.OrderBy(...)'
            var queryC = (from p in products
                          where p.Price < 1000m
                          select p)
                         .OrderBy(p => p.Name)
                         .ToList();

            // (d) Method-syntax '.Where(...)', wrapped and piped into query-syntax 'orderby'
            var queryD = (from p in products.Where(p => p.Price < 1000m)
                          orderby p.Name
                          select p).ToList();

            // Output Results
            Console.WriteLine("--- (a) Method Syntax ---");
            queryA.ForEach(p => Console.WriteLine(p));

            // Verification via SequenceEqual (comparing IDs to ensure identical items & order)
            bool matchAB = queryA.Select(p => p.Id).SequenceEqual(queryB.Select(p => p.Id));
            bool matchAC = queryA.Select(p => p.Id).SequenceEqual(queryC.Select(p => p.Id));
            bool matchAD = queryA.Select(p => p.Id).SequenceEqual(queryD.Select(p => p.Id));

            Console.WriteLine("\n--- Sequence Equality Verification ---");
            Console.WriteLine($"A == B : {matchAB}");
            Console.WriteLine($"A == C : {matchAC}");
            Console.WriteLine($"A == D : {matchAD}");
            Console.WriteLine($"All 4 identical: {matchAB && matchAC && matchAD}");
        }
    }
}
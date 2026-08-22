using System;
using System.Collections.Generic;
using System.Linq;

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
    }

    public class ProductSummaryDto
    {
        public string Name { get; set; }
        public string PriceLabel { get; set; }

        public override string ToString() => $"{Name} -> {PriceLabel}";
    }

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

            // 1. Project to just names (IEnumerable<string>)
            IEnumerable<string> productNames = products.Select(p => p.Name);

            Console.WriteLine("=== 1. Product Names ===");
            foreach (var name in productNames)
            {
                Console.WriteLine(name);
            }

            // 2. Project to anonymous type: Name + PriceWithTax (18% tax)
            var anonymousWithTax = products.Select(p => new
            {
                p.Name,
                PriceWithTax = Math.Round(p.Price * 1.18m, 2)
            });

            Console.WriteLine("\n=== 2. Anonymous Type (Name & PriceWithTax) ===");
            foreach (var item in anonymousWithTax)
            {
                Console.WriteLine($"{item.Name,-22} | Incl. 18% Tax: Rs.{item.PriceWithTax:F2}");
            }

            // 3. Project to named DTO: ProductSummaryDto
            IEnumerable<ProductSummaryDto> dtos = products.Select(p => new ProductSummaryDto
            {
                Name = p.Name,
                PriceLabel = $"Rs.{p.Price:F2}"
            });

            Console.WriteLine("\n=== 3. Named ProductSummaryDto ===");
            foreach (var dto in dtos)
            {
                Console.WriteLine(dto);
            }

            // 4. Index-aware Select: "#1: Keyboard"-style formatting
            IEnumerable<string> indexedLabels = products.Select((p, index) => $"#{index + 1}: {p.Name}");

            Console.WriteLine("\n=== 4. Index-Aware Projection ===");
            foreach (var label in indexedLabels)
            {
                Console.WriteLine(label);
            }
        }
    }

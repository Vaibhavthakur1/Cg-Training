using System;
using System.Collections.Generic;

namespace Lab6LambdasExpressionVsStatement
{
    // Supporting domain models
    public record OrderItem(string Name, double UnitPrice, int Quantity);

    public record Order(int Id, string CustomerName, List<OrderItem> Items);

    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public double DiscountRate { get; set; } // e.g., 0.15 for 15% off
        public int StockQuantity { get; set; }

        public double DiscountedPrice => Price * (1.0 - DiscountRate);

        public override string ToString() =>
            $"{Name,-18} | Price: ${Price,6:F2} | DiscPrice: ${DiscountedPrice,6:F2} | Stock: {StockQuantity,2}";
    }

    public class Program
    {
        public static void Main()
        {
            // 1. Expression-Bodied Lambda
            Console.WriteLine("=== 1. Expression-Bodied Lambda ===");
            Func<double, double, double> rectangleArea = (w, h) => w * h;

            Console.WriteLine($"Rectangle Area (12.5 x 4.0): {rectangleArea(12.5, 4.0):F2}");
            Console.WriteLine();

            // 2. Statement-Bodied Lambda (Multi-Line Formatted Receipt)
            Console.WriteLine("=== 2. Statement-Bodied Lambda ===");
            Action<Order> printReceipt = order =>
            {
                double total = 0;
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine($"RECEIPT - ORDER #{order.Id}");
                Console.WriteLine($"Customer: {order.CustomerName}");
                Console.WriteLine("---------------------------------------------");

                foreach (var item in order.Items)
                {
                    double lineTotal = item.UnitPrice * item.Quantity;
                    total += lineTotal;
                    Console.WriteLine($"{item.Name,-20} x{item.Quantity}  ${lineTotal,7:F2}");
                }

                Console.WriteLine("---------------------------------------------");
                Console.WriteLine($"TOTAL DUE:                     ${total,7:F2}");
                Console.WriteLine("---------------------------------------------\n");
            };

            var sampleOrder = new Order(501, "Jane Doe", new List<OrderItem>
            {
                new("Mechanical Keyboard", 120.00, 1),
                new("USB-C Cable", 15.50, 2),
                new("Desk Pad", 25.00, 1)
            });

            printReceipt(sampleOrder);

            // 3. Sorting List<Product> using Comparison<T> Lambdas
            Console.WriteLine("=== 3. List<T>.Sort with Lambdas ===");

            var products = new List<Product>
            {
                new() { Name = "Ergonomic Chair",  Price = 350.00, DiscountRate = 0.20, StockQuantity = 8 },
                new() { Name = "4K Monitor",       Price = 420.00, DiscountRate = 0.05, StockQuantity = 0 },
                new() { Name = "Wireless Mouse",   Price = 45.00,  DiscountRate = 0.10, StockQuantity = 15 },
                new() { Name = "Noise-Canceling",  Price = 210.00, DiscountRate = 0.30, StockQuantity = 0 },
                new() { Name = "Standing Desk",    Price = 520.00, DiscountRate = 0.15, StockQuantity = 3 }
            };

            // Sort 1: By Price Ascending
            Console.WriteLine("-- Sorted by Price Ascending --");
            products.Sort((p1, p2) => p1.Price.CompareTo(p2.Price));
            products.ForEach(p => Console.WriteLine(p));
            Console.WriteLine();

            // Sort 2: By Name Descending
            Console.WriteLine("-- Sorted by Name Descending --");
            products.Sort((p1, p2) => string.Compare(p2.Name, p1.Name, StringComparison.OrdinalIgnoreCase));
            products.ForEach(p => Console.WriteLine(p));
            Console.WriteLine();

            // Sort 3: By Computed "Discounted Price" Ascending
            Console.WriteLine("-- Sorted by Discounted Price Ascending --");
            products.Sort((p1, p2) => p1.DiscountedPrice.CompareTo(p2.DiscountedPrice));
            products.ForEach(p => Console.WriteLine(p));
            Console.WriteLine();

            // 4. RemoveAll with Predicate Lambda (Out-of-Stock Filter)
            Console.WriteLine("=== 4. RemoveAll Out-of-Stock Products ===");
            int removedCount = products.RemoveAll(p => p.StockQuantity <= 0);

            Console.WriteLine($"Removed {removedCount} out-of-stock item(s).\n");
            Console.WriteLine("-- In-Stock Inventory --");
            products.ForEach(p => Console.WriteLine(p));
        }
    }
}
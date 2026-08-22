using System;
using System.Collections.Generic;

    // 1. Custom delegate declare
    public delegate double Discount(double price);

    public class Program
    {
        // 2. Matching methods
        public static double NoDiscount(double price) => price;

        public static double TenPercentOff(double price) => price * 0.90;

        public static double HalfOff(double price) => price * 0.50;

        // 3. Higher-order method accepting the delegate
        public static double ApplyDiscount(double price, Discount discount)
        {
            return discount(price);
        }

        public static void Main()
        {
            double initialPrice = 100.00;

            // 4. Invoking ApplyDiscount with each method
            Console.WriteLine("=== 4. Direct ApplyDiscount Calls ===");
            Console.WriteLine($"Initial Price:       ${initialPrice:F2}");
            Console.WriteLine($"NoDiscount:          ${ApplyDiscount(initialPrice, NoDiscount):F2}");
            Console.WriteLine($"TenPercentOff:       ${ApplyDiscount(initialPrice, TenPercentOff):F2}");
            Console.WriteLine($"HalfOff:             ${ApplyDiscount(initialPrice, HalfOff):F2}");
            Console.WriteLine();

            // 5. Storing and iterating delegates in a List<Discount>
            Console.WriteLine("=== 5. List<Discount> Iteration ===");
            var discountStrategies = new List<Discount>
            {
                NoDiscount,
                TenPercentOff,
                HalfOff
            };

            foreach (var discount in discountStrategies)
            {
                double finalPrice = discount(initialPrice);
                Console.WriteLine($"Method [{discount.Method.Name}]: ${finalPrice:F2}");
            }
        }
    }

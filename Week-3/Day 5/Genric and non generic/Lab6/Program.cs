using System;
using System.Collections.Generic;

namespace GenericsLab
{
    // 1. Generic Swap Method & 4. AllMatch Method
    public static class GenericUtilities
    {
        // 1. Generic Swap method
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        // 4. Generic AllMatch method
        public static bool AllMatch<T>(IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            foreach (T item in items)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }
            return true;
        }
    }

    // ==========================================
    // 2. Generic Pair Class
    // ==========================================
    public class Pair<TFirst, TSecond>
    {
        public TFirst First { get; set; }
        public TSecond Second { get; set; }

        public Pair(TFirst first, TSecond second)
        {
            First = first;
            Second = second;
        }

        public override string ToString()
        {
            return $"({First}, {Second})";
        }
    }

    // 3. Generic MinMaxTracker Class
    public class MinMaxTracker<T> where T : IComparable<T>
    {
        private bool _hasValues;

        public T Min { get; private set; }
        public T Max { get; private set; }

        public void Add(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (!_hasValues)
            {
                Min = value;
                Max = value;
                _hasValues = true;
                return;
            }

            // O(1) comparison against cached extremes
            if (value.CompareTo(Min) < 0)
            {
                Min = value;
            }

            if (value.CompareTo(Max) > 0)
            {
                Max = value;
            }
        }
    }

    // ==========================================
    // Custom Type for Testing: Product
    // ==========================================
    public class Product : IComparable<Product>
    {
        public string Name { get; }
        public decimal Price { get; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public int CompareTo(Product other)
        {
            if (other == null) return 1;
            return Price.CompareTo(other.Price);
        }

        public override string ToString() => $"{Name} (${Price})";
    }

    // ==========================================
    // 5. Test Suite
    // ==========================================
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("=== 1. Testing Swap<T> ===");
            // Type 1: int
            int x = 10, y = 20;
            Console.WriteLine($"Before Swap (int): x = {x}, y = {y}");
            GenericUtilities.Swap(ref x, ref y);
            Console.WriteLine($"After Swap (int):  x = {x}, y = {y}");

            // Type 2: Product
            Product p1 = new Product("Laptop", 1200m);
            Product p2 = new Product("Mouse", 25m);
            Console.WriteLine($"Before Swap (Product): p1 = {p1}, p2 = {p2}");
            GenericUtilities.Swap(ref p1, ref p2);
            Console.WriteLine($"After Swap (Product):  p1 = {p1}, p2 = {p2}");
            Console.WriteLine();

            Console.WriteLine("=== 2. Testing Pair<TFirst, TSecond> ===");
            // Type Combination 1: <int, string>
            var pair1 = new Pair<int, string>(101, "Admin");
            Console.WriteLine($"Pair<int, string>: {pair1}");

            // Type Combination 2: <Product, bool>
            var pair2 = new Pair<Product, bool>(new Product("Keyboard", 75m), true);
            Console.WriteLine($"Pair<Product, bool>: {pair2}");
            Console.WriteLine();

            Console.WriteLine("=== 3. Testing MinMaxTracker<T> ===");
            // Type 1: int
            var intTracker = new MinMaxTracker<int>();
            intTracker.Add(45);
            intTracker.Add(12);
            intTracker.Add(89);
            intTracker.Add(3);
            Console.WriteLine($"MinMaxTracker<int> -> Min: {intTracker.Min}, Max: {intTracker.Max}");

            // Type 2: Product
            var productTracker = new MinMaxTracker<Product>();
            productTracker.Add(new Product("Monitor", 300m));
            productTracker.Add(new Product("Desk", 450m));
            productTracker.Add(new Product("Stylus", 15m));
            productTracker.Add(new Product("Headphones", 120m));
            Console.WriteLine($"MinMaxTracker<Product> -> Min: {productTracker.Min}, Max: {productTracker.Max}");
            Console.WriteLine();

            Console.WriteLine("=== 4. Testing AllMatch<T> ===");
            // Type 1: int (Check if all are positive)
            var intList = new List<int> { 2, 4, 6, 8, 10 };
            bool allPositive = GenericUtilities.AllMatch(intList, n => n > 0);
            Console.WriteLine($"All integers positive in [{string.Join(", ", intList)}]: {allPositive}");

            // Type 2: Product (Check if all products cost over $10)
            var productList = new List<Product>
            {
                new Product("Book", 15m),
                new Product("Pen", 5m), // Fails condition
                new Product("Backpack", 45m)
            };
            bool allOverTen = GenericUtilities.AllMatch(productList, p => p.Price > 10m);
            Console.WriteLine($"All products > $10: {allOverTen}");
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab5MyList
{
    // Custom reference type for testing
    public record Product(int Id, string Name);

    public class MyList<T> : IEnumerable<T>
    {
        private const int DefaultCapacity = 4;
        private T[] _items;
        private int _count;

        public int Count => _count;
        public int Capacity => _items.Length;

        public MyList(int capacity = DefaultCapacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity cannot be negative.");
            }

            _items = new T[capacity == 0 ? DefaultCapacity : capacity];
            _count = 0;
        }

        // Indexer with bounds checking
        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return _items[index];
            }
            set
            {
                ValidateIndex(index);
                _items[index] = value;
            }
        }

        // Add method enabling collection initializer syntax: new MyList<T> { a, b, c }
        public void Add(T item)
        {
            EnsureCapacity(_count + 1);
            _items[_count] = item;
            _count++;
        }

        // Remove element at index and shift trailing elements left
        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            _count--;
            if (index < _count)
            {
                Array.Copy(_items, index + 1, _items, index, _count - index);
            }

            // Clear reference to avoid memory leaks with reference types
            _items[_count] = default!;
        }

        // Geometric capacity doubling: O(1) amortized insertion
        private void EnsureCapacity(int minCapacity)
        {
            if (_items.Length < minCapacity)
            {
                int newCapacity = _items.Length == 0 ? DefaultCapacity : _items.Length * 2;
                if (newCapacity < minCapacity)
                {
                    newCapacity = minCapacity;
                }

                T[] newItems = new T[newCapacity];
                Array.Copy(_items, newItems, _count);
                _items = newItems;
            }
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} is out of range [0, {_count - 1}].");
            }
        }

        // 1. Generic Enumerator using yield return
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _items[i];
            }
        }

        // 2. Non-generic IEnumerable fallback
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class Program
    {
        public static void Main()
        {
            // 3. Collection-Initializer Syntax & Value Type Test
            Console.WriteLine("=== 1. Testing with int (Value Type) & Collection Initializer ===");
            var numbers = new MyList<int> { 10, 20, 30 };
            numbers.Add(40);
            numbers.Add(50); // Triggers capacity expansion

            Console.WriteLine($"Numbers Count: {numbers.Count}, Underlying Capacity: {numbers.Capacity}");

            // 2. Prove foreach works
            Console.Write("Numbers via foreach: ");
            foreach (var num in numbers)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();

            // Test RemoveAt and Indexer mutation
            numbers.RemoveAt(1); // Remove 20
            numbers[0] = 99;     // Update 10 -> 99
            Console.Write("After removing index 1 and updating index 0: ");
            foreach (var num in numbers)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine("\n");

            // 1. Testing with Reference Type
            Console.WriteLine("=== 2. Testing with Custom Reference Type (Product) ===");
            var products = new MyList<Product>
            {
                new Product(1, "Keyboard"),
                new Product(2, "Mouse"),
                new Product(3, "Monitor")
            };

            foreach (var p in products)
            {
                Console.WriteLine($"Product #{p.Id}: {p.Name}");
            }
            Console.WriteLine();

            // 4. Deliberately Trigger & Catch Out-of-Range Access
            Console.WriteLine("=== 3. Deliberately Testing Out-Of-Range Access ===");
            try
            {
                Console.WriteLine($"Accessing index 10 on a list of count {products.Count}...");
                var invalidAccess = products[10];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Successfully caught expected exception: {ex.Message}");
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomCollectionLab
{
    // =========================================================================
    // 1 & 2. FixedSizeStack<T> implementing IEnumerable<T> & IReadOnlyCollection<T>
    // =========================================================================
    public class FixedSizeStack<T> : IReadOnlyCollection<T>
    {
        private readonly T[] _items;
        private int _count;

        public int Capacity => _items.Length;
        public int Count => _count;
        public bool IsFull => _count == _items.Length;
        public bool IsEmpty => _count == 0;

        public FixedSizeStack(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be non-negative.");
            }

            _items = new T[capacity];
            _count = 0;
        }

        public void Push(T item)
        {
            if (IsFull)
            {
                throw new InvalidOperationException($"Stack is full. Maximum capacity of {Capacity} reached.");
            }

            _items[_count++] = item;
        }

        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Cannot pop from an empty stack.");
            }

            _count--;
            T item = _items[_count];
            _items[_count] = default; // Clear reference to allow garbage collection for reference types
            return item;
        }

        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Cannot peek at an empty stack.");
            }

            return _items[_count - 1];
        }

        // Top-to-bottom iteration (LIFO order)
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    // 3. Generic Extension Method
    public static class FixedSizeStackExtensions
    {
        public static FixedSizeStack<T> ToFixedSizeStack<T>(this IEnumerable<T> source, int capacity)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be non-negative.");

            var stack = new FixedSizeStack<T>(capacity);

            foreach (T item in source)
            {
                if (stack.IsFull)
                {
                    throw new InvalidOperationException($"Source sequence contains more elements than the stack capacity of {capacity}.");
                }

                stack.Push(item);
            }

            return stack;
        }
    }

    // 4. Demonstration Program
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Demonstration 1: FixedSizeStack<int> ===");
            var intStack = new FixedSizeStack<int>(5);

            Console.WriteLine("Pushing integers: 10, 20, 30, 40");
            intStack.Push(10);
            intStack.Push(20);
            intStack.Push(30);
            intStack.Push(40);

            Console.WriteLine($"Count: {intStack.Count} / Capacity: {intStack.Capacity}");
            Console.WriteLine($"Peek at top: {intStack.Peek()}");

            Console.WriteLine("\nIterating through stack with foreach (Top to Bottom):");
            foreach (int value in intStack)
            {
                Console.WriteLine($" -> {value}");
            }

            Console.WriteLine($"\nPopping top item: {intStack.Pop()}");
            Console.WriteLine($"New Count after Pop: {intStack.Count}");
            Console.WriteLine($"New Peek at top: {intStack.Peek()}");

            Console.WriteLine("\n=== Demonstration 2: Extension Method with List<string> ===");
            var stringList = new List<string> { "First", "Second", "Third" };

            Console.WriteLine($"Original List: [{string.Join(", ", stringList)}]");

            // Convert List<string> to FixedSizeStack<string> with capacity 5
            FixedSizeStack<string> stringStack = stringList.ToFixedSizeStack(5);

            Console.WriteLine($"Converted Stack Count: {stringStack.Count}, Capacity: {stringStack.Capacity}");

            Console.WriteLine("\nIterating converted stack with foreach (Top to Bottom):");
            foreach (string word in stringStack)
            {
                Console.WriteLine($" -> {word}");
            }

            Console.WriteLine("\n=== Demonstration 3: Exception Handling Verification ===");
            try
            {
                Console.WriteLine("Attempting to pop remaining items until empty...");
                while (!stringStack.IsEmpty)
                {
                    Console.WriteLine($"Popped: {stringStack.Pop()}");
                }

                // Trigger InvalidOperationException on empty Pop
                stringStack.Pop();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Caught expected exception: {ex.Message}");
            }
        }
    }
}
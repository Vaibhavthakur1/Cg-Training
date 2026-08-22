using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LabIteratorsAndYield
{
    // =========================================================================
    // 4. TreeNode<T> with Recursive Depth-First Search (DFS) Traversal
    // =========================================================================
    public class TreeNode<T> : IEnumerable<T>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Children { get; } = new();

        public TreeNode(T value)
        {
            Value = value;
        }

        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value);
            Children.Add(node);
            return node;
        }

        // Recursive Pre-Order Depth-First Traversal
        public IEnumerator<T> GetEnumerator()
        {
            // 1. Yield current node
            yield return Value;

            // 2. Recursively yield each descendant
            foreach (var child in Children)
            {
                foreach (var descendant in child)
                {
                    yield return descendant;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    // =========================================================================
    // 5. MyList<T> with Secondary Named Iterator (InReverse)
    // =========================================================================
    public class MyList<T> : IEnumerable<T>
    {
        private T[] _items = new T[4];
        private int _count;

        public int Count => _count;

        public void Add(T item)
        {
            if (_count == _items.Length)
            {
                Array.Resize(ref _items, _items.Length * 2);
            }
            _items[_count++] = item;
        }

        // Primary forward enumerator
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // Secondary named iterator yielding backwards without allocating an array
        public IEnumerable<T> InReverse()
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }
    }

    // =========================================================================
    // Demonstration & Verification
    // =========================================================================
    public class Program
    {
        public static void Main()
        {
            // ----------------------------------------------------
            // 1. Infinite Fibonacci Iterator
            // ----------------------------------------------------
            Console.WriteLine("=== 1. Infinite Fibonacci (First 10 Values) ===");
            var first10Fibs = Fibonacci().Take(10);
            Console.WriteLine(string.Join(", ", first10Fibs));
            Console.WriteLine();

            // ----------------------------------------------------
            // 2. TakeWhilePositive with yield break
            // ----------------------------------------------------
            Console.WriteLine("=== 2. TakeWhilePositive with yield break ===");
            int[] testNumbers = { 5, 12, 8, 3, 0, 7, -2, 10 };
            var positivePrefix = TakeWhilePositive(testNumbers);
            Console.WriteLine($"Input:  [{string.Join(", ", testNumbers)}]");
            Console.WriteLine($"Output: [{string.Join(", ", positivePrefix)}]");
            Console.WriteLine();

            // ----------------------------------------------------
            // 3. Proof of Lazy Evaluation
            // ----------------------------------------------------
            Console.WriteLine("=== 3. Proof of Lazy Evaluation ===");
            Console.WriteLine("Calling LazyDemo()...");
            IEnumerable<int> lazySequence = LazyDemo();
            Console.WriteLine("-> Function returned, but iterator has NOT executed yet.");

            Console.WriteLine("Starting foreach loop:");
            foreach (var item in lazySequence)
            {
                Console.WriteLine($"[Consumer] Received value: {item}");
            }
            Console.WriteLine();

            // ----------------------------------------------------
            // 4. TreeNode<T> Depth-First Traversal
            // ----------------------------------------------------
            Console.WriteLine("=== 4. TreeNode<T> Recursive DFS Traversal ===");
            var root = new TreeNode<string>("Root");
            var dev = root.AddChild("Dev");
            var qa = root.AddChild("QA");

            dev.AddChild("Frontend");
            dev.AddChild("Backend");
            qa.AddChild("Automation");

            Console.WriteLine("Tree DFS Traversal via foreach:");
            foreach (var nodeValue in root)
            {
                Console.WriteLine($"• {nodeValue}");
            }
            Console.WriteLine();

            // ----------------------------------------------------
            // 5. Secondary Named Iterator (InReverse)
            // ----------------------------------------------------
            Console.WriteLine("=== 5. InReverse() Named Iterator on MyList<T> ===");
            var list = new MyList<string> { "First", "Second", "Third", "Fourth" };

            Console.WriteLine($"Forward: {string.Join(" -> ", list)}");
            Console.WriteLine($"Reverse: {string.Join(" -> ", list.InReverse())}");
        }

        // 1. Infinite Fibonacci sequence generator
        public static IEnumerable<int> Fibonacci()
        {
            int current = 0;
            int next = 1;

            while (true)
            {
                yield return current;
                int temp = current + next;
                current = next;
                next = temp;
            }
        }

        // 2. Custom TakeWhilePositive using yield break
        public static IEnumerable<int> TakeWhilePositive(IEnumerable<int> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            foreach (var item in source)
            {
                if (item <= 0)
                {
                    yield break; // Halts enumeration immediately
                }
                yield return item;
            }
        }

        // 3. Iterator demonstrating deferred execution
        public static IEnumerable<int> LazyDemo()
        {
            Console.WriteLine("  [Iterator] Inside LazyDemo() -> Generating 100");
            yield return 100;

            Console.WriteLine("  [Iterator] Resuming LazyDemo() -> Generating 200");
            yield return 200;
        }
    }
}
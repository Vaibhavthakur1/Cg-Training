using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab6MyDictionary
{
    // =========================================================================
    // Custom Chained-Hash-Table Generic Dictionary
    // =========================================================================
    public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : notnull
    {
        // Singly-linked list node for bucket chaining (collision resolution)
        private class Node
        {
            public TKey Key { get; }
            public TValue Value { get; set; }
            public Node? Next { get; set; }

            public Node(TKey key, TValue value, Node? next = null)
            {
                Key = key;
                Value = value;
                Next = next;
            }
        }

        private readonly Node?[] _buckets;
        private readonly IEqualityComparer<TKey> _comparer;
        private int _count;

        public int Count => _count;
        public int BucketCount => _buckets.Length;

        public MyDictionary(int bucketCount = 7, IEqualityComparer<TKey>? comparer = null)
        {
            if (bucketCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bucketCount), "Bucket count must be greater than zero.");
            }

            _buckets = new Node?[bucketCount];
            _comparer = comparer ?? EqualityComparer<TKey>.Default;
            _count = 0;
        }

        // Map hash code to a non-negative bucket index
        private int GetBucketIndex(TKey key)
        {
            int hashCode = _comparer.GetHashCode(key);
            return (int)((uint)hashCode % (uint)_buckets.Length);
        }

        // Add method: throws on duplicate keys
        public void Add(TKey key, TValue value)
        {
            ArgumentNullException.ThrowIfNull(key);

            int bucketIndex = GetBucketIndex(key);
            Node? current = _buckets[bucketIndex];

            while (current != null)
            {
                if (_comparer.Equals(current.Key, key))
                {
                    throw new ArgumentException($"An item with key '{key}' already exists in the dictionary.", nameof(key));
                }
                current = current.Next;
            }

            // Prepend new node to bucket chain: O(1)
            _buckets[bucketIndex] = new Node(key, value, _buckets[bucketIndex]);
            _count++;
        }

        // TryGetValue: safe lookup without throwing exceptions
        public bool TryGetValue(TKey key, out TValue value)
        {
            ArgumentNullException.ThrowIfNull(key);

            int bucketIndex = GetBucketIndex(key);
            Node? current = _buckets[bucketIndex];

            while (current != null)
            {
                if (_comparer.Equals(current.Key, key))
                {
                    value = current.Value;
                    return true;
                }
                current = current.Next;
            }

            value = default!;
            return false;
        }

        // Indexer: Get (throws KeyNotFoundException) and Set (insert or update)
        public TValue this[TKey key]
        {
            get
            {
                if (TryGetValue(key, out var value))
                {
                    return value;
                }
                throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
            }
            set
            {
                ArgumentNullException.ThrowIfNull(key);

                int bucketIndex = GetBucketIndex(key);
                Node? current = _buckets[bucketIndex];

                while (current != null)
                {
                    if (_comparer.Equals(current.Key, key))
                    {
                        current.Value = value; // Update existing
                        return;
                    }
                    current = current.Next;
                }

                // Insert new key-value pair
                _buckets[bucketIndex] = new Node(key, value, _buckets[bucketIndex]);
                _count++;
            }
        }

        // Enumerates all key-value pairs across all non-empty buckets
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < _buckets.Length; i++)
            {
                Node? current = _buckets[i];
                while (current != null)
                {
                    yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    // =========================================================================
    // Demonstration & Verification
    // =========================================================================
    public class Program
    {
        public static void Main()
        {
            // ----------------------------------------------------
            // 1. Store 25 items across 7 buckets (Forcing Collisions)
            // ----------------------------------------------------
            Console.WriteLine("=== 1. Collision Verification (25 Keys into 7 Buckets) ===");
            var myDict = new MyDictionary<string, int>(bucketCount: 7);
            var realDict = new Dictionary<string, int>();

            for (int i = 1; i <= 25; i++)
            {
                string key = $"Item_{i}";
                int val = i * 10;

                myDict.Add(key, val);
                realDict.Add(key, val);
            }

            Console.WriteLine($"Total Entries: {myDict.Count}, Buckets: {myDict.BucketCount}");
            Console.WriteLine($"Average Chain Length: {(double)myDict.Count / myDict.BucketCount:F2} items/bucket\n");

            // ----------------------------------------------------
            // 2. Compare Lookups Against System.Collections.Generic.Dictionary
            // ----------------------------------------------------
            Console.WriteLine("=== 2. Parity Check Against Standard Dictionary ===");
            bool allMatched = true;

            for (int i = 1; i <= 25; i++)
            {
                string key = $"Item_{i}";
                bool mySuccess = myDict.TryGetValue(key, out int myVal);
                bool realSuccess = realDict.TryGetValue(key, out int realVal);

                if (!mySuccess || !realSuccess || myVal != realVal || myDict[key] != realDict[key])
                {
                    Console.WriteLine($"Mismatch found for key: {key}");
                    allMatched = false;
                    break;
                }
            }

            Console.WriteLine($"All 25 lookups matched standard Dictionary behavior: {allMatched}\n");

            // ----------------------------------------------------
            // 3. Index Initializer Syntax Demo
            // ----------------------------------------------------
            Console.WriteLine("=== 3. Index Initializer Syntax Demo ===");
            var countryCodes = new MyDictionary<string, string>(bucketCount: 5)
            {
                ["US"] = "United States",
                ["CA"] = "Canada",
                ["UK"] = "United Kingdom",
                ["IN"] = "India",
                ["JP"] = "Japan"
            };

            // Update an existing key and add a new one via indexer
            countryCodes["US"] = "USA";
            countryCodes["DE"] = "Germany";

            foreach (var kvp in countryCodes)
            {
                Console.WriteLine($"Code: {kvp.Key} -> Country: {kvp.Value}");
            }
            Console.WriteLine();

            Console.WriteLine("=== 4. KeyNotFoundException Test ===");
            try
            {
                Console.WriteLine("Attempting to access non-existent key 'FR'...");
                var val = countryCodes["FR"];
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Caught expected exception: {ex.Message}");
            }
        }
    }
}
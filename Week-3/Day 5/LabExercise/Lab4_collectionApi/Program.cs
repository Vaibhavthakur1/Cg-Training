using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lab4CollectionAPI
{
    public class Program
    {
        public static void Main()
        {
            // Setup test data
            var sourceItems = new[] { 10, 20, 30 };

            // 3. Demonstrate on List<T>, HashSet<T>, and LinkedList<T>
            Console.WriteLine("=== 1. Testing with List<int> ===");
            var list = new List<int> { 1, 2 };
            bool listAdded = TryAddAll(list, sourceItems);
            int[] listSnapshot = Snapshot(list);
            Console.WriteLine($"TryAddAll Success: {listAdded}");
            Console.WriteLine($"Snapshot Array: [{string.Join(", ", listSnapshot)}]");

            Console.WriteLine("\n=== 2. Testing with HashSet<int> ===");
            var set = new HashSet<int> { 1, 2 };
            bool setAdded = TryAddAll(set, sourceItems);
            int[] setSnapshot = Snapshot(set);
            Console.WriteLine($"TryAddAll Success: {setAdded}");
            Console.WriteLine($"Snapshot Array: [{string.Join(", ", setSnapshot)}]");

            Console.WriteLine("\n=== 3. Testing with LinkedList<int> ===");
            var linkedList = new LinkedList<int>();
            linkedList.AddLast(1);
            linkedList.AddLast(2);
            bool linkedListAdded = TryAddAll(linkedList, sourceItems);
            int[] linkedListSnapshot = Snapshot(linkedList);
            Console.WriteLine($"TryAddAll Success: {linkedListAdded}");
            Console.WriteLine($"Snapshot Array: [{string.Join(", ", linkedListSnapshot)}]");

            // 4. Demonstrate ReadOnly Collection Refusal
            Console.WriteLine("\n=== 4. Testing with ReadOnlyCollection<int> ===");
            var baseList = new List<int> { 1, 2 };
            ICollection<int> readOnlyWrapper = baseList.AsReadOnly();

            Console.WriteLine($"Is target ReadOnly? {readOnlyWrapper.IsReadOnly}");
            bool readOnlyAdded = TryAddAll(readOnlyWrapper, sourceItems);
            Console.WriteLine($"TryAddAll on ReadOnly Collection Succeeded: {readOnlyAdded}");

            int[] readOnlySnapshot = Snapshot(readOnlyWrapper);
            Console.WriteLine($"Snapshot remains unchanged: [{string.Join(", ", readOnlySnapshot)}]");
        }

         //1. Snapshot using ICollection<T>.CopyTo
        public static T[] Snapshot<T>(ICollection<T> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            // Allocate an exact-sized array and copy using native CopyTo
            var array = new T[source.Count];
            source.CopyTo(array, 0);
            return array;
        }

        // 2. TryAddAll checking ICollection<T>.IsReadOnly
        public static bool TryAddAll<T>(ICollection<T> target, IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(target);
            ArgumentNullException.ThrowIfNull(items);

            // Guard against read-only collections before attempting mutations
            if (target.IsReadOnly)
            {
                return false;
            }

            foreach (var item in items)
            {
                target.Add(item);
            }

            return true;
        }
    }
}
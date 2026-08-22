using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        // --- Task 2: Process List<int> (Reject negative numbers) ---
        Console.WriteLine("=== Processing Integers ===");
        var numbers = new List<int> { 10, -5, 42, -1, 0, 99 };

        ProcessBatch(
            items: numbers,
            validator: num => num >= 0,
            onSuccess: num => Console.WriteLine($"[PASS] Integer: {num}"),
            onFailure: (num, reason) => Console.WriteLine($"[FAIL] Integer: {num} -> Reason: {reason}")
        );

        Console.WriteLine();

        // --- Task 3: Process List<string> (Reject empty/whitespace strings) ---
        Console.WriteLine("=== Processing Strings ===");
        var textEntries = new List<string> { "Alice", "", "Bob", "   ", "Charlie", null };

        ProcessBatch(
            items: textEntries,
            validator: text => !string.IsNullOrWhiteSpace(text),
            onSuccess: text => Console.WriteLine($"[PASS] Name: '{text}'"),
            onFailure: (text, reason) => Console.WriteLine($"[FAIL] Name: '{text ?? "<null>"}' -> Reason: {reason}")
        );
    }

    // --- Task 1: Generic Batch Processor with Callbacks ---
    public static void ProcessBatch<T>(
        List<T> items,
        Action<T> onSuccess,
        Action<T, string> onFailure,
        Func<T, bool> validator)
    {
        if (items == null) throw new ArgumentNullException(nameof(items));
        if (onSuccess == null) throw new ArgumentNullException(nameof(onSuccess));
        if (onFailure == null) throw new ArgumentNullException(nameof(onFailure));
        if (validator == null) throw new ArgumentNullException(nameof(validator));

        foreach (var item in items)
        {
            if (validator(item))
            {
                onSuccess(item);
            }
            else
            {
                onFailure(item, "Validation rule failed.");
            }
        }
    }
}
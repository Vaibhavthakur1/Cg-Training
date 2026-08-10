using System;

class Lab1
{
    static void Main()
    {
        
        string original = "  Hello, Training Team!  ";

        // removes leading and trailing spaces
        string trimmed = original.Trim();

        // ReferenceEquals refer to the same object
        Console.WriteLine("ReferenceEquals(original, trimmed): " +
            object.ReferenceEquals(original, trimmed));

        // Contains the string word "Training"
        Console.WriteLine("Contains \"Training\": " +
            trimmed.Contains("Training"));

        // StartsWith with "Hello"
        Console.WriteLine("StartsWith trimmed \"Hello\": " +
            trimmed.StartsWith("Hello"));

        // IndexOf of the first comma
        Console.WriteLine("Index of first comma: " +
            trimmed.IndexOf(','));

        // Replace  string with "Training Team" 
        string replaced = trimmed.Replace("Training Team", "Engineering Team");

        // Print the replaced string
        Console.WriteLine("\"Training Team\" replaced -> " + replaced);

        // Split separates the string using spaces and commas and removes empty entries
        string[] words = trimmed.Split(
            new char[] { ' ', ',' },
            StringSplitOptions.RemoveEmptyEntries);

        // Print each word on a separate line
        foreach (string word in words)
        {
            Console.WriteLine(word);
        }

        // Check whether null is considered null or whitespace
        Console.WriteLine("IsNullOrWhiteSpace(null): " +
            string.IsNullOrWhiteSpace(null));

        // Check whether an empty string is considered null or whitespace
        Console.WriteLine("IsNullOrWhiteSpace(\"\"): " +
            string.IsNullOrWhiteSpace(""));

        // Check whether a string containing only spaces is whitespace
        Console.WriteLine("IsNullOrWhiteSpace(\"   \"): " +
            string.IsNullOrWhiteSpace("   "));

        // Check whether a normal string is null or whitespace
        Console.WriteLine("IsNullOrWhiteSpace(\"ok\"): " +
            string.IsNullOrWhiteSpace("ok"));

        // Compare two strings ignoring their letter casing
        string first = "HELLO";
        string second = "hello";

        // OrdinalIgnoreCase ignores uppercase and lowercase differences, so the result is 0
        Console.WriteLine("String.Compare: " +
            string.Compare(first, second, StringComparison.OrdinalIgnoreCase));
    }
}
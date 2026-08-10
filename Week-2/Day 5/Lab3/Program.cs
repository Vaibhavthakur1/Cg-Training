using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

static class StringToolkit
{
    // Reverses the char
    public static string Reverse(string input)
    {
        char[] chars = input.ToCharArray();

        Array.Reverse(chars);

        return new string(chars);
    }

    // Count a character appears in string
    public static int CountChar(string text, char searchChar)
    {
        int count = 0;

        // Check every character in the string
        foreach (char c in text)
        {
            if (c == searchChar)
            {
                count++;
            }
        }

        return count;
    }

    // Removes duplicate characters while keeping the first occurrence
    public static string RemoveDuplicates(string input)
    {
        StringBuilder result = new StringBuilder();

        foreach (char c in input)
        {
            if (!result.ToString().Contains(c.ToString()))
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }

    // Check palindrome ignoring case and spaces
    public static bool IsPalindrome(string input)
    {
       
        string cleaned = input.Replace(" ", "").ToLower();

        string reversed = Reverse(cleaned);

        return cleaned == reversed;
    }

    // Convert the string to title case using the current culture
    public static string ToTitleCase(string input)
    {
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

        return textInfo.ToTitleCase(input.ToLower());
    }

    // Extracts only digit characters from the input
    public static string ExtractNumbers(string input)
    {
        StringBuilder result = new StringBuilder();

        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }
}

//class Lab3
//{
//    static void Main()
//    {
//        // Test Reverse
//        Console.WriteLine(
//            "Reverse(\"Hello\") -> " +
//            StringToolkit.Reverse("Hello"));

//        // Test CountChar
//        Console.WriteLine(
//            "CountChar(\"banana\", 'a') -> " +
//            StringToolkit.CountChar("banana", 'a'));

//        // Test RemoveDuplicates
//        Console.WriteLine(
//            "RemoveDuplicates(\"mississippi\") -> " +
//            StringToolkit.RemoveDuplicates("mississippi"));

//        // Test IsPalindrome
//        Console.WriteLine(
//            "IsPalindrome(\"race car\") -> " +
//            StringToolkit.IsPalindrome("race car"));

//        // Test ToTitleCase
//        Console.WriteLine(
//            "ToTitleCase(\"hello training team\") -> " +
//            StringToolkit.ToTitleCase("hello training team"));

//        // Test ExtractNumbers
//        Console.WriteLine(
//            "ExtractNumbers(\"Order #4521, qty 3\") -> " +
//            StringToolkit.ExtractNumbers("Order #4521, qty 3"));
//    }
//}
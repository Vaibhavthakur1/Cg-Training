using System;
using System.Text.RegularExpressions;

public static class PatternLibrary
{
    // Pre-compiled static regex fields for performance reuse
    public static readonly Regex Email = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled
    );

    public static readonly Regex UsPhone = new Regex(
        @"^\d{3}-\d{3}-\d{4}$",
        RegexOptions.Compiled
    );

    public static readonly Regex HexColor = new Regex(
        @"^#[0-9A-Fa-f]{6}$",
        RegexOptions.Compiled
    );

    // Wrapper validation methods
    public static bool IsValidEmail(string input) => Email.IsMatch(input);
    public static bool IsValidPhone(string input) => UsPhone.IsMatch(input);
    public static bool IsValidHexColor(string input) => HexColor.IsMatch(input);
}

class Program
{
    static void Main()
    {
        // TODO 3: IgnoreCase Demo
        bool ignoreCaseOff = Regex.IsMatch("HELLO", "hello");
        bool ignoreCaseOn = Regex.IsMatch("HELLO", "hello", RegexOptions.IgnoreCase);

        Console.WriteLine($"IgnoreCase off: {ignoreCaseOff}, IgnoreCase on: {ignoreCaseOn}");


        // TODO 4: Multiline Demo (matching start of line)
        string multilineText = "Line 1\nLine 2\nLine 3";

        // Without Multiline, '^' matches ONLY the beginning of the entire string (1 match)
        int countWithoutMultiline = Regex.Matches(multilineText, @"^\w+").Count;

        // With Multiline, '^' matches the beginning of EVERY line (3 matches)
        int countWithMultiline = Regex.Matches(multilineText, @"^\w+", RegexOptions.Multiline).Count;

        Console.WriteLine($"Line-start matches WITHOUT Multiline: {countWithoutMultiline}");
        Console.WriteLine($"Line-start matches WITH Multiline: {countWithMultiline}");


        // TODO 5: Exercise PatternLibrary with valid & invalid samples
        Console.WriteLine(
            $"IsValidEmail(\"a@b.com\"): {PatternLibrary.IsValidEmail("a@b.com")}, " +
            $"IsValidEmail(\"not-an-email\"): {PatternLibrary.IsValidEmail("not-an-email")}"
        );

        Console.WriteLine(
            $"IsValidPhone(\"555-123-4567\"): {PatternLibrary.IsValidPhone("555-123-4567")}, " +
            $"IsValidPhone(\"5551234567\"): {PatternLibrary.IsValidPhone("5551234567")}"
        );

        Console.WriteLine(
            $"IsValidHexColor(\"#1A2B3C\"): {PatternLibrary.IsValidHexColor("#1A2B3C")}, " +
            $"IsValidHexColor(\"1A2B3C\"): {PatternLibrary.IsValidHexColor("1A2B3C")}"
        );
    }
}
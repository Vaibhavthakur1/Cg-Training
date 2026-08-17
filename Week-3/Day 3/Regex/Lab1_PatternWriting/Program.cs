using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // TODO 1: US ZIP code
        string zipPattern = @"^\d{5}(-\d{4})?$";

        Console.WriteLine(
            $"ZIP \"12345\": {Regex.IsMatch("12345", zipPattern)} | " +
            $"\"12345-6789\": {Regex.IsMatch("12345-6789", zipPattern)} | " +
            $"\"1234\": {Regex.IsMatch("1234", zipPattern)}"
        );


        // TODO 2: Username
        string usernamePattern = @"^[A-Za-z][A-Za-z0-9_]{2,15}$";

        Console.WriteLine(
            $"Username \"user_1\": {Regex.IsMatch("user_1", usernamePattern)} | " +
            $"\"1user\": {Regex.IsMatch("1user", usernamePattern)} | " +
            $"\"ab\": {Regex.IsMatch("ab", usernamePattern)}"
        );


        // TODO 3: Hex color
        string hexPattern = @"^#[0-9A-Fa-f]{6}$";

        Console.WriteLine(
            $"Hex \"#1A2B3C\": {Regex.IsMatch("#1A2B3C", hexPattern)} | " +
            $"\"#GGGGGG\": {Regex.IsMatch("#GGGGGG", hexPattern)} | " +
            $"\"1A2B3C\": {Regex.IsMatch("1A2B3C", hexPattern)}"
        );


        // TODO 4: Password strength
        string password1 = "password";
        string password2 = "Password1";
        string password3 = "pass1";

        bool validPassword1 =
            password1.Length >= 8 &&
            Regex.IsMatch(password1, @"\d") &&
            Regex.IsMatch(password1, @"[A-Z]");

        bool validPassword2 =
            password2.Length >= 8 &&
            Regex.IsMatch(password2, @"\d") &&
            Regex.IsMatch(password2, @"[A-Z]");

        bool validPassword3 =
            password3.Length >= 8 &&
            Regex.IsMatch(password3, @"\d") &&
            Regex.IsMatch(password3, @"[A-Z]");

        Console.WriteLine(
            $"Password \"password\": {validPassword1} | " +
            $"\"Password1\": {validPassword2} | " +
            $"\"pass1\": {validPassword3}"
        );


        // TODO 5: Sentence ending in exactly one '.', '!' or '?'
        string sentencePattern = @"^[^.!?]+[.!?]$";

        Console.WriteLine(
            $"Sentence \"Hello there.\": {Regex.IsMatch("Hello there.", sentencePattern)} | " +
            $"\"Wait...\": {Regex.IsMatch("Wait...", sentencePattern)} | " +
            $"\"Really?\": {Regex.IsMatch("Really?", sentencePattern)}"
        );
    }
}
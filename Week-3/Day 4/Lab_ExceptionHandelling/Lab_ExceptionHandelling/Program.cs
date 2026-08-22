using System;

class Program
{
    static void ParseAge(string input)
    {
        Console.WriteLine("Step 1");
        int age = int.Parse(input);
        if(age<0|| age > 150)
        {
            throw new ArgumentOutOfRangeException(nameof(input), "Age must be between 0 and 150");

        }

        Console.WriteLine("Step 2(only if valid)");
        Console.WriteLine($"Result: {age}");

    }

    static void Main(string[] args)
    {


        try
        {
            Console.WriteLine("-- ParseAge(\"abc\") --");
            ParseAge("abc");
        }
        catch(FormatException ex)
        {
            Console.WriteLine($"Caught FormatException: {ex.Message}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Caught General Exception: {ex.Message}");
        }

        Console.WriteLine();


        // 2. Call with out-of-range number "200" (Correct Catch Block Order)
        Console.WriteLine("-- ParseAge(\"200\") --");
        try
        {
            ParseAge("200");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Caught ArgumentOutOfRangeException (most specific, ran first): {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Caught ArgumentException: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught Exception: {ex.Message}");
        }

        Console.WriteLine();


        Console.WriteLine("-- ParseAge(\"30\") --");
        try
        {
            ParseAge("30");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught Exception: {ex.Message}");
        }
    }
}
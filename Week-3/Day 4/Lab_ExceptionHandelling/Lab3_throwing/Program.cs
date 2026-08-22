using System;

class Lab3
{
    static int DivideInternal(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero in DivideInternal");
        }
        return a / b;
    }

    static int CallSiteGood(int a, int b)
    {
        try
        {
            return DivideInternal(a, b);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("[Good] Logging before rethrow...");
            throw; // Preserves the original stack trace
        }
    }

    static int CallSiteBad(int a, int b)
    {
        try
        {
            return DivideInternal(a, b);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("[Bad] Logging before rethrow...");
            throw ex; // Resets the stack trace to this line
        }
    }

    static void Validate(int value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative.");
        }
    }

    static void Main()
    {
        // 1. CallSiteGood test
        try
        {
            CallSiteGood(10, 0);
        }
        catch (Exception ex)
        {
            bool containsOrigin = ex.StackTrace != null && ex.StackTrace.Contains("DivideInternal");
            if (containsOrigin)
            {
                Console.WriteLine("Good stack trace mentions: DivideInternal\n");
            }
            else
            {
                Console.WriteLine("Good stack trace does not mention DivideInternal\n");
            }
        }

        // 2. CallSiteBad test
        try
        {
            CallSiteBad(10, 0);
        }
        catch (Exception ex)
        {
            bool containsOrigin = ex.StackTrace != null && ex.StackTrace.Contains("DivideInternal");
            Console.WriteLine($"Bad stack trace mentions DivideInternal: {containsOrigin}  (starts at CallSiteBad instead)\n");
        }

        // 3. Validate test
        try
        {
            Validate(-5);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Validate(-5) threw: {ex.Message}");
        }
    }
}
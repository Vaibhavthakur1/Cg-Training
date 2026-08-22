using System;

class Lab4
{
    static string ReadRawConfigValue(string key)
    {
        if (key == "timeout")
            throw new FormatException("Value 'abc' is not a valid integer");
        return "dummy-value";
    }

    static int GetTimeoutSetting()
    {
        try
        {
            string raw = ReadRawConfigValue("timeout");
            return int.Parse(raw);
        }
        catch (FormatException ex)
        {
            // Wrap low-level FormatException in a high-level InvalidOperationException
            throw new InvalidOperationException("Application configuration is invalid", ex);
        }
    }

    static void PrintExceptionChain(Exception ex)
    {
        Exception current = ex;
        int depth = 0;

        while (current != null)
        {
            string indent = new string(' ', depth * 2);
            Console.WriteLine($"{indent}{current.GetType().Name}: {current.Message}");
            current = current.InnerException;
            depth++;
        }
    }

    static void Main()
    {
        try
        {
            GetTimeoutSetting();
        }
        catch (Exception ex)
        {
            // 1. Direct InnerException inspection
            Console.WriteLine($"Top-level: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Caused by: {ex.InnerException.Message}");
                Console.WriteLine($"Inner exception type: {ex.InnerException.GetType().Name}");
            }

            Console.WriteLine();

            // 2. Exception chain inspection
            Console.WriteLine("-- PrintExceptionChain --");
            PrintExceptionChain(ex);
        }
    }
}
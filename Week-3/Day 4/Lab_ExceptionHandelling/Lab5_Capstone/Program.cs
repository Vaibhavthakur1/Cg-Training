using System;

public class OrderValidationException : Exception
{
    public string FieldName { get; } = string.Empty;

    public OrderValidationException() : base() { }
    public OrderValidationException(string message) : base(message) { }
    public OrderValidationException(string message, Exception inner) : base(message, inner) { }
    public OrderValidationException(string message, string fieldName) : base(message) => FieldName = fieldName;
}

public class MissingFieldException : OrderValidationException
{
    public MissingFieldException(string fieldName)
        : base($"Missing required field: {fieldName}", fieldName) { }
}

public class InvalidQuantityException : OrderValidationException
{
    public InvalidQuantityException(string fieldName)
        : base("Quantity must be greater than zero", fieldName) { }
}

class Lab5
{
    static decimal ValidateOrder(string customerName, int quantity, decimal unitPrice)
    {
        if (string.IsNullOrEmpty(customerName))
        {
            throw new MissingFieldException(nameof(customerName));
        }

        if (quantity <= 0)
        {
            throw new InvalidQuantityException(nameof(quantity));
        }

        if (unitPrice < 0)
        {
            throw new OrderValidationException("Unit price cannot be negative");
        }

        return quantity * unitPrice;
    }

    static void SaveOrder(string customerName, decimal total, bool simulateFailure)
    {
        if (simulateFailure)
        {
            throw new InvalidOperationException("Database unavailable");
        }
    }

    static void ProcessOrder(string customerName, int quantity, decimal unitPrice, bool simulateSaveFailure = false)
    {
        try
        {
            decimal total = ValidateOrder(customerName, quantity, unitPrice);

            try
            {
                SaveOrder(customerName, total, simulateSaveFailure);
                Console.WriteLine($"Order total: ${total}");
            }
            catch (InvalidOperationException ex)
            {
                // NOTE: 'throw;' cannot be used here because 'throw;' only rethrows 
                // the existing caught exception instance (InvalidOperationException). 
                // When translating/wrapping into a brand NEW exception (OrderValidationException), 
                // we must use 'throw new ...' and pass the caught exception as the inner exception.
                throw new OrderValidationException("Could not save order", ex);
            }
        }
        catch (MissingFieldException ex)
        {
            Console.WriteLine($"Missing field: {ex.FieldName}");
        }
        catch (InvalidQuantityException ex)
        {
            Console.WriteLine($"Invalid quantity for field: {ex.FieldName}");
        }
        catch (OrderValidationException ex)
        {
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Order validation failed: {ex.Message} (caused by: {ex.InnerException.Message})");
            }
            else
            {
                Console.WriteLine($"Order validation failed: {ex.Message}");
            }
        }
        finally
        {
            Console.WriteLine("Order attempt complete.");
        }
    }

    static void Main()
    {
        // 1. Missing customer name
        Console.WriteLine("-- Missing customer name --");
        ProcessOrder("", 2, 50.00m);
        Console.WriteLine();

        // 2. Zero quantity
        Console.WriteLine("-- Zero quantity --");
        ProcessOrder("Alice", 0, 50.00m);
        Console.WriteLine();

        // 3. Negative price
        Console.WriteLine("-- Negative price --");
        ProcessOrder("Alice", 2, -10.00m);
        Console.WriteLine();

        // 4. Valid order, SaveOrder fails
        Console.WriteLine("-- Valid order, SaveOrder fails --");
        ProcessOrder("Alice", 2, 50.00m, simulateSaveFailure: true);
        Console.WriteLine();

        // 5. Fully valid order
        Console.WriteLine("-- Fully valid order --");
        ProcessOrder("Alice", 4, 49.99m);
    }
}
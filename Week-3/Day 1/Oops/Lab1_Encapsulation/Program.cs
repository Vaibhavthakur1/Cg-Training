using System;

public class InventoryItem
{
    // Private backing fields
    private int _quantity;
    private decimal _unitPrice;

    // Name can only be set during construction
    public string Name { get; init; }

    // Quantity with validation
    public int Quantity
    {
        get { return _quantity; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Quantity cannot be negative");

            _quantity = value;
        }
    }

    // UnitPrice with validation
    public decimal UnitPrice
    {
        get { return _unitPrice; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("UnitPrice must be greater than zero");

            _unitPrice = value;
        }
    }

    // Read-only computed property
    public decimal TotalValue
    {
        get { return Quantity * UnitPrice; }
    }

    public InventoryItem(string name, int quantity, decimal unitPrice)
    {
        // Validate Name
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or whitespace");

        // Assign through properties so validation runs
        Name = name;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}

class Program
{
    static void Main()
    {
        InventoryItem item = new InventoryItem("Keyboard", 3, 45.00m);

        Console.WriteLine(
            $"Created: {item.Name}, Qty={item.Quantity}, " +
            $"Price=${item.UnitPrice:F2}, Total=${item.TotalValue:F2}"
        );

        try
        {
            item.Quantity = -5;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(
                $"Caught expected error setting Quantity=-5: {ex.Message}"
            );
        }

        try
        {
            item.UnitPrice = 0;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(
                $"Caught expected error setting UnitPrice=0: {ex.Message}"
            );
        }
    }
}
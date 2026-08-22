using System;
using System.Collections.Generic;

// Custom exception for inventory business logic failures
public class InsufficientStockException : Exception
{
    public string Sku { get; }
    public int Requested { get; }
    public int Available { get; }

    public InsufficientStockException(string sku, int requested, int available)
        : base($"Cannot fulfill sale for SKU '{sku}'. Requested: {requested}, Available: {available}.")
    {
        Sku = sku;
        Requested = requested;
        Available = available;
    }
}

public class InventoryManager
{
    private readonly Dictionary<string, int> _inventory = new(StringComparer.OrdinalIgnoreCase);

    public InventoryManager()
    {
        // 2. Pre-load 8 sample SKUs
        _inventory["SKU-KB-01"] = 45;   
        _inventory["SKU-MS-02"] = 12;   
        _inventory["SKU-MN-03"] = 4;    
        _inventory["SKU-HD-04"] = 80;   
        _inventory["SKU-CH-05"] = 2;    
        _inventory["SKU-CB-06"] = 150;  
        _inventory["SKU-HP-07"] = 5;    
        _inventory["SKU-MC-08"] = 25;   
    }

    // 3a. Restock:
    public void RestockItem(string sku, int quantity)
    {
        if (quantity <= 0)
        {
            Console.WriteLine($"[Restock Error] Quantity must be positive for '{sku}'.");
            return;
        }

        if (_inventory.TryGetValue(sku, out int currentStock))
        {
            _inventory[sku] = currentStock + quantity;
            Console.WriteLine($"[Restock] Updated '{sku}': {currentStock} -> {_inventory[sku]} units.");
        }
        else
        {
            _inventory[sku] = quantity;
            Console.WriteLine($"[Restock] Added new item '{sku}' with {quantity} units.");
        }
    }

    // 3b. SellItem: 
    public void SellItem(string sku, int quantity)
    {
        if (quantity <= 0)
        {
            Console.WriteLine($"[Sale Error] Quantity must be positive for '{sku}'.");
            return;
        }

        // Handle missing key gracefully without throwing KeyNotFoundException
        if (!_inventory.TryGetValue(sku, out int currentStock))
        {
            throw new InsufficientStockException(sku, quantity, 0);
        }

        if (currentStock < quantity)
        {
            throw new InsufficientStockException(sku, quantity, currentStock);
        }

        _inventory[sku] = currentStock - quantity;
        Console.WriteLine($"[Sale] Successfully sold {quantity} units of '{sku}'. Remaining: {_inventory[sku]}.");
    }

    // 3c. LowStockReport: returns SKUs below threshold via KeyValuePair iteration
    public List<KeyValuePair<string, int>> LowStockReport(int threshold)
    {
        List<KeyValuePair<string, int>> lowStockItems = new();

        foreach (KeyValuePair<string, int> entry in _inventory)
        {
            if (entry.Value < threshold)
            {
                lowStockItems.Add(entry);
            }
        }

        return lowStockItems;
    }

    public void DisplayInventory()
    {
        Console.WriteLine("\n--- Current Inventory Status ---");
        foreach (var (sku, qty) in _inventory)
        {
            Console.WriteLine($"SKU: {sku,-12} | Quantity: {qty,3}");
        }
        Console.WriteLine("--------------------------------\n");
    }
}

class Program
{
    static void Main()
    {
        InventoryManager manager = new();
        manager.DisplayInventory();

        // 1. Demonstrate successful restock (both existing SKU and brand new SKU)
        Console.WriteLine("=== 1. Restock Demonstrations ===");
        manager.RestockItem("SKU-MN-03", 10);      
        manager.RestockItem("SKU-DK-09", 30);     
        Console.WriteLine();

        // 2. Demonstrate successful sale
        Console.WriteLine("=== 2. Successful Sale Demonstration ===");
        try
        {
            manager.SellItem("SKU-MS-02", 5);
        }
        catch (InsufficientStockException ex)
        {
            Console.WriteLine($"[Caught Exception] {ex.Message}");
        }
        Console.WriteLine();

        // 3. Demonstrate attempted oversell & missing SKU (caught and reported)
        Console.WriteLine("=== 3. Oversell / Missing SKU Demonstrations ===");
        try
        {
            // Overselling SKU-CH-05 (only 2 in stock)
            manager.SellItem("SKU-CH-05", 10);
        }
        catch (InsufficientStockException ex)
        {
            Console.WriteLine($"[Caught Expected Exception] {ex.Message}");
        }

        try
        {
            // Attempting to sell an SKU that doesn't exist
            manager.SellItem("SKU-NON-EXISTENT", 1);
        }
        catch (InsufficientStockException ex)
        {
            Console.WriteLine($"[Caught Expected Exception] {ex.Message}");
        }
        Console.WriteLine();

        // 4. Demonstrate Low-Stock Report (Threshold: 10 units)
        Console.WriteLine("=== 4. Low-Stock Report (Threshold < 10) ===");
        int threshold = 10;
        var report = manager.LowStockReport(threshold);

        foreach (var item in report)
        {
            Console.WriteLine($"ALERT: {item.Key} has critically low stock ({item.Value} remaining)");
        }
    }
}
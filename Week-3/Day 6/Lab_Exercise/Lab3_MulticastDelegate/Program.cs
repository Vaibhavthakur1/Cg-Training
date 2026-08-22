using System;

namespace Lab3MulticastDelegates
{
    // 1. Delegate declaration
    public delegate void OrderEvent(string orderId);

    public class Program
    {
        // 2. Separate handler methods
        public static void LogToConsole(string orderId) =>
            Console.WriteLine($"[Log] Order {orderId} logged to console.");

        public static void SendEmailSimulation(string orderId) =>
            Console.WriteLine($"[Email] Confirmation email sent for order {orderId}.");

        public static void UpdateInventorySimulation(string orderId) =>
            Console.WriteLine($"[Inventory] Stock levels updated for order {orderId}.");

        public static void Main()
        {
            // 3. Combine into Multicast Delegate via += and Invoke
            Console.WriteLine("=== 3. Multicast Invocation (All 3 Handlers) ===");
            OrderEvent? onOrderProcessed = null;
            onOrderProcessed += LogToConsole;
            onOrderProcessed += SendEmailSimulation;
            onOrderProcessed += UpdateInventorySimulation;

            onOrderProcessed("ORD-1001");
            Console.WriteLine();

            // 4. Remove a Handler via -= and Invoke
            Console.WriteLine("=== 4. After Removing SendEmailSimulation (-=) ===");
            onOrderProcessed -= SendEmailSimulation;

            onOrderProcessed("ORD-1001");
            Console.WriteLine();

            // 5. Lambda Unsubscription Pitfall & Resolution
            Console.WriteLine("=== 5. Lambda Unsubscription Pitfall & Fix ===");

            OrderEvent? pipeline = null;

            // Subscribe two identical lambda bodies
            pipeline += id => Console.WriteLine($"[Lambda Handler 1] Processing order {id}");
            pipeline += id => Console.WriteLine($"[Lambda Handler 2] Processing order {id}");

            // Pitfall: Creating a new lambda creates a distinct compiler-generated delegate instance
            Console.WriteLine("-- Pitfall: Attempting -= with an inline, unstored lambda --");
            pipeline -= id => Console.WriteLine($"[Lambda Handler 1] Processing order {id}");

            // Both handlers still fire because delegate equality comparison failed
            pipeline("ORD-2002");
            Console.WriteLine();

            // Fix: Store the lambda instance in a variable first
            Console.WriteLine("-- Fix: Storing the delegate reference for reliable removal --");
            OrderEvent storedAuditHandler = id => Console.WriteLine($"[Stored Handler] Audit log for {id}");

            pipeline += storedAuditHandler;
            Console.WriteLine("> Subscribed storedAuditHandler. Invoking:");
            pipeline("ORD-3003");
            Console.WriteLine();

            pipeline -= storedAuditHandler;
            Console.WriteLine("> Unsubscribed storedAuditHandler. Invoking:");
            pipeline("ORD-3003");
        }
    }
}
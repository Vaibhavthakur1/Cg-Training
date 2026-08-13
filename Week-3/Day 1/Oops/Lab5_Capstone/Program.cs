using System;
using System.Collections.Generic;
using System.Linq;

public interface IIdentifiable
{
    string Id { get; }
}

public interface IPaymentMethod : IIdentifiable
{
    string DisplayName { get; }

    PaymentResult Charge(decimal amount);
}

public class PaymentResult
{
    public bool Success { get; }
    public string Message { get; }

    public PaymentResult(bool success, string message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        Success = success;
        Message = message;
    }
}

public abstract class PaymentMethodBase : IPaymentMethod
{
    public string Id { get; }
    public string DisplayName { get; }

    protected PaymentMethodBase(string id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    public abstract PaymentResult Charge(decimal amount);
}

public class CreditCardPayment : PaymentMethodBase
{
    public CreditCardPayment(string id, string displayName)
        : base(id, displayName)
    {
    }

    public override PaymentResult Charge(decimal amount)
    {
        if (amount > 5000)
        {
            return new PaymentResult(
                false,
                "Credit card limit exceeded"
            );
        }

        return new PaymentResult(
            true,
            "Credit card payment successful"
        );
    }
}

public sealed class CashPayment : PaymentMethodBase
{
    public CashPayment(string id, string displayName)
        : base(id, displayName)
    {
    }

    public override PaymentResult Charge(decimal amount)
    {
        return new PaymentResult(
            true,
            "Cash payment successful"
        );
    }
}

class Program
{
    static void Main()
    {
        // Create different payment methods
        List<IPaymentMethod> payments = new List<IPaymentMethod>
        {
            new CreditCardPayment("CC-1", "Visa"),
            new CashPayment("CASH-1", "Cash Drawer"),
            new CreditCardPayment("CC-1", "Visa"),
            new CashPayment("CASH-1", "Cash Drawer")
        };

        // Charge each payment method
        var results = payments
            .Select((payment, index) =>
            {
                decimal amount;

                if (index == 0)
                    amount = 1500;
                else if (index == 1)
                    amount = 1500;
                else if (index == 2)
                    amount = 6000;
                else
                    amount = 6000;

                PaymentResult result = payment.Charge(amount);

                return new
                {
                    Id = payment.Id,
                    DisplayName = payment.DisplayName,
                    Attempted = amount,
                    Success = result.Success
                };
            })
            .ToList();

        // Print report
        foreach (var result in results)
        {
            Console.WriteLine(
                $"{result.Id} {result.DisplayName,-15} " +
                $"Attempted={result.Attempted:F2} " +
                $"Success={result.Success}"
            );
        }

        // Calculate successfully settled amount
        decimal totalSuccessfullySettled = results
            .Where(r => r.Success)
            .Sum(r => r.Attempted);

        Console.WriteLine();
        Console.WriteLine(
            $"Total successfully settled: {totalSuccessfullySettled:F2}"
        );
    }
}
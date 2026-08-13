using System;

public class TaxCalculator
{
    public virtual decimal CalculateTax(decimal amount)
    {
        return amount * 0.1m;
    }
}

public class RegionalTaxCalculator : TaxCalculator
{
    // Override the parent method and prevent further overriding
    public sealed override decimal CalculateTax(decimal amount)
    {
        return amount * 0.12m;
    }
}

// This would produce a compiler error because CalculateTax()
// is sealed in RegionalTaxCalculator.

// public class InvalidTaxCalculator : RegionalTaxCalculator
// {
//     public override decimal CalculateTax(decimal amount)
//     {
//         return amount * 0.20m;
//     }
// }

public sealed class FixedDiscountCalculator
{
    public decimal ApplyDiscount(decimal price)
    {
        return price * 0.9m;
    }
}

// This would produce a compiler error because
// FixedDiscountCalculator itself is sealed.

// public class InvalidDiscountCalculator : FixedDiscountCalculator
// {
// }

class Program
{
    static void Main()
    {
        RegionalTaxCalculator regionalTax =
            new RegionalTaxCalculator();

        decimal tax = regionalTax.CalculateTax(200);

        Console.WriteLine(
            $"RegionalTaxCalculator.CalculateTax(200) -> {tax:F2}"
        );

        FixedDiscountCalculator discountCalculator =
            new FixedDiscountCalculator();

        decimal discountedPrice =
            discountCalculator.ApplyDiscount(50);

        Console.WriteLine(
            $"FixedDiscountCalculator.ApplyDiscount(50) -> {discountedPrice:F2}"
        );
    }
}
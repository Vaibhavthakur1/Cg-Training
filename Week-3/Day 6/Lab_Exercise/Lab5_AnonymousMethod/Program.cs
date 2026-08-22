using System;

public class Program
    {
        public static void Main()
        {
            // 1. Anonymous Method using the 'delegate' keyword
            Console.WriteLine("=== 1. Anonymous Method (delegate keyword) ===");
            Action<int> printSquareDelegate = delegate (int x)
            {
                Console.WriteLine($"Square of {x}: {x * x}");
            };

            printSquareDelegate(6);
            Console.WriteLine();

            // 2. Closure Capturing and Mutating Outer Variable
            Console.WriteLine("=== 2. Closure Mutation with Anonymous Method ===");
            int total = 0;

            Action incrementTotalDelegate = delegate
            {
                total++;
            };

            for (int i = 0; i < 5; i++)
            {
                incrementTotalDelegate();
            }

            Console.WriteLine($"Outer variable 'total' after 5 calls: {total}");
            Console.WriteLine();

            // 3. Rewritten as Lambdas (Confirming Identical Behavior)
            Console.WriteLine("=== 3. Equivalent Lambda Implementations ===");

            // Lambda equivalent for (1): expression body, inferred parameter type
            Action<int> printSquareLambda = x => Console.WriteLine($"Square of {x}: {x * x}");
            printSquareLambda(6);

            // Lambda equivalent for (2): concise closure syntax
            int lambdaTotal = 0;
            Action incrementTotalLambda = () => lambdaTotal++;

            for (int i = 0; i < 5; i++)
            {
                incrementTotalLambda();
            }

            Console.WriteLine($"Outer variable 'lambdaTotal' after 5 calls: {lambdaTotal}");
        }
}



using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string text = "Order #4521 was shipped. order #99 is pending. ORDER #12345 was cancelled.";

        // TODO 1: Find all order numbers
        MatchCollection orders = Regex.Matches(
            text,
            @"Order #(\d+)",
            RegexOptions.IgnoreCase
        );

        Console.Write("Order numbers found: ");

        foreach (Match order in orders)
        {
            Console.Write(order.Groups[1].Value + " ");
        }

        Console.WriteLine();


        // TODO 2: Mask credit card numbers
        string cardText = "Card on file: 4111-1111-1111-1234";

        string maskedCard = Regex.Replace(
            cardText,
            @"\b(?:\d{4}[- ]?){3}\d{4}\b",
            "XXXX-XXXX-XXXX-1234"
        );

        Console.WriteLine("Masked card: " + maskedCard);


        // TODO 3: Change "lastname, firstname" to "firstname lastname"
        string names = "Smith, John";

        string newName = Regex.Replace(
            names,
            @"(\w+),\s*(\w+)",
            "$2 $1"
        );

        Console.WriteLine("Reformatted name: " + newName);


        // TODO 4: Split tags and remove extra spaces
        string tags = "red, blue;green , yellow";

        string[] tagArray = Regex.Split(tags, @"[,;]");

        Console.Write("Tags: [");

        for (int i = 0; i < tagArray.Length; i++)
        {
            tagArray[i] = tagArray[i].Trim();

            Console.Write(tagArray[i]);

            if (i < tagArray.Length - 1)
            {
                Console.Write(", ");
            }
        }

        Console.WriteLine("]");
    }
}
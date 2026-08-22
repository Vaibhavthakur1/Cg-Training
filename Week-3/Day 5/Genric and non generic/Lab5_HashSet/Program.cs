using System;
using System.Collections.Generic;

class CustomerOverlapAnalyzer
{
    static void PrintSet(string title, HashSet<string> set)
    {
        Console.WriteLine($"\n--- {title} (Count: {set.Count}) ---");
        foreach (var email in set)
        {
            Console.WriteLine($" • {email}");
        }
    }

    static void Main()
    {

        HashSet<string> newsletterSubscribers = new(StringComparer.OrdinalIgnoreCase)
        {
            "alice@example.com",
            "bob@example.com",
            "charlie@example.com",
            "david@example.com",
            "eva@example.com"
        };

        HashSet<string> appUsers = new(StringComparer.OrdinalIgnoreCase)
        {
            "bob@example.com",
            "charlie@example.com",
            "frank@example.com",
            "grace@example.com"
        };

        PrintSet("Initial Newsletter Subscribers", newsletterSubscribers);
        PrintSet("Initial App Users", appUsers);

        // 1. Both Subscribers and App Users 
        HashSet<string> both = new(newsletterSubscribers, StringComparer.OrdinalIgnoreCase);
        both.IntersectWith(appUsers);
        PrintSet("Customers Who Are BOTH Subscribers and App Users (IntersectWith)", both);

        // 2. Subscribers but NOT App Users 
        HashSet<string> subscribersOnly = new(newsletterSubscribers, StringComparer.OrdinalIgnoreCase);
        subscribersOnly.ExceptWith(appUsers);
        PrintSet("Subscribers BUT NOT App Users (ExceptWith)", subscribersOnly);

        // 3. All Unique Customers Across Both Lists
        HashSet<string> allCustomers = new(newsletterSubscribers, StringComparer.OrdinalIgnoreCase);
        allCustomers.UnionWith(appUsers);
        PrintSet("All Unique Customers Across Both Lists (UnionWith)", allCustomers);

        // 4. Subset Check (IsSubsetOf)
        bool isSubset = newsletterSubscribers.IsSubsetOf(appUsers);
        Console.WriteLine($"\n--- Subset Analysis ---");
        Console.WriteLine($"Is 'NewsletterSubscribers' a subset of 'AppUsers'? : {isSubset}");

        // Demonstrating a true subset scenario
        HashSet<string> powerUsers = new(StringComparer.OrdinalIgnoreCase) { "bob@example.com", "charlie@example.com" };
        Console.WriteLine($"Is '{string.Join(", ", powerUsers)}' a subset of 'AppUsers'? : {powerUsers.IsSubsetOf(appUsers)}");


        // Part 3: List Deduplication Benchmark
        Console.WriteLine("\n==========================================");
        Console.WriteLine("     LIST DEDUPLICATION REPORT            ");
        Console.WriteLine("==========================================");

        string[] emailPool = {
            "user1@domain.com", "user2@domain.com", "user3@domain.com",
            "user4@domain.com", "user5@domain.com", "user6@domain.com",
            "user7@domain.com", "user8@domain.com", "user9@domain.com",
            "user10@domain.com"
        };

        Random random = new Random(42); // Seeded for reproducibility
        List<string> rawEmailList = new List<string>(100);

        for (int i = 0; i < 100; i++)
        {
            // Randomly picks from the 10-email pool to create heavy intentional duplication
            rawEmailList.Add(emailPool[random.Next(emailPool.Length)]);
        }

        // Deduplicate into HashSet
        HashSet<string> uniqueEmails = new HashSet<string>(rawEmailList, StringComparer.OrdinalIgnoreCase);

        int originalCount = rawEmailList.Count;
        int uniqueCount = uniqueEmails.Count;
        int duplicatesRemoved = originalCount - uniqueCount;

        Console.WriteLine($"Total emails in raw list    : {originalCount}");
        Console.WriteLine($"Unique emails after dedupe  : {uniqueCount}");
        Console.WriteLine($"Total duplicates removed    : {duplicatesRemoved}");
    }
}
using System;
using System.Globalization;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // TODO 1: Named groups for date/time/level/message, print each
        string logLine = "2026-08-14 09:15:32 ERROR Connection timed out";

        string logPattern = @"^(?<date>\d{4}-\d{2}-\d{2})\s+(?<time>\d{2}:\d{2}:\d{2})\s+(?<level>\w+)\s+(?<message>.+)$";
        Match logMatch = Regex.Match(logLine, logPattern);

        if (logMatch.Success)
        {
            Console.WriteLine(
                $"date={logMatch.Groups["date"].Value}, " +
                $"time={logMatch.Groups["time"].Value}, " +
                $"level={logMatch.Groups["level"].Value}, " +
                $"message={logMatch.Groups["message"].Value}"
            );
        }

        // TODO 2: Named groups (?<key>...) and (?<value>...), print all pairs
        
        string kvText = "name=Alice;age=30;city=NYC";

        string kvPattern = @"(?<key>[^=;]+)=(?<value>[^=;]+)";
        MatchCollection kvMatches = Regex.Matches(kvText, kvPattern);

        foreach (Match m in kvMatches)
        {
            Console.WriteLine($"{m.Groups["key"].Value}={m.Groups["value"].Value}");
        }

        // TODO 3: MatchEvaluator - format numbers with thousands separators
        string numbers = "Revenue: 1234567, Costs: 89000";

        string formattedNumbers = Regex.Replace(
            numbers,
            @"\b\d+\b",
            match => long.Parse(match.Value).ToString("#,##0", CultureInfo.InvariantCulture)
        );

        Console.WriteLine(formattedNumbers);

        // TODO 4: MatchEvaluator - convert ALL CAPS words to Title Case
        string shouting = "THIS IS URGENT please respond";

        string fixedCasing = Regex.Replace(
            shouting,
            @"\b[A-Z]{2,}\b",
            match => char.ToUpper(match.Value[0]) + match.Value.Substring(1).ToLower()
        );

        Console.WriteLine(fixedCasing);
    }
}
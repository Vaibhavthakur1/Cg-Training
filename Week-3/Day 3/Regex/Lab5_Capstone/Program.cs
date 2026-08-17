using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class LogEntry
{
    public string Date { get; init; } = string.Empty;
    public string Time { get; init; } = string.Empty;
    public string Level { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}

class Program
{
    static void Main()
    {
        string rawLog = @"2026-08-14 09:15:00 INFO Service started
2026-08-14 09:16:12 WARN Disk usage high
2026-08-14 09:17:45 ERROR Request failed code=404
2026-08-14 09:18:03 INFO Request completed
2026-08-14 09:19:22 ERROR Upstream error code=500
2026-08-14 09:20:00 INFO Shutdown complete";

        // Parse log entries
        List<LogEntry> entries = ParseLog(rawLog);

        // Summarize log counts using LINQ
        Console.WriteLine($"Parsed {entries.Count} entries.");

        var counts = entries.GroupBy(e => e.Level)
                            .ToDictionary(g => g.Key, g => g.Count());

        string summary = string.Join(", ", counts.Select(kv => $"{kv.Key}: {kv.Value}"));
        Console.WriteLine($"Summary: {summary}\n");

        // Print redacted log
        Console.WriteLine("--- Redacted log ---");
        string redactedLog = RedactErrorCodes(rawLog);
        Console.WriteLine(redactedLog);
    }

    public static List<LogEntry> ParseLog(string rawLog)
    {
        string pattern = @"^(?<Date>\d{4}-\d{2}-\d{2})\s+(?<Time>\d{2}:\d{2}:\d{2})\s+(?<Level>INFO|WARN|ERROR)\s+(?<Message>.*)$";
        MatchCollection matches = Regex.Matches(rawLog, pattern, RegexOptions.Multiline);

        var logEntries = new List<LogEntry>();

        foreach (Match match in matches)
        {
            logEntries.Add(new LogEntry
            {
                Date = match.Groups["Date"].Value,
                Time = match.Groups["Time"].Value,
                Level = match.Groups["Level"].Value,
                Message = match.Groups["Message"].Value
            });
        }

        return logEntries;
    }

    public static string RedactErrorCodes(string rawLog)
    {
        // Target lines containing ERROR
        string linePattern = @"^.*?\bERROR\b.*$";

        return Regex.Replace(rawLog, linePattern, lineMatch =>
        {
            // Replace numeric code values with fixed mask inside ERROR lines
            return Regex.Replace(lineMatch.Value, @"code=\d+", "code=###");
        }, RegexOptions.Multiline);
    }
}
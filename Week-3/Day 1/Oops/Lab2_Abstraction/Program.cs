using System;
using System.Collections.Generic;
using System.Linq;

public abstract class NotificationChannel
{
    public bool TrySend(string message)
    {
        try
        {
            return Send(message);
        }
        catch
        {
            return false;
        }
    }

    protected abstract bool Send(string message);
}

public class EmailChannel : NotificationChannel
{
    protected override bool Send(string message)
    {
        // Email always succeeds
        return true;
    }
}

public class SmsChannel : NotificationChannel
{
    protected override bool Send(string message)
    {
        // SMS fails if message is longer than 160 characters
        if (message.Length > 160)
            throw new Exception("SMS message is too long");

        return true;
    }
}

class Program
{
    static void Main()
    {
        // Create different notification channels
        List<NotificationChannel> channels = new List<NotificationChannel>
        {
            new EmailChannel(),
            new SmsChannel(),
            new EmailChannel(),
            new SmsChannel()
        };

        // Short message
        string shortMessage = "Hello, this is a short notification.";

        // Long message - more than 160 characters
        string longMessage =
            "This is a very long notification message that is intentionally " +
            "created to contain more than one hundred and sixty characters " +
            "so that the SMS channel will fail when trying to send it.";

        // Send messages
        var results = new List<(NotificationChannel Channel, bool Success)>();

        results.Add((channels[0], channels[0].TrySend(shortMessage)));
        results.Add((channels[1], channels[1].TrySend(shortMessage)));
        results.Add((channels[2], channels[2].TrySend(longMessage)));
        results.Add((channels[3], channels[3].TrySend(longMessage)));

        // LINQ + anonymous type
        var report = results.Select(r => new
        {
            ChannelType = r.Channel.GetType().Name,Success = r.Success
        });

        // Print report
        foreach (var result in report)
        {
            Console.WriteLine(
                $"{result.ChannelType}: {(result.Success ? "Success" : "Failed")}"
            );
        }

        // Count successes and failures
        int succeeded = report.Count(r => r.Success);
        int failed = report.Count(r => !r.Success);

        Console.WriteLine();
        Console.WriteLine($"Succeeded: {succeeded}, Failed: {failed}");
    }
}
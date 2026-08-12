using System;
using System.Collections.Generic;
using System.Text;

class LogEntry
{
    public DateTime Timestamp { get; set; }
    public string LogLevel { get; set; }
    public string Message { get; set; }
    public Exception Exception { get; set; }

    public LogEntry(DateTime timestamp, string logLevel,
                    string message, Exception exception = null)
    {
        Timestamp = timestamp;
        LogLevel = logLevel;
        Message = message;
        Exception = exception;
    }
}

class LogProcessor
{
    private StringBuilder buffer = new StringBuilder();
    private List<LogEntry> errorLogs = new List<LogEntry>();

    private int bufferCapacity;

    public LogProcessor(int bufferCapacity)
    {
        this.bufferCapacity = bufferCapacity;
    }

    public void ProcessLog(LogEntry log)
    {
        // Use StringBuilder to construct the log message
        StringBuilder logMessage = new StringBuilder();

        logMessage.Append("[");
        logMessage.Append(log.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
        logMessage.Append("] ");

        logMessage.Append(log.LogLevel);
        logMessage.Append(": ");
        logMessage.Append(log.Message);

        // Add exception information if available
        if (log.Exception != null)
        {
            logMessage.Append(" | Exception: ");
            logMessage.Append(log.Exception.Message);
        }

        // Add formatted log to the buffer
        buffer.AppendLine(logMessage.ToString());

        // Store Error logs separately
        if (log.LogLevel == "ERROR")
        {
            errorLogs.Add(log);
        }

        // Flush when buffer reaches capacity
        if (buffer.Length >= bufferCapacity)
        {
            FlushBuffer();
        }
    }

    public void FlushBuffer()
    {
        if (buffer.Length == 0)
            return;

        Console.WriteLine("----- BUFFER FLUSH -----");
        Console.Write(buffer.ToString());

        // Clear the buffer after flushing
        buffer.Clear();

        Console.WriteLine("----- BUFFER CLEARED -----\n");
    }

    public void DisplayErrorSummary()
    {
        Console.WriteLine("\n===== ERROR SUMMARY =====");

        Console.WriteLine("Total Errors: " + errorLogs.Count);

        foreach (LogEntry error in errorLogs)
        {
            Console.WriteLine(
                $"{error.Timestamp:yyyy-MM-dd HH:mm:ss} - {error.Message}"
            );

            if (error.Exception != null)
            {
                Console.WriteLine(
                    $"Exception: {error.Exception.Message}"
                );
            }
        }
    }
}

class Program
{
    static void Main()
    {
        LogProcessor processor = new LogProcessor(150);

        // Create log entries
        LogEntry log1 = new LogEntry(
            DateTime.Now,
            "INFO",
            "Application started"
        );

        LogEntry log2 = new LogEntry(
            DateTime.Now,
            "INFO",
            "User logged in"
        );

        LogEntry log3 = new LogEntry(
            DateTime.Now,
            "ERROR",
            "Database connection failed",
            new Exception("Unable to connect to database")
        );

        LogEntry log4 = new LogEntry(
            DateTime.Now,
            "WARNING",
            "Memory usage is high"
        );

        LogEntry log5 = new LogEntry(
            DateTime.Now,
            "ERROR",
            "File processing failed",
            new Exception("File not found")
        );

        // Process logs
        processor.ProcessLog(log1);
        processor.ProcessLog(log2);
        processor.ProcessLog(log3);
        processor.ProcessLog(log4);
        processor.ProcessLog(log5);

        // Flush remaining logs
        processor.FlushBuffer();

        // Display errors
        processor.DisplayErrorSummary();
    }
}
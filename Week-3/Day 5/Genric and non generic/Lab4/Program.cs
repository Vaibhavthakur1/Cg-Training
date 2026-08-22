using System;
using System.Collections.Generic;

public static class ParenthesesChecker
{
    public static bool IsBalanced(string expression)
    {
        if (string.IsNullOrEmpty(expression))
            return true;

        Stack<char> stack = new Stack<char>();

        foreach (char ch in expression)
        {
            // Push opening brackets onto the stack
            if (ch is '(' or '{' or '[')
            {
                stack.Push(ch);
            }
            // Check closing brackets against the top of the stack
            else if (ch is ')' or '}' or ']')
            {
                // Unmatched closing bracket (stack empty)
                if (stack.Count == 0)
                    return false;

                char top = stack.Pop();

                // Check for bracket mismatch
                if ((ch == ')' && top != '(') ||
                    (ch == '}' && top != '{') ||
                    (ch == ']' && top != '['))
                {
                    return false;
                }
            }
        }

        // If stack is empty, all opening brackets were properly closed
        return stack.Count == 0;
    }
}

// 4B: Print Job Queue (Queue)
public record PrintJob(string DocumentName, int Pages, bool IsHighPriority = false);

public class Printer
{
    
    private readonly Queue<PrintJob> _highPriorityQueue = new Queue<PrintJob>();
    private readonly Queue<PrintJob> _normalPriorityQueue = new Queue<PrintJob>();

    public int TotalPendingJobs => _highPriorityQueue.Count + _normalPriorityQueue.Count;

    public void EnqueueJob(PrintJob job)
    {
        if (job.IsHighPriority)
        {
            _highPriorityQueue.Enqueue(job);
            Console.WriteLine($"[PRIORITY ENQUEUE] '{job.DocumentName}' ({job.Pages} pages) placed in High-Priority Queue!");
        }
        else
        {
            _normalPriorityQueue.Enqueue(job);
            Console.WriteLine($"[ENQUEUE] '{job.DocumentName}' ({job.Pages} pages) added to normal queue.");
        }
    }

    public PrintJob PeekNextJob()
    {
        if (_highPriorityQueue.Count > 0)
            return _highPriorityQueue.Peek();

        if (_normalPriorityQueue.Count > 0)
            return _normalPriorityQueue.Peek();

        return null;
    }

    public void ProcessAllJobs()
    {
        Console.WriteLine("\n--- Starting Print Queue Processing ---");

        while (TotalPendingJobs > 0)
        {
            // Peek before dequeue
            PrintJob nextJob = PeekNextJob();
            string priorityTag = nextJob.IsHighPriority ? "[HIGH PRIORITY] " : "";
            Console.WriteLine($"Now printing next: {priorityTag}'{nextJob.DocumentName}' ({nextJob.Pages} pages)...");

            // Dequeue from high-priority first, fall back to normal queue
            PrintJob activeJob = _highPriorityQueue.Count > 0
                ? _highPriorityQueue.Dequeue()
                : _normalPriorityQueue.Dequeue();

            Console.WriteLine($"-> Finished printing '{activeJob.DocumentName}'.\n");
        }

        Console.WriteLine("--- All print jobs completed. ---");
    }
}

class Program
{
    static void Main()
    {
    
        Console.WriteLine("   4A: BALANCED PARENTHESES CHECKER (STACK)       ");
        string[] testExpressions = {
            "{[a+(b*c)]-d}",            // Balanced
            "((a + b) * [c - d])",      // Balanced
            "{[(])}",                   // Mismatched nesting
            "([{}])",                   // Balanced
            "(((a + b)",                // Missing closing brackets
            "a + b) - c",               // Premature closing bracket
            ""                          // Empty (Balanced)
        };

        foreach (var expr in testExpressions)
        {
            bool result = ParenthesesChecker.IsBalanced(expr);
            Console.WriteLine($"Expression: {expr,-22} | Is Balanced: {result}");
        }

        Console.WriteLine("\n");

        // Run Simulation 4B: Print Job Queue Simulation
        Console.WriteLine("   4B: PRINTER QUEUE SIMULATION (QUEUE)           ");
        Printer printer = new Printer();

        // 1. Enqueue 5 normal print jobs
        printer.EnqueueJob(new PrintJob("QuarterlyReport.pdf", 14));
        printer.EnqueueJob(new PrintJob("Contract_v2.docx", 6));
        printer.EnqueueJob(new PrintJob("DesignMockup.png", 2));
        printer.EnqueueJob(new PrintJob("Invoice_10492.pdf", 1));
        printer.EnqueueJob(new PrintJob("ProjectRoadmap.pptx", 30));

        // 2. High-priority interrupt arrives
        Console.WriteLine();
        printer.EnqueueJob(new PrintJob("URGENT_BoardMeeting_Brief.pdf", 4, IsHighPriority: true));

        // 3. Process jobs (Priority job will process before remaining normal jobs)
        printer.ProcessAllJobs();
    }
}
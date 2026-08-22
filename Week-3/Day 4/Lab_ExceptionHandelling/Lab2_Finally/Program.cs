using System;

class FakeFileHandle : IDisposable
{
    public FakeFileHandle() => Console.WriteLine("Handle opened");
    public void Dispose() => Console.WriteLine("Handle closed");
}

class Program
{
    static void Process(int mode)
    {
        Console.WriteLine("Opening");
        try
        {
            if (mode == 1) throw new InvalidOperationException("Simulated failure");
            Console.WriteLine("Working");
            if (mode == 2) return;
            Console.WriteLine("Finishing normally");
        }
        finally
        {
            Console.WriteLine("Closing");
        }
    }


    static void UseResource()
    {
        using (var handle = new FakeFileHandle())
        {
            Console.WriteLine("Working with resource...");
            throw new Exception("Error during resource usage");
        }
    }

    static void Main()
    {
        Console.WriteLine("-- Process(0) --");
        Process(0);
        Console.WriteLine();


        Console.WriteLine("-- Process(1) --");
        try
        {
            Process(1);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Caught in Main: {ex.Message}");
        }
        Console.WriteLine();
        // 3. Early return path (mode 2)
        Console.WriteLine("-- Process(2) --");
        Process(2);
        Console.WriteLine();

        // 4. Using statement with IDisposable cleanup demonstration
        Console.WriteLine("-- Using / Dispose --");
        try
        {
            UseResource();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught in Main: {ex.Message}");
        }

    }

}
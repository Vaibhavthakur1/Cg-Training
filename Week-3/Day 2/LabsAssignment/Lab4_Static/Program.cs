using System;

public static class StringUtils
{
    public static bool IsPalindrome(string s)
    {
        string reversed = Reverse(s);
        return s.Equals(reversed, StringComparison.OrdinalIgnoreCase);
    }

    public static string Reverse(string s)
    {
        char[] ch = s.ToCharArray();
        Array.Reverse(ch);

        return new string(ch);
    }

    public static int WordCount(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            return 0;

        return s.Split(new[] { ' ' },
           StringSplitOptions.RemoveEmptyEntries
       ).Length;
    }
}

public class TrackedWidget
{
    public Guid InstanceId { get; }
    public static int LiveCount { get; private set; }


    public TrackedWidget()
    {
        InstanceId = Guid.NewGuid();
        LiveCount++;
    }
    public void Dispose() => LiveCount--;
    public void PrintInfo() => Console.WriteLine($"Widget {InstanceId}: LiveCount={LiveCount}");

}
class Program
{
    static void Main(string[] args)
    {
        // StringUtils
        Console.WriteLine(
            $"IsPalindrome(\"racecar\") -> {StringUtils.IsPalindrome("racecar")}"
        );


        Console.WriteLine(
           $"Reverse(\"Hello\") -> {StringUtils.Reverse("Hello")}"
       );

        Console.WriteLine(
           $"WordCount(\"the quick brown fox\") -> {StringUtils.WordCount("the quick brown fox")}"
       );

        // creating instance of class
        TrackedWidget w1 = new TrackedWidget();
        TrackedWidget w2 = new TrackedWidget();
        TrackedWidget w3 = new TrackedWidget();

        Console.WriteLine(
            $"LiveCount after creating 3 widgets: {TrackedWidget.LiveCount}"
        );

        w1.PrintInfo();
        w2.PrintInfo();
        w3.PrintInfo();

        w1.Dispose();
        w2.Dispose();
        Console.WriteLine(
           $"LiveCount after disposing 2: {TrackedWidget.LiveCount}"
       );


    }
}
using System;

class Appointment
{
    public string Title { get; }
    public DateTime Start { get; }
    public TimeSpan Duration { get; }
    public string Location { get; }
    public static int DefaultDurationMinutes;


    static Appointment()
    {
        Console.WriteLine("Appointment type initialized. Default duration set to 30 minutes.");

        DefaultDurationMinutes = 30;
    }

    public Appointment(string title, DateTime start, TimeSpan duration, string location)
    {
        Title = title;
        Start = start;
        Duration = duration;
        Location = location;
    }

    //constructor with two argument
    public Appointment(string title, DateTime start)
       : this(
           title,
           start,
           TimeSpan.FromMinutes(30),
           "TBD")
    {
    }

    // 1-argument constructor
    // Chains to the 2-argument constructor
    public Appointment(string title)
        : this(title, DateTime.Now.AddDays(1))
    {
    }

    // Display appointment details
    public void PrintDetails()
    {
        Console.WriteLine(
            $"{Title} @ {Start:yyyy-MM-dd HH:mm}, {Duration.TotalMinutes:0} min, {Location}"
        );
    }

    
}
public class Program
{
    public static void Main()
    {
        // Full constructor
        Appointment full = new Appointment(
            "Standup",
            new DateTime(2026, 8, 12, 9, 0, 0),
            TimeSpan.FromMinutes(30),
            "Room 4"
        );
        Console.Write("Full: ");

        full.PrintDetails();


        Appointment twoArg = new Appointment(
           "Client Call",
           new DateTime(2026, 8, 12, 14, 0, 0)
       );

        Console.Write("Two-arg: ");
        twoArg.PrintDetails();

        // 1-argument constructor
        Appointment oneArg = new Appointment("Follow Up");

        Console.Write("One-arg: ");
        oneArg.PrintDetails();

        Console.WriteLine(
            $"DefaultDurationMinutes: {Appointment.DefaultDurationMinutes}"
        );




    }
}

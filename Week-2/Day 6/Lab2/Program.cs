public class LibraryBook
{
    private string _isbn;
    public string Title;
    protected string ShelfLocation = "Unassigned";
    internal int CopiesAvailable;
    public static int TotalBooksCreated;

    public LibraryBook(string title, string isbn)
    {
        Title = title;
        _isbn = isbn;

        // Each new book starts with 1 copy
        CopiesAvailable = 1;

        // Shared variable increases for every object
        TotalBooksCreated++;
    }

    protected internal void Relocate(string newLocation)
    {
        ShelfLocation = newLocation;
    }

    private protected void AdjustCopies(int delta)
    {
        CopiesAvailable += delta;
    }


}


public class ReferenceBook : LibraryBook
{
    public ReferenceBook(string title, string isbn)
        : base(title, isbn)
    {
    }

    public void PrintLocation()
    {
        // Calling protected internal method
        Relocate("Reference Section");


        // Accessing protected member
        Console.WriteLine(
            $"ReferenceBook shelf location after Relocate: \"{ShelfLocation}\""
        );


        // Calling private protected method
        AdjustCopies(2);

        Console.WriteLine(
            $"Copies available after AdjustCopies(+2): {CopiesAvailable}"
        );
    }
}


public class Program
{
    public static void Main()
    {
        LibraryBook book1 = new LibraryBook("C# Basics", "ISBN-001");

        Console.WriteLine(
            $"Book 1 created. Total books so far: {LibraryBook.TotalBooksCreated}"
        );

        LibraryBook book2 = new LibraryBook("OOP", "ISBN-002");

        Console.WriteLine(
            $"Book 2 created. Total books so far: {LibraryBook.TotalBooksCreated}"
        );

        LibraryBook book3 = new LibraryBook("C# Advanced", "ISBN-003");

        Console.WriteLine(
            $"Book 3 created. Total books so far: {LibraryBook.TotalBooksCreated}"
        );

        ReferenceBook referenceBook =
            new ReferenceBook("Reference C#", "ISBN-004");

        referenceBook.PrintLocation();
    }
}
using System;


class HistoryManagement
{
    int[] stack = new int[5];
    int top = -1;

   public void VisitPage()
    {
        if (top == stack.Length - 1)
        {
            Console.WriteLine("Stack Overflow");
            return;
        }
        Console.WriteLine("Enter the page number");
        int page = Convert.ToInt32(Console.ReadLine());
        stack[++top] = page;
    }

    public void BackToPrevious()
    {
        if (top == -1)
        {

            Console.WriteLine("Stack Underflow");
                return;
        }
        Console.WriteLine("Previous Page: " + stack[top--]);
    }

    public void DisplayHistory()
    {
        for(int i = top; i >= 0; i--)
        {
            Console.WriteLine(stack[i]);
        }
    }

    public void CurrentPage()
    {
        Console.WriteLine( stack[top]);
    }

    public void TotalPage()
    {
        int count = 0;
        for(int i = top; i >= 0; i--)
        {
            count++;
        }
        Console.WriteLine("Total Page: " + count);

    }
    public void ClearHistory()
    {
        for (int i = top; i >= 0; i--)
        {
            BackToPrevious();
        }
        Console.WriteLine("All history cleared");
    }
}


class Program
{
    static void Main()
    {
        HistoryManagement hs = new HistoryManagement();

        bool running = true;
        while (running)
        {
           
            Console.WriteLine("1.Visit Page");
            Console.WriteLine("2.Back");
            Console.WriteLine("3.Current Page");
            Console.WriteLine("4.Display History");
            Console.WriteLine("5.Clear History");
            Console.WriteLine("6.Total Page");
            Console.WriteLine("7.Exit");
            Console.WriteLine("Enter Your Choice");

            int input = Convert.ToInt32(Console.ReadLine());

            switch (input)
            {
                case 1:
                    hs.VisitPage();
                    break;

                case 2:
                    hs.BackToPrevious();

                    break;
                case 3:
                    Console.WriteLine("Current Page: ");
                    hs.CurrentPage();
                    break;
                case 4:
                    Console.WriteLine("History:");
                    hs.DisplayHistory();
                    break;
                case 5:
                    hs.ClearHistory();
                    break;
                case 6:
                    hs.TotalPage();
                    break;
                case 7:
                    running = false;
                    break;

                default:
                    Console.WriteLine("Enter valid choice");
                    break;
                    
            }

        }
    }
}
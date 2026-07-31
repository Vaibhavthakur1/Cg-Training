using System;
using System.ComponentModel.Design;

class Customer
{

    string[] tickets =
       {
            "T001|John|Login Issue",
            "T002|Alice|Payment Failed",
            "T003|David|Account Locked",
            "T004|Emma|Refund Request",
            "T005|James|Password Reset"
        };
    string[] queue = new string[5];
    int front = 0;
    int rear = -1;

    //to insert tickets into the queue
    public void enqueue()
    {
        foreach (string ticket in tickets)
        {
            if (rear == queue.Length - 1)
            {
                Console.WriteLine("Queue is full");
                break;
            }
            queue[++rear] = ticket;
        }

    }

    //to display the queue
    public void Display()
    {
        for (int i = front; i <= rear; i++)
        {
            Console.WriteLine(queue[i]);
        }
    }

    //search ticket by id
    public void SearchById()
    {
        Console.Write("Enter ticket id: ");
        string id = Console.ReadLine();

        bool found = false;

        for(int i = front; i <= rear; i++)
        {
            string[] data = queue[i].Split('|');
            if (data[0] == id)
            {
                Console.WriteLine("Ticket Found: " + data[0]);
                Console.WriteLine("Name " + data[1]);
                Console.WriteLine("Issue " + data[2]);
                found = true;
                break;
            }

        }
        if (!found)
        {
            Console.WriteLine("Ticket not found");
        }
    }
    
    //search BY Name
    public void SearchByName()
    {
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        bool found = false;

        for (int i = front; i <= rear; i++)
        {
            string[] data = queue[i].Split('|');
            if (data[1] == name)
            {
                Console.WriteLine("Ticket Found: " + data[0]);
                Console.WriteLine("Name " + data[1]);
                Console.WriteLine("Issue " + data[2]);
                found = true;
                break;
            }

        }
        if (!found)
        {
            Console.WriteLine("Ticket not found");
        }
    }

    // remove element from the queue
    public void Dequeue()
    {
        if (front <= rear)
        {
            Console.WriteLine("Dequeued tickets:");
            Console.WriteLine(queue[front]);
            front++;
        }
        else
        {
            Console.WriteLine("Queue is empty");
        }

    }

    //Count the no of issues in the queue

    public void CountIssue()
    {
        int login = 0;
        int payment = 0;
        int account = 0;
        int refund = 0; 
        int password = 0;

        for(int i = front; i <= rear; i++)
        {
            string[] data = queue[i].Split('|');
            if (data[2] == "Login Issue")
                login++;
            else if (data[2] == "Payment Failed")
                payment++;
            else if (data[2] == "Account Locked")
                account++;
            else if (data[2] == "Refund Request")
                refund++;
            else if (data[2] == "Password Reset")
                password++;


        }
        Console.WriteLine("Login Issue: " + login);
        Console.WriteLine("Payment issue: " + payment);
        Console.WriteLine("Account Locked: " + account);
        Console.WriteLine("Refund Issue: " + refund);
        Console.WriteLine("Password Reset: " + password);
    }
}
class Program
{
    static void Main()
    {
        Customer cs = new Customer();

        //execute the methods
        cs.enqueue();
        cs.Display();
        cs.CountIssue();
        cs.SearchByName();
        cs.SearchById();

        //cs.Dequeue();
        //cs.Display();






    }
}


     



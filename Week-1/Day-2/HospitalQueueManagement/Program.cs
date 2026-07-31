using System;

class HospitalQueue
{
    int[] queue = new int[5];
    int front = 0;
    int rear = -1;

    public void RegisterPatient()
    {
        if (rear == queue.Length - 1)
        {
            Console.WriteLine("Queue is full");
            return;
        }
        int value = Convert.ToInt32(Console.ReadLine());
        queue[++rear] = value;
        Console.WriteLine("Registered Successfully");

    }

    public void NextPatient()
    {
        if (front > rear)
        {
            Console.WriteLine("No Patient Remaining");
            return;

        }
        Console.WriteLine("Next patient is: " + queue[front++]);
    }

    public void ViewNext()
    {
        if(front > rear)
        {
            Console.WriteLine("No remaining Patient");
        }
        Console.WriteLine("Next patient is :" + queue[front]);
    }

    public void DisplayWaiting()
    {
        for(int i = front; i <= rear; i++)
        {
            Console.WriteLine(queue[i]);
        }
    }

   public void SearchPatient()
    {
        int Id = Convert.ToInt32(Console.ReadLine());
        for(int i = front; i < rear; i++)
        {
            if (queue[i] == Id)
            {
                Console.WriteLine("Patient is Present: " + queue[i]);
                return;
            }
            Console.WriteLine("Patient not found");
        }
    }
    public void WaitingPatient()
    {
        int count = 0;
        for (int i = front; i <= rear; i++)
        {
            count++;
        }
        Console.WriteLine("Total waiting patient: " + count);
    }

}

class Program
{
    static void Main()
    {
        HospitalQueue q = new HospitalQueue();
        bool running = true;
        while (running)
        {

            Console.WriteLine("1.Register Patient");
            Console.WriteLine("2.Call Next Patient");
            Console.WriteLine("3.View Next Patient");
            Console.WriteLine("4.Display Waiting Patient");
            Console.WriteLine("5.Search Patient");
            Console.WriteLine("6.Count Waiting Patient");
            Console.WriteLine("7.Exit");
            Console.WriteLine("Enter Your Choice");

            int input = Convert.ToInt32(Console.ReadLine());

            switch (input)
            {
                case 1:
                    Console.WriteLine("Enter the Paitent Id");
                    q.RegisterPatient();
                    break;
                case 2:
                    q.NextPatient();
                    break;
                case 3:
                    q.ViewNext();
                    break;
                case 4:
                    Console.WriteLine("Waiting Patients:");
                    q.DisplayWaiting();
                    break;
                case 5:
                    Console.WriteLine("Enter the Patient Id");
                    q.SearchPatient();
                    break;
                case 6:
                    q.WaitingPatient();
                    break;
                case 7:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Enter Valid choice: ");
                    break;
            }

        }
    }
}


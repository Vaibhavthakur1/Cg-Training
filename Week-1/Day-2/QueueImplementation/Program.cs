using System;

class QueueArray
{
    int[] queue = new int[5];
    int front = 0;
    int rear = -1;

    public void Enqueue(int value)
    {
        if (rear == queue.Length - 1)
        {
            Console.WriteLine("Queue Full");
            return;
        }

        queue[++rear] = value;
    }

    public void Dequeue()
    {
        if (front > rear)
        {
            Console.WriteLine("Queue Empty");
            return;
        }

        Console.WriteLine("Deleted: " + queue[front++]);
    }

    public void Display()
    {
        for (int i = front; i <= rear; i++)
        {
            Console.WriteLine(queue[i]);
        }
    }
}

class Program
{
    static void Main()
    {
        QueueArray q = new QueueArray();

        q.Enqueue(10);
        q.Enqueue(20);
        q.Enqueue(30);

        q.Display();

        q.Dequeue();

        Console.WriteLine("\nAfter Dequeue");

        q.Display();
    }
}
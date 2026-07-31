using System;

class StackArray
{
    int[] stack = new int[5];
    int top = -1;

    public void Push(int value)
    {
        if (top == stack.Length - 1)
        {
            Console.WriteLine("Stack Overflow");
            return;
        }

        stack[++top] = value;
    }

    public void Pop()
    {
        if (top == -1)
        {
            Console.WriteLine("Stack Underflow");
            return;
        }

        Console.WriteLine("Deleted: " + stack[top--]);
    }

    public void Display()
    {
        for (int i = top; i >= 0; i--)
            Console.WriteLine(stack[i]);
    }
}

class Program
{
    static void Main()
    {
        StackArray s = new StackArray();

        s.Push(10);
        s.Push(20);
        s.Push(30);

        s.Display();

        s.Pop();

        s.Display();
    }
}
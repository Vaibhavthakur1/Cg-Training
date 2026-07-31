//using System;

//class Node
//{
//    public int Data;
//    public Node Next;


//    public Node(int data)
//    {
//        Data = data;
//        Next = null;
//    }
//}

//class SinglyLinkedList
//{
//    Node head;

//    public void Insert(int data)
//    {
//        Node newNode = new Node(data);

//        if (head == null)
//        {
//            head = newNode;
//            return;
//        }
//        Node temp = head;

//        while (temp.Next != null)
//        {
//            temp = temp.Next;
//        }

//        temp.Next = newNode;


//    }

//    public void Display()
//    {
//        Node temp = head;
//        while (temp != null)
//        {
//            Console.Write(temp.Data + " ->");
//            temp = temp.Next;
//        }
//        Console.WriteLine("Null");
//    }
//    static void Main()
//    {
//        SinglyLinkedList list = new SinglyLinkedList();
//        list.Insert(10);
//        list.Insert(20);
//        list.Insert(30);
//        Console.WriteLine("Singly Linked List: ");
//        list.Display();
//    }

//}




//DublyLinkedList

//using System;

//class Node
//{
//    public int Data;
//    public Node Prev;
//    public Node Next;

//    public Node(int data)
//    {
//        Data = data;
//        Prev = null;
//        Next = null;
//    }
//}

//class DoublyLinkedList
//{
//    Node head;

//    public void Insert(int data)
//    {
//        Node newNode = new Node(data);

//        if (head == null)
//        {
//            head = newNode;
//            return;
//        }

//        Node temp = head;

//        while (temp.Next != null)
//        {
//            temp = temp.Next;
//        }

//        temp.Next = newNode;
//        newNode.Prev = temp;
//    }

//    public void DisplayForward()
//    {
//        Node temp = head;

//        while (temp != null)
//        {
//            Console.Write(temp.Data + " <-> ");
//            temp = temp.Next;
//        }

//        Console.WriteLine("NULL");
//    }

//    static void Main()
//    {
//        DoublyLinkedList list = new DoublyLinkedList();

//        list.Insert(10);
//        list.Insert(20);
//        list.Insert(30);
//        list.Insert(40);

//        Console.WriteLine("Doubly Linked List:");
//        list.DisplayForward();
//    }
//}
//Output
//Doubly Linked List:
//10 <-> 20 <-> 30 <-> 40 <->NULL


using System;

class Node
{
    public int Data;
    public Node Next;

    public Node(int data)
    {
        Data = data;
        Next = null;
    }
}

class CircularLinkedList
{
    Node head;

    public void Insert(int data)
    {
        Node newNode = new Node(data);

        if (head == null)
        {
            head = newNode;
            newNode.Next = head;
            return;
        }

        Node temp=head;

        while (temp.Next != head)
        {
            temp = temp.Next;
        }

        temp.Next = newNode;
        newNode.Next = head;
    }

    public void Display()
    {
        if (head == null)
        {
            return;
        }

        Node temp = head;

        do
        {
            Console.Write(temp.Data + " -> ");
            temp = temp.Next;

        } while (temp != head);

        Console.WriteLine("BAck to head");
    }

    static void Main()
    {
        CircularLinkedList list = new CircularLinkedList();
        list.Insert(10);
        list.Insert(20);
        list.Insert(30);
        list.Insert(40);
        list.Display();

    }
}
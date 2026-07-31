using System;

public class BSTNode
{
    public int Value { get; set; }
    public BSTNode Left { get; set; }
    public BSTNode Right { get; set; }

    public BSTNode(int value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

public class BST
{
    public BSTNode Root { get; set; }

    public BST()
    {
        Root = null;
    }

    // Insert
    public void Insert(int value)
    {
        Root = InsertRec(Root, value);
    }

    private BSTNode InsertRec(BSTNode node, int value)
    {
        if (node == null)
            return new BSTNode(value);

        if (value < node.Value)
            node.Left = InsertRec(node.Left, value);
        else if (value > node.Value)
            node.Right = InsertRec(node.Right, value);

        return node;
    }

    // Search
    //public BSTNode Search(int value)
    //{
    //    return SearchRec(Root, value);
    //}

    //private BSTNode SearchRec(BSTNode node, int value)
    //{
    //    if (node == null || node.Value == value)
    //        return node;

    //    if (value < node.Value)
    //        return SearchRec(node.Left, value);
    //    return SearchRec(node.Right, value);
    //}

    // Delete
    //public void Delete(int value)
    //{
    //    Root = DeleteRec(Root, value);
    //}

    //private BSTNode DeleteRec(BSTNode node, int v2alue)
    //{
    //    if (node == null)
    //        return null;

    //    if (value < node.Value)
    //        node.Left = DeleteRec(node.Left, value);
    //    else if (value > node.Value)
    //        node.Right = DeleteRec(node.Right, value);
    //    else
    //    {
    //        // Node found
    //        // Case 1: Leaf node
    //        if (node.Left == null && node.Right == null)
    //            return null;

    //        // Case 2: One child
    //        if (node.Left == null)
    //            return node.Right;
    //        if (node.Right == null)
    //            return node.Left;

    //        // Case 3: Two children
    //        // Find inorder successor (smallest in right subtree)
    //        BSTNode successor = MinValueNode(node.Right);
    //        node.Value = successor.Value;
    //        // Delete the successor
    //        node.Right = DeleteRec(node.Right, successor.Value);
    //    }

    //    return node;
    //}

    private BSTNode MinValueNode(BSTNode node)
    {
        BSTNode current = node;
        while (current.Left != null)
            current = current.Left;
        return current;
    }

    // Inorder traversal (gives sorted order)
    public void Inorder()
    {
        InorderRec(Root);
        Console.WriteLine();
    }

    private void InorderRec(BSTNode node)
    {
        if (node != null)
        {
            InorderRec(node.Left);
            Console.Write(node.Value + " ");
            InorderRec(node.Right);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BST tree = new BST();

        // Insert nodes
        tree.Insert(50);
        tree.Insert(30);
        tree.Insert(70);
        tree.Insert(20);
        tree.Insert(40);
        tree.Insert(60);
        tree.Insert(80);

        // Display tree
        Console.Write("Inorder Traversal: ");
        tree.Inorder();

        // Search
        //Console.Write("Enter value to search: ");
        //int searchValue = Convert.ToInt32(Console.ReadLine());

        //BSTNode result = tree.Search(searchValue);

        //if (result != null)
        //    Console.WriteLine(searchValue + " found in BST.");
        //else
        //    Console.WriteLine(searchValue + " not found.");

        // Delete
        //Console.Write("Enter value to delete: ");
        //int deleteValue = Convert.ToInt32(Console.ReadLine());

        //tree.Delete(deleteValue);

        //Console.Write("Inorder after deletion: ");
        //tree.Inorder();
    }
}
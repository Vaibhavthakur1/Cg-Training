using System;
using System.Collections.Generic;

class Node
{
    public int data;
    public Node left;
    public Node right;

    // Initializes a tree node with the given value.
    public Node(int data)
    {
        this.data = data;
        left = null;
        right = null;
    }
}

class Solution
{
    // Inserts a value into the Binary Search Tree.
    public static Node insert(Node root, int data)
    {
        if (root == null)
            return new Node(data);

        if (data <= root.data)
            root.left = insert(root.left, data);
        else
            root.right = insert(root.right, data);

        return root;
    }

    // Prints the tree in preorder traversal.
    public static void preOrder(Node root)
    {
        if (root == null)
            return;

        Console.Write(root.data + " ");
        preOrder(root.left);
        preOrder(root.right);
    }

    // Reads input, builds the tree, and prints its preorder traversal.
    static void Main(string[] args)
    {
        int t = int.Parse(Console.ReadLine());

        string[] values = Console.ReadLine().Split(' ');

        Node root = null;

        for (int i = 0; i < t; i++)
        {
            int data = int.Parse(values[i]);
            root = insert(root, data);
        }

        preOrder(root);
    }
}

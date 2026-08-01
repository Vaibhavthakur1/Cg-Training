using System;

class Node
{
    public int data;
    public Node left;
    public Node right;

    public Node(int value)
    {
        data = value;
        left = null;
        right = null;
    }
}

class BinarySearchTree
{
    public Node root;

    public BinarySearchTree()
    {
        root = null;
    }

    public void Insert(int data)
    {
        root = Insert(root, data);
    }

    private Node Insert(Node root, int data)
    {
        if (root == null)
            return new Node(data);

        if (data <= root.data)
            root.left = Insert(root.left, data);
        else
            root.right = Insert(root.right, data);

        return root;
    }

    static int getHeight(Node root)
    {   
        // Base case: an empty tree has height -1
        if (root == null)
            return -1;

         // Recursively calculate the height of left and right subtrees
        int left = getHeight(root.left);
        int right = getHeight(root.right);
    
        //Return the Max height +1 for current node
        return Math.Max(left, right) + 1;
    }

    static void Main(string[] args)
    {
        BinarySearchTree tree = new BinarySearchTree();

        int n = int.Parse(Console.ReadLine());
        string[] values = Console.ReadLine().Split(' ');

        for (int i = 0; i < n; i++)
        {
            tree.Insert(int.Parse(values[i]));
        }
        Console.WriteLine(getHeight(tree.root));
    }
}
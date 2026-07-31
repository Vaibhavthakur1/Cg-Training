using System;

using System.Collections.Generic;

public class BTreeNode
{
    public List<int> Keys { get; set; }

    public List<BTreeNode> Children { get; set; }

    public bool IsLeaf { get; set; }

    public BTreeNode(bool isleaf)
    {
        Keys = new List<int>();
        Children = new List<BTreeNode>();
        IsLeaf = isleaf;
    }
}

public class Btree
{
    private BTreeNode root;
    private int degree;
    private int maxKeys => 2 * degree - 1;
    private int minKeys => degree - 1;

    public Btree(int degree)
    {
        this.degree = degree;
        root = new BTreeNode(true);
    }

   //insert Key
    public void Insert(int key)
    {
        if (root.Keys.Count == maxKeys)
        {
            BTreeNode newRoot = new BTreeNode(false);
            newRoot.Children.Add(root);
            SplitChild(newRoot, 0);
            root = newRoot;
            InsertNonFull(root, key);
        }
        else
        {
            InsertNonFull(root, key);
        }
    }

    private void InsertNonFull(BTreeNode node ,int key)
    {
        int i = node.Keys.Count - 1;
        if (node.IsLeaf)
        {
            //instert key in sorted order
            node.Keys.Add(0);
            while(i>=0 && key < node.Keys[i])
            {
                node.Keys[i + 1] = node.Keys[i];
                i--;
            }
            node.Keys[i + 1] = key;


        }
        else
        {
            while (i >= 0 && key < node.Keys[i])
                i--;
            i++;

            if (node.Children[i].Keys.Count == maxKeys)
            {
                SplitChild(node, i);
                if (key > node.Keys[i])
                {
                    i++;
                }
            }
            InsertNonFull(node.Children[i],key);
        }
    }

    private void SplitChild(BTreeNode parent, int index)
    {
        BTreeNode child = parent.Children[index];
        BTreeNode newChild = new BTreeNode(child.IsLeaf);

        int middleKey = child.Keys[degree - 1];

        //copy the last minKeys keys to newChild
        for (int j = 0; j < minKeys; j++)
        {
            newChild.Keys.Add(child.Keys[j + degree]);
        }

        //copy the last degree children if not leaf
        if (!child.IsLeaf)
        {
            for (int j = degree; j < child.Children.Count; j++)
            {
                newChild.Children.Add(child.Children[j]);
            }

            child.Children.RemoveRange(degree, child.Children.Count - degree);
        }

        //Remove the copies from child
        child.Keys.RemoveRange(degree - 1, child.Keys.Count - (degree - 1));


        // Insert into parent
        parent.Children.Insert(index + 1, newChild);
        parent.Keys.Insert(index, middleKey);

    }
   


    
    public void Display()
    {
        DisplayRecord(root, 0);
    }
    private void DisplayRecord(BTreeNode node,int level)
    {
        Console.WriteLine($"Level {level}:" + string.Join(", ", node.Keys));
        if (!node.IsLeaf)
        {
            foreach(var child in node.Children)
            {
                DisplayRecord(child, level + 1);
            }
        }
    }


}
class Program
{
    static void Main(string[] args)
    {
        Btree bTree = new Btree(3);//degree 3
        int[] keys = { 10, 20, 4, 5, 12, 30, 7, 17 };
        foreach(int key in keys)
        {
            bTree.Insert(key);
        }
        Console.WriteLine("B-Tree Structure:");
        bTree.Display();
    }
}

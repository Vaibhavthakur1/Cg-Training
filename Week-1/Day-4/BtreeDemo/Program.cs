using System;

using System.Collections.Generic;

public class BTreeNode
{
    public List<int> Keys { get; set; }

    public List<BTreeNode> Children { get; set; }

    public bool Isleaf { get; set; }

    public BTreeNode(bool isLeaf)
    {
        Keys = new List<int>();
        Children = new List<BTreeNode>();
        Isleaf = isLeaf;
    }
}

public class Btree
{
    private BTreeNode root;
    private int degree;
    private int maxKeys => 2 * degree - 1;
    private int minKeys => degree - 1;

    public Btree(int degree){
        this.degree = degree;
        root = new BTreeNode(true);
    }

    public void Insert(int key)
    {
        if (root.Keys.Count == maxKeys)
        {
            BTreeNode newRoot = new BTreeNode(false);
            newRoot.Children.Add(root);
        }
    }

    private void SplitChild(BTreeNode parent,int index)
    {
        BTreeNode child = parent.Children[index];
        BTreeNode newChild = new BTreeNode(child.Isleaf);
    }
}
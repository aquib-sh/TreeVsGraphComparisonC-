using System;
using System.Collections.Generic;

class TreeNode
{
    public string Value { get; }
    public List<TreeNode> Children { get; }

    public TreeNode(string value)
    {
        Value = value;
        Children = new List<TreeNode>();
    }
}

class HierarchyTree
{
    private TreeNode root;

    public HierarchyTree(Dictionary<string, string> hierarchyDict)
    {
        BuildTree(hierarchyDict);
    }

    private void BuildTree(Dictionary<string, string> hierarchyDict)
    {
        Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();

        foreach (var key in hierarchyDict.Keys)
        {
            nodes[key] = new TreeNode(key);
        }

        foreach (var entry in hierarchyDict)
        {
            string child = entry.Key;
            string parent = entry.Value;

            TreeNode childNode = nodes[child];

            if (nodes.TryGetValue(parent, out TreeNode parentNode))
            {
                parentNode.Children.Add(childNode);
            }
            else
            {
                // If the parent is not present in the hierarchyDict, it becomes the root
                root = childNode;
            }
        }
    }

    public bool IsChildOf(string child, string parent)
    {
        if (root == null)
        {
            return false;
        }

        return IsChildOfHelper(root, child, parent);
    }

    private bool IsChildOfHelper(TreeNode currentNode, string child, string parent)
    {
        if (currentNode.Value == parent)
        {
            foreach (var childNode in currentNode.Children)
            {
                if (childNode.Value == child)
                {
                    return true;
                }
            }
            return false;
        }

        foreach (var childNode in currentNode.Children)
        {
            if (IsChildOfHelper(childNode, child, parent))
            {
                return true;
            }
        }

        return false;
    }
}



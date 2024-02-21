using System;
using System.Collections.Generic;
using System.Diagnostics;

class HierarchyGraph
{
    private Dictionary<string, List<string>> adjacencyList;

    public HierarchyGraph(Dictionary<string, string> hierarchyDict)
    {
        adjacencyList = new Dictionary<string, List<string>>();
        foreach (var entry in hierarchyDict)
        {
            string child = entry.Key;
            string parent = entry.Value;

            if (!adjacencyList.ContainsKey(parent))
            {
                adjacencyList[parent] = new List<string>();
            }

            adjacencyList[parent].Add(child);
        }
    }

    public bool IsChildOf(string child, string parent)
    {
        if (!adjacencyList.ContainsKey(parent))
        {
            return false;
        }

        if (child == parent)
        {
            return true;
        }

        Queue<string> queue = new Queue<string>(adjacencyList[parent]);
        HashSet<string> visited = new HashSet<string>();

        while (queue.Count > 0)
        {
            string current = queue.Dequeue();
            if (current == child)
            {
                return true;
            }

            visited.Add(current);

            if (adjacencyList.ContainsKey(current))
            {
                foreach (var neighbor in adjacencyList[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        return false;
    }
}

class Program
{
    static void Main()
    {
       Dictionary<string, string> hierarchyDict = new Dictionary<string, string>
{
    {"a", "SC"},
    {"b", "m"},
    {"m", "a"},
    {"n", "m"},
    {"l", "n"},
    {"c", "SD"},
    {"d", "e"},
    {"e", "f"},
    {"g", "f"},
    {"h", "b"},
    {"i", "h"},
    {"j", "i"},
    {"k", "j"},
    {"x", "k"},
    {"y", "x"},
    {"z", "y"},
    {"p", "d"},    // Additional relationship
    {"q", "p"},    // Additional relationship
    {"r", "q"},    // Additional relationship
    {"s", "r"},    // Additional relationship
    {"t", "s"},    // Additional relationship
    {"u", "t"},    // Additional relationship
    {"v", "u"},    // Additional relationship
    {"w", "v"},    // Additional relationship
    {"1", "w"},    // Additional relationship
    {"2", "1"},    // Additional relationship
    {"3", "2"},    // Additional relationship
    {"4", "3"},    // Additional relationship
    {"5", "4"},    // Additional relationship
    {"6", "5"},    // Additional relationship
    {"7", "6"},    // Additional relationship
    {"8", "7"},    // Additional relationship
    {"9", "8"},    // Additional relationship
    {"10", "9"}    // Additional relationship
};
 

        HierarchyGraph graph = new HierarchyGraph(hierarchyDict);
        HierarchyTree tree = new HierarchyTree(hierarchyDict);

        // Test cases


var testCases = new[]
{
    ("l", "SC", true),
    ("a", "SC", true),
    ("n", "SC", true),
    ("g", "e", false),
    ("c", "e", false),
    ("d", "f", true),
    ("x", "y", false),
    ("z", "SC", true),      // Tricky: Deeply nested
    ("z", "b", true),       // Tricky: Multiple levels
    ("10", "w", true),      // Tricky: Multiple levels
    ("10", "9", true),      // Tricky: Multiple levels
    ("9", "10", false),     // Tricky: Multiple levels, reverse order
    ("a", "z", false),      // Tricky: Not a child, reverse order
    ("p", "8", true),       // Tricky: Intermediate node
    ("s", "d", true),       // Tricky: Intermediate node
    ("1", "8", true),       // Tricky: Intermediate node
    ("5", "2", true),       // Tricky: Intermediate node
    ("3", "8", false),      // Tricky: Intermediate node, not a child
    ("7", "3", false),      // Tricky: Intermediate node, not a child
    ("6", "10", false),     // Tricky: Intermediate node, not a child
    ("y", "x", true),       // Tricky: Multiple relationships
    ("y", "z", false)       // Tricky: Not a child, multiple relationships
};



Stopwatch treeWatch = new Stopwatch();
Stopwatch graphWatch = new Stopwatch();

for(int i = 0; i < 1000; i++)
    {
foreach (var testCase in testCases)
{
    string child = testCase.Item1;
    string parent = testCase.Item2;
    bool expectedResult = testCase.Item3;

    graphWatch.Start();
    bool graphResult = graph.IsChildOf(child, parent);
    graphWatch.Stop();

    treeWatch.Start();
    bool treeResult = tree.IsChildOf(child, parent);
    treeWatch.Stop();

    // Console.WriteLine($"Is {child} a child of {parent}? Expected: {expectedResult}, Graph Result: {graphResult}, Tree Result: {treeResult}");
}
    }


Console.WriteLine("$$$ PERFORMANCE in Milliseconds $$$");
Console.WriteLine($"GRAPH: {graphWatch.Elapsed.TotalMilliseconds}");
Console.WriteLine($"TREE: {treeWatch.Elapsed.TotalMilliseconds}");
 
    }
}


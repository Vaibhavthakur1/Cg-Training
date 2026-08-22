using System;
using System.Collections.Generic;

namespace GraphTraversalLab
{
    public class Program
    {
        public static void Main()
        {
            // Adjacency list representation of the directed graph
            var graph = new Dictionary<string, List<string>>
            {
                ["A"] = new() { "B", "C" },
                ["B"] = new() { "D" },
                ["C"] = new() { "D" },
                ["D"] = new() { "E" },
                ["E"] = new() // Sink node
            };

            // 1. BFS Traversal
            var bfsOrder = BreadthFirstSearch(graph, "A");
            Console.WriteLine($"BFS Order: {string.Join(" -> ", bfsOrder)}");
            // Output: A -> B -> C -> D -> E

            // 2. DFS Traversal
            var dfsOrder = DepthFirstSearch(graph, "A");
            Console.WriteLine($"DFS Order: {string.Join(" -> ", dfsOrder)}");
            // Output: A -> C -> D -> E -> B (or A -> B -> D -> E -> C depending on neighbor push order)
        }

        // 1. Breadth-First Search (Queue + HashSet)
        public static List<string> BreadthFirstSearch(Dictionary<string, List<string>> graph, string startNode)
        {
            var result = new List<string>();
            var visited = new HashSet<string>();
            var queue = new Queue<string>();

            visited.Add(startNode);
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                result.Add(current);

                if (graph.TryGetValue(current, out var neighbors))
                {
                    foreach (var neighbor in neighbors)
                    {
                        if (visited.Add(neighbor)) // Returns true if the element was added (not already present)
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }

            return result;
        }

        // 2. Depth-First Search (Stack + HashSet)
        public static List<string> DepthFirstSearch(Dictionary<string, List<string>> graph, string startNode)
        {
            var result = new List<string>();
            var visited = new HashSet<string>();
            var stack = new Stack<string>();

            stack.Push(startNode);

            while (stack.Count > 0)
            {
                string current = stack.Pop();

                if (!visited.Add(current))
                {
                    continue;
                }

                result.Add(current);

                if (graph.TryGetValue(current, out var neighbors))
                {
                    foreach (var neighbor in neighbors)
                    {
                        if (!visited.Contains(neighbor))
                        {
                            stack.Push(neighbor);
                        }
                    }
                }
            }

            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

// Edge class for weighted graphs
public class Edge
{
    public int Destination { get; set; }
    public int Weight { get; set; }

    public Edge(int destination, int weight = 1)
    {
        Destination = destination;
        Weight = weight;
    }
}

// Vertex class for storing additional data
public class Vertex
{
    public int Id { get; set; }
    public string Label { get; set; }

    public Vertex(int id, string label = null)
    {
        Id = id;
        Label = label ?? id.ToString();
    }

    public override string ToString() => Label;
}

//2.Undirected Unweighted Graph
//Characteristics: Edges have no direction (bidirectional), and no weights.

//csharp
public class UndirectedUnweightedGraph
{
    private Dictionary<int, List<int>> adjacencyList;

    public UndirectedUnweightedGraph()
    {
        adjacencyList = new Dictionary<int, List<int>>();
    }

    // Add vertex
    public void AddVertex(int vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
            adjacencyList[vertex] = new List<int>();
    }

    // Add edge (bidirectional)
    public void AddEdge(int source, int destination)
    {
        if (!adjacencyList.ContainsKey(source))
            AddVertex(source);
        if (!adjacencyList.ContainsKey(destination))
            AddVertex(destination);

        adjacencyList[source].Add(destination);
        adjacencyList[destination].Add(source); // Undirected
    }

    // Remove edge
    public void RemoveEdge(int source, int destination)
    {
        if (adjacencyList.ContainsKey(source))
            adjacencyList[source].Remove(destination);
        if (adjacencyList.ContainsKey(destination))
            adjacencyList[destination].Remove(source);
    }

    // Remove vertex
    public void RemoveVertex(int vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
            return;

        // Remove all edges pointing to this vertex
        foreach (var v in adjacencyList.Keys.ToList())
        {
            adjacencyList[v].Remove(vertex);
        }

        adjacencyList.Remove(vertex);
    }

    // Check if edge exists
    public bool HasEdge(int source, int destination)
    {
        return adjacencyList.ContainsKey(source) &&
               adjacencyList[source].Contains(destination);
    }

    // Get neighbors
    public List<int> GetNeighbors(int vertex)
    {
        return adjacencyList.ContainsKey(vertex) ?
               adjacencyList[vertex] : new List<int>();
    }

    // BFS Traversal
    public void BFS(int start)
    {
        if (!adjacencyList.ContainsKey(start))
            return;

        var visited = new HashSet<int>();
        var queue = new Queue<int>();

        visited.Add(start);
        queue.Enqueue(start);

        Console.Write("BFS: ");
        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            Console.Write(current + " ");

            foreach (var neighbor in adjacencyList[current])
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }
        Console.WriteLine();
    }

    // DFS Traversal
    public void DFS(int start)
    {
        if (!adjacencyList.ContainsKey(start))
            return;

        var visited = new HashSet<int>();
        Console.Write("DFS: ");
        DFSRecursive(start, visited);
        Console.WriteLine();
    }

    private void DFSRecursive(int vertex, HashSet<int> visited)
    {
        visited.Add(vertex);
        Console.Write(vertex + " ");

        foreach (var neighbor in adjacencyList[vertex])
        {
            if (!visited.Contains(neighbor))
                DFSRecursive(neighbor, visited);
        }
    }

    // Display graph
    public void Display()
    {
        foreach (var kvp in adjacencyList)
        {
            Console.WriteLine($"{kvp.Key} -> [{string.Join(", ", kvp.Value)}]");
        }
    }
}


//3.Directed Unweighted Graph
//Characteristics: Edges have direction(one - way), no weights.

//csharp
public class DirectedUnweightedGraph
{
    private Dictionary<int, List<int>> adjacencyList;

    public DirectedUnweightedGraph()
    {
        adjacencyList = new Dictionary<int, List<int>>();
    }

    public void AddVertex(int vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
            adjacencyList[vertex] = new List<int>();
    }

    // Add directed edge (source → destination only)
    public void AddEdge(int source, int destination)
    {
        if (!adjacencyList.ContainsKey(source))
            AddVertex(source);
        if (!adjacencyList.ContainsKey(destination))
            AddVertex(destination);

        adjacencyList[source].Add(destination);
    }

    public void RemoveEdge(int source, int destination)
    {
        if (adjacencyList.ContainsKey(source))
            adjacencyList[source].Remove(destination);
    }

    public void RemoveVertex(int vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
            return;

        // Remove all incoming edges
        foreach (var v in adjacencyList.Keys.ToList())
        {
            adjacencyList[v].Remove(vertex);
        }

        adjacencyList.Remove(vertex);
    }

    public bool HasEdge(int source, int destination)
    {
        return adjacencyList.ContainsKey(source) &&
               adjacencyList[source].Contains(destination);
    }

    public List<int> GetNeighbors(int vertex)
    {
        return adjacencyList.ContainsKey(vertex) ?
               adjacencyList[vertex] : new List<int>();
    }

    // Check if graph has a cycle (using DFS)
    public bool HasCycle()
    {
        var visited = new HashSet<int>();
        var recursionStack = new HashSet<int>();

        foreach (var vertex in adjacencyList.Keys)
        {
            if (HasCycleDFS(vertex, visited, recursionStack))
                return true;
        }
        return false;
    }

    private bool HasCycleDFS(int vertex, HashSet<int> visited, HashSet<int> recursionStack)
    {
        if (recursionStack.Contains(vertex))
            return true;

        if (visited.Contains(vertex))
            return false;

        visited.Add(vertex);
        recursionStack.Add(vertex);

        foreach (var neighbor in adjacencyList[vertex])
        {
            if (HasCycleDFS(neighbor, visited, recursionStack))
                return true;
        }

        recursionStack.Remove(vertex);
        return false;
    }

    // Topological Sort (only works for DAGs)
    public List<int> TopologicalSort()
    {
        if (HasCycle())
        {
            Console.WriteLine("Cannot perform topological sort: Graph has a cycle");
            return new List<int>();
        }

        var visited = new HashSet<int>();
        var stack = new Stack<int>();

        foreach (var vertex in adjacencyList.Keys)
        {
            if (!visited.Contains(vertex))
                TopologicalSortDFS(vertex, visited, stack);
        }

        return stack.ToList();
    }

    private void TopologicalSortDFS(int vertex, HashSet<int> visited, Stack<int> stack)
    {
        visited.Add(vertex);

        foreach (var neighbor in adjacencyList[vertex])
        {
            if (!visited.Contains(neighbor))
                TopologicalSortDFS(neighbor, visited, stack);
        }

        stack.Push(vertex);
    }

    public void Display()
    {
        foreach (var kvp in adjacencyList)
        {
            Console.WriteLine($"{kvp.Key} -> [{string.Join(", ", kvp.Value)}]");
        }
    }
}


//4.Undirected Weighted Graph
//Characteristics: Edges are bidirectional with weights/costs.

//csharp
public class UndirectedWeightedGraph
{
    private Dictionary<int, List<Edge>> adjacencyList;

    public UndirectedWeightedGraph()
    {
        adjacencyList = new Dictionary<int, List<Edge>>();
    }

    public void AddVertex(int vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
            adjacencyList[vertex] = new List<Edge>();
    }

    // Add weighted edge (bidirectional)
    public void AddEdge(int source, int destination, int weight)
    {
        if (!adjacencyList.ContainsKey(source))
            AddVertex(source);
        if (!adjacencyList.ContainsKey(destination))
            AddVertex(destination);

        adjacencyList[source].Add(new Edge(destination, weight));
        adjacencyList[destination].Add(new Edge(source, weight));
    }

    public List<Edge> GetNeighbors(int vertex)
    {
        return adjacencyList.ContainsKey(vertex) ?
               adjacencyList[vertex] : new List<Edge>();
    }

    // Dijkstra's Algorithm for shortest path
    public Dictionary<int, int> Dijkstra(int start)
    {
        if (!adjacencyList.ContainsKey(start))
            return new Dictionary<int, int>();

        var distances = new Dictionary<int, int>();
        var priorityQueue = new SortedSet<(int distance, int vertex)>();

        // Initialize distances
        foreach (var vertex in adjacencyList.Keys)
        {
            distances[vertex] = int.MaxValue;
        }
        distances[start] = 0;
        priorityQueue.Add((0, start));

        while (priorityQueue.Count > 0)
        {
            var (currentDist, current) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            if (currentDist > distances[current])
                continue;

            foreach (var edge in adjacencyList[current])
            {
                int newDist = currentDist + edge.Weight;
                if (newDist < distances[edge.Destination])
                {
                    distances[edge.Destination] = newDist;
                    priorityQueue.Add((newDist, edge.Destination));
                }
            }
        }

        return distances;
    }

    // Display graph with weights
    public void Display()
    {
        foreach (var kvp in adjacencyList)
        {
            var edges = kvp.Value.Select(e => $"{e.Destination}({e.Weight})");
            Console.WriteLine($"{kvp.Key} -> [{string.Join(", ", edges)}]");
        }
    }
}
//5.Directed Weighted Graph
//Characteristics: Edges have direction AND weights.

//csharp
public class DirectedWeightedGraph
{
    private Dictionary<int, List<Edge>> adjacencyList;

    public DirectedWeightedGraph()
    {
        adjacencyList = new Dictionary<int, List<Edge>>();
    }

    public void AddVertex(int vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
            adjacencyList[vertex] = new List<Edge>();
    }

    // Add directed weighted edge (source → destination)
    public void AddEdge(int source, int destination, int weight)
    {
        if (!adjacencyList.ContainsKey(source))
            AddVertex(source);
        if (!adjacencyList.ContainsKey(destination))
            AddVertex(destination);

        adjacencyList[source].Add(new Edge(destination, weight));
    }

    public List<Edge> GetNeighbors(int vertex)
    {
        return adjacencyList.ContainsKey(vertex) ?
               adjacencyList[vertex] : new List<Edge>();
    }

    // Bellman-Ford for shortest path (handles negative weights)
    public Dictionary<int, int> BellmanFord(int start)
    {
        if (!adjacencyList.ContainsKey(start))
            return new Dictionary<int, int>();

        var distances = new Dictionary<int, int>();
        var vertices = adjacencyList.Keys.ToList();

        // Initialize distances
        foreach (var vertex in vertices)
        {
            distances[vertex] = int.MaxValue;
        }
        distances[start] = 0;

        // Relax edges V-1 times
        for (int i = 0; i < vertices.Count - 1; i++)
        {
            foreach (var source in vertices)
            {
                if (distances[source] == int.MaxValue)
                    continue;

                foreach (var edge in adjacencyList[source])
                {
                    if (distances[source] + edge.Weight < distances[edge.Destination])
                    {
                        distances[edge.Destination] = distances[source] + edge.Weight;
                    }
                }
            }
        }

        // Check for negative weight cycles
        foreach (var source in vertices)
        {
            if (distances[source] == int.MaxValue)
                continue;

            foreach (var edge in adjacencyList[source])
            {
                if (distances[source] + edge.Weight < distances[edge.Destination])
                {
                    Console.WriteLine("Graph contains negative weight cycle!");
                    return new Dictionary<int, int>();
                }
            }
        }

        return distances;
    }

    // Floyd-Warshall for all-pairs shortest path
    public int[,] FloydWarshall()
    {
        var vertices = adjacencyList.Keys.ToList();
        int n = vertices.Count;
        var dist = new int[n, n];

        // Initialize matrix
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                dist[i, j] = (i == j) ? 0 : int.MaxValue / 2;
            }
        }

        // Fill with edge weights
        for (int i = 0; i < n; i++)
        {
            foreach (var edge in adjacencyList[vertices[i]])
            {
                int j = vertices.IndexOf(edge.Destination);
                dist[i, j] = edge.Weight;
            }
        }

        // Floyd-Warshall algorithm
        for (int k = 0; k < n; k++)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (dist[i, k] + dist[k, j] < dist[i, j])
                    {
                        dist[i, j] = dist[i, k] + dist[k, j];
                    }
                }
            }
        }

        return dist;
    }

    public void Display()
    {
        foreach (var kvp in adjacencyList)
        {
            var edges = kvp.Value.Select(e => $"{e.Destination}({e.Weight})");
            Console.WriteLine($"{kvp.Key} -> [{string.Join(", ", edges)}]");
        }
    }
}
//6.Graph Utilities and Extended Features
//csharp
public static class GraphUtils
{
    // Detect cycle in undirected graph
    public static bool HasCycleUndirected(UndirectedUnweightedGraph graph)
    {
        var visited = new HashSet<int>();

        foreach (var vertex in graph.GetAllVertices())
        {
            if (!visited.Contains(vertex))
            {
                if (HasCycleUndirectedDFS(vertex, -1, visited, graph))
                    return true;
            }
        }
        return false;
    }

    private static bool HasCycleUndirectedDFS(int vertex, int parent,
                                               HashSet<int> visited,
                                               UndirectedUnweightedGraph graph)
    {
        visited.Add(vertex);

        foreach (var neighbor in graph.GetNeighbors(vertex))
        {
            if (!visited.Contains(neighbor))
            {
                if (HasCycleUndirectedDFS(neighbor, vertex, visited, graph))
                    return true;
            }
            else if (neighbor != parent)
            {
                return true;
            }
        }
        return false;
    }

    // Check if graph is connected (undirected)
    public static bool IsConnected(UndirectedUnweightedGraph graph)
    {
        var vertices = graph.GetAllVertices();
        if (vertices.Count == 0)
            return true;

        var visited = new HashSet<int>();
        var queue = new Queue<int>();

        queue.Enqueue(vertices.First());
        visited.Add(vertices.First());

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            foreach (var neighbor in graph.GetNeighbors(current))
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        return visited.Count == vertices.Count;
    }

    // Find all connected components (undirected)
    public static List<List<int>> GetConnectedComponents(UndirectedUnweightedGraph graph)
    {
        var components = new List<List<int>>();
        var visited = new HashSet<int>();

        foreach (var vertex in graph.GetAllVertices())
        {
            if (!visited.Contains(vertex))
            {
                var component = new List<int>();
                var queue = new Queue<int>();

                queue.Enqueue(vertex);
                visited.Add(vertex);

                while (queue.Count > 0)
                {
                    int current = queue.Dequeue();
                    component.Add(current);

                    foreach (var neighbor in graph.GetNeighbors(current))
                    {
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            queue.Enqueue(neighbor);
                        }
                    }
                }
                components.Add(component);
            }
        }

        return components;
    }
}

//Extension methods for graphs
public static class GraphExtensions
{
    public static List<int> GetAllVertices(this UndirectedUnweightedGraph graph)
    {
        // This would need to be implemented by exposing the adjacency list
        // or using reflection. For simplicity, we add a method to each graph class.
        return new List<int>();
    }
}

class Program
{
    static void Main(string[] args)
    {
        var undirectedGraph = new UndirectedUnweightedGraph();
        undirectedGraph.AddEdge(0, 1);
        undirectedGraph.AddEdge(0, 2);
        undirectedGraph.AddEdge(1, 3);
        undirectedGraph.AddEdge(2, 3);
        undirectedGraph.AddEdge(3, 4);

        Console.WriteLine("Graph: ");
        undirectedGraph.Display();
        Console.WriteLine();


        undirectedGraph.BFS(0);
        undirectedGraph.DFS(0);
        Console.WriteLine();

    }
}
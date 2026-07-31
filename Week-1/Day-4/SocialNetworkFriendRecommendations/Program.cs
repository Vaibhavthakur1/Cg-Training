using System;
using System.Collections.Generic;
using System.Linq;

public class SocialNetwork
{
    private readonly int _numUsers;
    private readonly Dictionary<int, List<int>> _adjList;

    // Initializes the social network with a specified number of users
    public SocialNetwork(int numUsers)
    {
        _numUsers = numUsers;
        _adjList = new Dictionary<int, List<int>>();

        for (int i = 0; i < numUsers; i++)
        {
            _adjList[i] = new List<int>();
        }
    }

    // Adds a mutual (undirected) friendship edge between two users.
    public void AddFriendship(int u, int v)
    {
        _adjList[u].Add(v);
        _adjList[v].Add(u);
    }

    //Retrieves all direct friends of a given user;
    public List<int> GetFriends(int user)
    {
        return new List<int>(_adjList[user]);
    }

    //check if two user are connectd directly or indirectly using BFS
    public bool IsConnected(int start, int target)
    {
        if (start == target) return true;

        var visited = new HashSet<int> { start };
        var queue = new Queue<int>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            int curr = queue.Dequeue();

            foreach (int neighbor in _adjList[curr])
            {
                if (neighbor == target) return true;

                if (visited.Add(neighbor))
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        return false;
    }
    // Finds the shortest path between two users using BFS
    public List<int> FindShortestPath(int start, int target)
    {
        if (start == target) return new List<int> { start };

        var parent = new Dictionary<int, int>();
        var visited = new HashSet<int> { start };
        var queue = new Queue<int>();

        queue.Enqueue(start);
        bool found = false;

        while (queue.Count > 0)
        {
            int curr = queue.Dequeue();

            if (curr == target)
            {
                found = true;
                break;
            }

            foreach (int neighbor in _adjList[curr])
            {
                if (visited.Add(neighbor))
                {
                    parent[neighbor] = curr;
                    queue.Enqueue(neighbor);
                }
            }
        }

        if (!found) return new List<int>();

        // Reconstruct path backward from target to start
        var path = new List<int>();
        int currNode = target;

        while (currNode != start)
        {
            path.Add(currNode);
            currNode = parent[currNode];
        }
        path.Add(start);
        path.Reverse();

        return path;
    }


    // Finds all users exactly at a given distance (k steps) from a start user using level-order BFS.
    public List<int> GetUsersAtDistance(int start, int targetDistance)
    {
        var visited = new HashSet<int> { start };
        var queue = new Queue<int>();
        queue.Enqueue(start);

        int currentDistance = 0;

        while (queue.Count > 0 && currentDistance < targetDistance)
        {
            int levelSize = queue.Count;
            currentDistance++;

            for (int i = 0; i < levelSize; i++)
            {
                int curr = queue.Dequeue();

                foreach (int neighbor in _adjList[curr])
                {
                    if (visited.Add(neighbor))
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        return currentDistance == targetDistance ? queue.ToList() : new List<int>();
    }

    // Detects if there is any cycle in the undirected network using Depth-First Search (DFS).
    public bool HasCycle()
    {
        var visited = new bool[_numUsers];

        for (int i = 0; i < _numUsers; i++)
        {
            if (!visited[i])
            {
                if (DfsHasCycle(i, parent: -1, visited))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool DfsHasCycle(int u, int parent, bool[] visited)
    {
        visited[u] = true;

        foreach (int neighbor in _adjList[u])
        {
            if (!visited[neighbor])
            {
                if (DfsHasCycle(neighbor, u, visited))
                {
                    return true;
                }
            }
            else if (neighbor != parent)
            {
                // Visited node that is not the direct parent indicates a back-edge (cycle)
                return true;
            }
        }

        return false;
    }

    // Finds all connected components (separate friend groups) in the network.
    public List<List<int>> FindConnectedComponents()
    {
        var visited = new bool[_numUsers];
        var components = new List<List<int>>();

        for (int i = 0; i < _numUsers; i++)
        {
            if (!visited[i])
            {
                var component = new List<int>();
                BfsComponent(i, visited, component);
                components.Add(component);
            }
        }

        return components;
    }

    private void BfsComponent(int start, bool[] visited, List<int> component)
    {
        var queue = new Queue<int>();
        queue.Enqueue(start);
        visited[start] = true;

        while (queue.Count > 0)
        {
            int curr = queue.Dequeue();
            component.Add(curr);

            foreach (int neighbor in _adjList[curr])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        // Task 1: Create a social network with 6 users (0-5)
        var network = new SocialNetwork(numUsers: 6);

        // Task 2: Add friendships: 0-1, 0-2, 1-3, 2-3, 2-4, 3-5, 4-5
        var friendships = new (int u, int v)[]
        {
                (0, 1), (0, 2), (1, 3), (2, 3), (2, 4), (3, 5), (4, 5)
        };

        foreach (var (u, v) in friendships)
        {
            network.AddFriendship(u, v);
        }

        // Task 3: Find all friends of user 2
        var friendsUser2 = network.GetFriends(2);
        Console.WriteLine($"1. Friends of User 2: [{string.Join(", ", friendsUser2)}]");

        // Task 4: Check if user 0 and user 5 are connected
        bool isConnected = network.IsConnected(0, 5);
        Console.WriteLine($"2. Are User 0 and User 5 connected? {isConnected}");

        // Task 5: Find the shortest path between user 0 and user 5
        var shortestPath = network.FindShortestPath(0, 5);
        Console.WriteLine($"3. Shortest path between User 0 and User 5: [{string.Join(" -> ", shortestPath)}]");

        // Task 6: Find all users at distance 2 from user 1
        var dist2Users = network.GetUsersAtDistance(1, targetDistance: 2);
        Console.WriteLine($"4. Users at distance 2 from User 1: [{string.Join(", ", dist2Users)}]");

        // Task 7: Detect if there's a cycle in the network
        bool hasCycle = network.HasCycle();
        Console.WriteLine($"5. Has cycle in network: {hasCycle}");

        // Task 8: Find all connected components (friend groups)
        var components = network.FindConnectedComponents();
        Console.WriteLine("6. Connected components (Friend Groups):");
        for (int i = 0; i < components.Count; i++)
        {
            Console.WriteLine($"   Group {i + 1}: [{string.Join(", ", components[i])}]");
        }
    }
}







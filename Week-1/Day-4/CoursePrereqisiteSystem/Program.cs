using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class CousrePrerequisiteSystem
{

    private readonly int _numCourses;
    private readonly Dictionary<int, List<int>> _adjList;
    private readonly int[] _inDegree;


    //initialize the course system with a specified number of courses.
    public CousrePrerequisiteSystem(int numCourses)
    {
        _numCourses = numCourses;
        _adjList = new Dictionary<int, List<int>>();
        _inDegree = new int[numCourses];
        for(int i = 0; i < numCourses; i++)
        {
            _adjList[i] = new List<int>();

        }
    }
        
    //Adds a directed edge from prerequisite too dependent cousrse
    public void AddPrerequisite(int prereq, int course)
    {
        _adjList[prereq].Add(course);
        _inDegree[course]++;
    }

    //Find all course directly required for a given target course;
    public List<int> GetDirectPrerequisites(int targetCourse)
    {
        var directPrereqs = new List<int>();

        for(int course = 0; course < _numCourses; course++)
        {
            if (_adjList[course].Contains(targetCourse))
            {
                directPrereqs.Add(course);
            }
        }
        return directPrereqs;
    }

    public HashSet<int> GetAllPrerequisites(int targetCourse)
    {
        var allPrereqs = new HashSet<int>();
        var queue = new Queue<int>();
        queue.Enqueue(targetCourse);

        // Construct reverse adjacency map: Dependent -> Direct Prerequisites
        var reverseAdj = new Dictionary<int, List<int>>();
        for (int i = 0; i < _numCourses; i++)
        {
            reverseAdj[i] = new List<int>();
        }

        foreach (var kvp in _adjList)
        {
            int u = kvp.Key;
            foreach (int v in kvp.Value)
            {
                reverseAdj[v].Add(u);
            }
        }

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            foreach (int prereq in reverseAdj[current])
            {
                if (allPrereqs.Add(prereq))
                {
                    queue.Enqueue(prereq);
                }
            }
        }

        return allPrereqs;
    }
        
    public bool HasCycle()
    {
        int[] inDegCopy = (int[])_inDegree.Clone();
        var queue = new Queue<int>();
        for (int i = 0; i < _numCourses; i++)
        {
            if (inDegCopy[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        int processedCount = 0;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            processedCount++;

            foreach (int neighbor in _adjList[current])
            {
                inDegCopy[neighbor]--;
                if (inDegCopy[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        return processedCount != _numCourses;
    }

    //perform a topological sor using Kahn's Algorithm to find a valid learning order
    public List<int> TopologicalSort()
    {
        int[] inDegCopy = (int[])_inDegree.Clone();
        var queue = new Queue<int>();
        var order = new List<int>();

        for (int i = 0; i < _numCourses; i++)
        {
            if (inDegCopy[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            order.Add(current);

            foreach (int neighbor in _adjList[current])
            {
                inDegCopy[neighbor]--;
                if (inDegCopy[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        if (order.Count != _numCourses)
        {
            return new List<int>(); // Graph contains a cycle
        }

        return order;
    }

    // Finds all courses that have no prerequisites and can be taken first.
    public List<int> GetCoursesWithoutPrerequisites()
    {
        var startCourses = new List<int>();
        for (int i = 0; i < _numCourses; i++)
        {
            if (_inDegree[i] == 0)
            {
                startCourses.Add(i);
            }
        }
        return startCourses;
    }


    // Counts how many courses directly depend on a given course
    public int CountDirectDependents(int course)
    {
        return _adjList[course].Count;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Task 1: Create a course dependency graph with 6 courses (0 to 5)
        var system = new CousrePrerequisiteSystem(numCourses: 6);

        // Task 2: Add prerequisites
        var dependencies = new (int Prereq, int Course)[]
        {
                (0, 1), // Course 1 needs Course 0
                (0, 2), // Course 2 needs Course 0
                (1, 3), // Course 3 needs Course 1
                (2, 3), // Course 3 needs Course 2
                (2, 4), // Course 4 needs Course 2
                (3, 5), // Course 5 needs Course 3
                (4, 5)  // Course 5 needs Course 4
        };

        foreach (var (prereq, course) in dependencies)
        {
            system.AddPrerequisite(prereq, course);
        }

        // Task 3: Determine all prerequisites (direct and indirect) for Course 5
        var prereqs5 = system.GetAllPrerequisites(5).OrderBy(c => c);
        Console.WriteLine($"1. All prerequisites (direct & indirect) for Course 5: [{string.Join(", ", prereqs5)}]");

        // Task 4: Find all courses that are directly required for Course 3
        var direct3 = system.GetDirectPrerequisites(3);
        Console.WriteLine($"2. Directly required courses for Course 3: [{string.Join(", ", direct3)}]");

        // Task 5: Check if graph has a cycle
        bool isCyclic = system.HasCycle();
        Console.WriteLine($"3. Has cycle: {isCyclic}");

        // Task 6: Topological sort
        if (!isCyclic)
        {
            var order = system.TopologicalSort();
            Console.WriteLine($"4. Valid Course Order (Topological Sort): [{string.Join(", ", order)}]");
        }

        // Task 7: Courses with no prerequisites
        var noPrereqs = system.GetCoursesWithoutPrerequisites();
        Console.WriteLine($"5. Courses with no prerequisites: [{string.Join(", ", noPrereqs)}]");

        // Task 8: Count direct dependents of Course 2
        int dependents2Count = system.CountDirectDependents(2);
        Console.WriteLine($"6. Direct dependents on Course 2: {dependents2Count}");
    }
}


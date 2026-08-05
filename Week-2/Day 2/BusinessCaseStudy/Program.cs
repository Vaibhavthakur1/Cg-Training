using System;
using System.Collections.Generic;

class Employee
{
    public int EmployeeId;
    public string Name;
    public string Designation;
    public string Department;
    public int ManagerId;

    public Employee(int id, string name, string designation, string department, int managerId)
    {
        EmployeeId = id;
        Name = name;
        Designation = designation;
        Department = department;
        ManagerId = managerId;
    }
}

class Program
{
    static List<Employee> employees = new List<Employee>
    {
        new Employee(1001,"John Smith","CEO","Management",0),
        new Employee(1002,"Michael Johnson","IT Manager","IT",1001),
        new Employee(1003,"Sarah Williams","HR Manager","HR",1001),
        new Employee(1004,"David Brown","Finance Manager","Finance",1001),
        new Employee(1005,"Robert Davis","Team Lead","IT",1002),
        new Employee(1006,"Jennifer Miller","QA Lead","IT",1002),
        new Employee(1007,"William Wilson","Senior Developer","IT",1005),
        new Employee(1008,"Emma Moore","Senior Developer","IT",1005),
        new Employee(1009,"Daniel Taylor","QA Engineer","IT",1006),
        new Employee(1010,"Sophia Anderson","QA Engineer","IT",1006),
        new Employee(1011,"James Thomas","Recruiter","HR",1003),
        new Employee(1012,"Olivia Jackson","Recruiter","HR",1003),
        new Employee(1013,"Benjamin White","Accountant","Finance",1004),
        new Employee(1014,"Charlotte Harris","Accountant","Finance",1004),
        new Employee(1015,"Lucas Martin","Developer","IT",1007),
        new Employee(1016,"Ethan Walker","Developer","IT",1007),
        new Employee(1017,"Mia Hall","UI Developer","IT",1008),
        new Employee(1018,"Alexander Young","Business Analyst","IT",1005),
        new Employee(1019,"Harper King","HR Executive","HR",1011),
        new Employee(1020,"Jack Scott","Finance Executive","Finance",1013)
    };

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("ABC TECHNOLOGIES");
            Console.WriteLine("Organization Hierarchy Management System");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Display Complete Organization Chart");
            Console.WriteLine("2. Find Employee by ID");
            Console.WriteLine("3. Find Employee by Name");
            Console.WriteLine("4. Display Employees under a Manager");
            Console.WriteLine("5. Count Total Employees under a Manager");
            Console.WriteLine("6. Display Hierarchy Level");
            Console.WriteLine("7. Exit");
            Console.Write("Enter Choice: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Employee ceo = employees.Find(e => e.ManagerId == 0);
                    Console.WriteLine("\n" + ceo.Name + " (" + ceo.Designation + ")");
                    DisplayHierarchy(ceo.EmployeeId, "");
                    break;

                case 2:
                    Console.Write("Enter Employee ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Employee emp = employees.Find(e => e.EmployeeId == id);
                    if (emp != null)
                    {
                        Console.WriteLine($"Name: {emp.Name}");
                        Console.WriteLine($"Designation: {emp.Designation}");
                        Console.WriteLine($"Department: {emp.Department}");
                    }
                    else Console.WriteLine("Employee Not Found");
                    break;

                case 3:
                    Console.Write("Enter Employee Name: ");
                    string name = Console.ReadLine().ToLower();
                    emp = employees.Find(e => e.Name.ToLower() == name);
                    if (emp != null)
                    {
                        Console.WriteLine($"ID: {emp.EmployeeId}");
                        Console.WriteLine($"Designation: {emp.Designation}");
                        Console.WriteLine($"Department: {emp.Department}");
                    }
                    else Console.WriteLine("Employee Not Found");
                    break;

                case 4:
                    Console.Write("Enter Manager ID: ");
                    id = int.Parse(Console.ReadLine());
                    DisplayEmployees(id);
                    break;

                case 5:
                    Console.Write("Enter Manager ID: ");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Total Employees: " + CountEmployees(id));
                    break;

                case 6:
                    Console.Write("Enter Employee ID: ");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Hierarchy Level: " + GetLevel(id));
                    break;

                case 7:
                    return;
            }
        }
    }

    // Recursive method to print organization hierarchy
    static void DisplayHierarchy(int managerId, string indent)
    {
        List<Employee> subs = employees.FindAll(e => e.ManagerId == managerId);

        for (int i = 0; i < subs.Count; i++)
        {
            Employee e = subs[i];

            Console.Write(indent);
            Console.Write(i == subs.Count - 1 ? "└── " : "├── ");
            Console.WriteLine($"{e.Name} ({e.Designation})");

            string newIndent = indent + (i == subs.Count - 1 ? "    " : "│   ");

            DisplayHierarchy(e.EmployeeId, newIndent);
        }
    }

    // Recursive method to display all employees under a manager
    static void DisplayEmployees(int managerId)
    {
        List<Employee> subs = employees.FindAll(e => e.ManagerId == managerId);

        foreach (Employee e in subs)
        {
            Console.WriteLine($"{e.EmployeeId} - {e.Name} ({e.Designation})");
            DisplayEmployees(e.EmployeeId);
        }
    }

    // Recursive method to count employees under a manager
    static int CountEmployees(int managerId)
    {
        int count = 0;

        List<Employee> subs = employees.FindAll(e => e.ManagerId == managerId);

        foreach (Employee e in subs)
        {
            count++;
            count += CountEmployees(e.EmployeeId);
        }

        return count;
    }

    // Recursive method to find hierarchy level
    static int GetLevel(int employeeId)
    {
        Employee emp = employees.Find(e => e.EmployeeId == employeeId);

        if (emp == null)
            return -1;

        if (emp.ManagerId == 0)
            return 1;

        return 1 + GetLevel(emp.ManagerId);
    }
}
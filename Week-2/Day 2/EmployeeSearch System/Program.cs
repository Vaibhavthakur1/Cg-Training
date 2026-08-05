using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Cryptography;
class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Department { get; set; }
    public string Designation { get; set; }

    public int Experience { get; set; }

    public double Salary { get; set; }

    public string City { get; set; }

    public Employee(int id,string name,string department,string designation,int experience,double salary,string city)
    {
        Id = id;
        Name = name;
        Department = department;
        Designation = designation;
        Experience = experience;
        Salary = salary;
        City = city;
    }
    public override string ToString()
    {
        return $"ID: {Id}\n" +
               $"Name: {Name}\n" +
               $"Department: {Department}\n" +
               $"Designation: {Designation}\n" +
               $"Experience: {Experience}\n" +
               $"Salary: {Salary}\n" +
               $"City: {City}\n";
    }




}
class Program
{   
    static void Display(List<Employee> employees)
    {
        foreach(var item in employees)
        {
            Console.WriteLine("ID:"+item.Id);
            Console.WriteLine("Employee Name:"+item.Name);
            Console.WriteLine("Department:"+item.Department);
            Console.WriteLine("Designation:"+item.Designation);
            Console.WriteLine("Experience:"+item.Experience);
            Console.WriteLine();
        }
    }

    static int SearchByIdLiner(List<Employee> employees,int target)
    {
        for(int i=0;i<employees.Count;i++)
        {
            if (employees[i].Id == target)
            {
                return i;
            }
        }
        return -1;
    }
    static int SearchByIdBinary(List<Employee> employees, int target)
    {
        int low = 0;
        int high = employees.Count - 1;

        while (low <= high)
        {
            int mid = (low + high) / 2;

            if (employees[mid].Id == target)
            {
                return mid;
            }else if (employees[mid].Id < target)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }
        return -1;
    }

    static int SearchByName(List<Employee> employees ,string name)
    {
        for(int i=0;i<employees.Count;i++)
        {
            if (employees[i].Name == name)
            {
                return i;
            }
        }
        return -1;
    }

    static void SearchByDepName(List<Employee> employees,string Dname)
    {
        bool found= false;
      foreach(var emp in employees)
        {
            if (emp.Department == Dname)
            {
                Console.WriteLine(emp);
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("Employee in not found.");
        }
    }

    static void SearchByCity(List<Employee> employees, string city)
    {
        bool found = false;

        foreach (var emp in employees)
        {
            if (emp.City == city)
            {
                Console.WriteLine(emp);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("City not found.");
        }
    }

    static void SearchByExperience(List<Employee> employees, int experience)
    {
        bool found = false;

        foreach (var emp in employees)
        {
            if (emp.Experience == experience)
            {
                Console.WriteLine(emp);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No employees found.");
        }
    }

    static void SearchBySalary(List<Employee> employees, double salary)
    {
        bool found = false;

        foreach (var emp in employees)
        {
            if (emp.Salary == salary)
            {
                Console.WriteLine(emp);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No employees found.");
        }
    }





    static void Main(string[] args)
    {
    

        List<Employee> employees = new List<Employee>

        {
            new Employee(1001,"Rahul Sharma","IT","Software Engineer",2,45000,"Chennai"),

            new Employee(1002,"Priya Singh","HR","HR Executive",3,40000,"Bangalore"),

            new Employee(1003,"Amit Kumar","Finance","Accountant",5,55000,"Hyderabad"),

            new Employee(1004,"Neha Patel","IT","Senior Developer",6,85000,"Pune"),

            new Employee(1005,"Arjun Reddy","Sales","Sales Executive",2,38000,"Chennai"),

            new Employee(1006,"Sneha Iyer","Marketing","Marketing Executive",4,52000,"Coimbatore"),

            new Employee(1007,"Karan Mehta","IT","Team Lead",8,95000,"Mumbai"),

            new Employee(1008,"Divya Nair","Support","Support Engineer",1,32000,"Kochi"),

            new Employee(1009,"Rohit Verma","IT","Software Engineer",3,50000,"Delhi"),

            new Employee(1010,"Anjali Gupta","Finance","Financial Analyst",4,65000,"Noida"),

            new Employee(1011,"Suresh Kumar","Admin","Administrator",7,58000,"Madurai"),

            new Employee(1012,"Pooja Sharma","HR","Recruiter",2,42000,"Bangalore"),

            new Employee(1013,"Vikram Das","IT","System Engineer",5,62000,"Chennai"),

            new Employee(1014,"Meena Joshi","Support","Technical Support",3,41000,"Trichy"),

            new Employee(1015,"Naveen Raj","Sales","Sales Manager",9,98000,"Salem"),

            new Employee(1016,"Kavya R","Marketing","SEO Analyst",2,45000,"Chennai"),

            new Employee(1017,"Ajay Kumar","IT","DevOps Engineer",4,72000,"Hyderabad"),

            new Employee(1018,"Lakshmi Devi","Finance","Senior Accountant",6,76000,"Coimbatore"),

            new Employee(1019,"Manoj Singh","IT","QA Engineer",3,53000,"Pune"),

            new Employee(1020,"Deepika Rao","HR","HR Manager",8,90000,"Bangalore")

        };


        Console.WriteLine("===================================");
        Console.WriteLine("ABC Technologies\nEmployee Search Management System");
        Console.WriteLine("===================================");
        bool running = true;
        
        while (running)
        {
            Console.WriteLine("1. Display All Employees");
            Console.WriteLine("2. Search by Employee ID (Linear Search)");
            Console.WriteLine("3. Search by Employee ID (Binary Search)");
            Console.WriteLine("4 Search by Employee Name");
            Console.WriteLine("5. Search by Department");
            Console.WriteLine("6. Search by City");
            Console.WriteLine("7. Search by Experience");
            Console.WriteLine("8. Search by Salary");
            Console.WriteLine("9. Exit");
            Console.WriteLine("Enter Your Choice:");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Display(employees);
                    break;
                case 2:
                    Console.WriteLine("Enter the id to search");
                    int target = Convert.ToInt32(Console.ReadLine());
                    int index = SearchByIdLiner(employees, target);
                    if (index != -1)
                    {
                        Console.WriteLine($"Id found at {index}");
                        Console.WriteLine(employees[index]);
                    }
                    else
                    {
                        Console.WriteLine("Employee not found.");
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter the id to Search: ");
                    int target2 = Convert.ToInt32(Console.ReadLine());
                    int index2 = SearchByIdBinary(employees, target2);
                    if (index2 != -1)
                    {
                        Console.WriteLine($"Id found at {index2}");
                        Console.WriteLine(employees[index2]);
                    }
                    else
                    {
                        Console.WriteLine("Employee not found.");
                    }
                    break;
                case 4:
                    Console.WriteLine("Enter the name to search");
                    string name = Console.ReadLine();
                    int index3 = SearchByName(employees, name);
                    if (index3 != -1)
                    {
                        Console.WriteLine($"Id found at {index3}");
                        Console.WriteLine(employees[index3]);
                    }
                    else
                    {
                        Console.WriteLine("Employee not found.");
                    }
                    break;
                case 5:
                    Console.WriteLine("Enter the Department Name:");
                    string DepName = Console.ReadLine();
                    SearchByDepName(employees, DepName);
                    break;
                case 6:
                    Console.Write("Enter City: ");
                    string city = Console.ReadLine();
                    SearchByCity(employees, city);
                    break;

                case 7:
                    Console.Write("Enter Experience: ");
                    int experience = Convert.ToInt32(Console.ReadLine());
                    SearchByExperience(employees, experience);
                    break;

                case 8:
                    Console.Write("Enter Salary: ");
                    double salary = Convert.ToDouble(Console.ReadLine());
                    SearchBySalary(employees, salary);
                    break;

                case 9:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Enter the valid choice");
                    break;

            }

        }


    }
}
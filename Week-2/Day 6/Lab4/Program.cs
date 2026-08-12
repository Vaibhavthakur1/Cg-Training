using System;
using System.Data;

public abstract class Employee
{
    public string Name { get; }
    public decimal BaseSalary { get; }
    protected Employee(string name, decimal baseSalary)
    { 
        Name = name; 
        BaseSalary = baseSalary;
    }
    public abstract decimal CalculatePay();
    public void PrintPaySlip() => Console.WriteLine($"{Name}: ${CalculatePay():N2}");
}
public class SalariedEmployee: Employee
{
    public SalariedEmployee(string name, decimal baseSalary) : base(name, baseSalary)
    {
    }

    public override decimal CalculatePay()
    {
        return BaseSalary;
    }
}

public class CommissionEmployee : Employee
{
    public decimal CommissionEarned;

    public CommissionEmployee(string name,decimal baseSalary,decimal commission): base(name, baseSalary)
    {
        CommissionEarned = commission;
    }

    public override decimal CalculatePay()
    {
        return BaseSalary + CommissionEarned;
    }
}

public class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>
        {
            new SalariedEmployee("Alice", 4500),
            new SalariedEmployee("Bob", 3200),
            new CommissionEmployee("Carla", 3500, 650)
        };


        foreach (Employee employee in employees)
        {
            employee.PrintPaySlip();
        }

    }
}

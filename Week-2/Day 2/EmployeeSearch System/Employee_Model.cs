using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeSearch_System
{
    // Employee class stores employee details
    class Employee_Model
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Department { get; set; }
        public string Designation { get; set; }

        public int Experience { get; set; }

        public double Salary { get; set; }

        public string City { get; set; }

        public Employee_Model(int id, string name, string department, string designation, int experience, double salary, string city)
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
}

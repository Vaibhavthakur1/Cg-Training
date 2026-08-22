using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Marks { get; set; } = 0;

    public Student(int id, string name, double marks)
    {
        Id = Convert.ToInt32(id);
        Name = name;
        Marks = marks;
    }

    public override string ToString()
    {
        return $"Id: {Id} Name: {Name} Marks: {Marks}";
    }

}
class Program
{   
    static void AddStudent(List<Student> student)
    {
        Console.WriteLine("Enter the id");
        int id=int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the Name of student");
        string name = (Console.ReadLine());

        Console.WriteLine("Enter Marks of student");
        double marks = Double.Parse(Console.ReadLine());

        student.Add(new Student(id, name, marks));
        Console.WriteLine("Sutdent Added successfully");

    }

    static void RemoveStudent(List<Student>student,int id)
    {

        int removeCount=student.RemoveAll(s=>s.Id == id);
        if (removeCount > 0)
        {
            Console.WriteLine("Student removed successfully");
        }
        else
        {
            Console.WriteLine("Student not found");

        }

     }
    public static void UpdateMarks(List<Student> student,int id,double newMarks)
    {
        Student studentf = student.Find(s => s.Id == id);

        if (studentf != null)
        {
            studentf.Marks = newMarks;
            Console.WriteLine($"Marks updated successfully for {studentf.Name} (ID: {id}) from  {studentf.Marks} to {newMarks}");
        }
        else
        {
            Console.WriteLine($"No student with id:{id} found");
        }
    }
    static Student GetTopStudent(List<Student> students)
    {
        if(students ==null || students.Count == 0)
        {
            return null;
        }


        return students.OrderByDescending(s => s.Marks).FirstOrDefault();
    }




    static void Main(string[] args)
    { 
        List < Student > list = new List<Student>();
        Console.WriteLine("Enter the number of student");
        int noOfStudent = int.Parse(Console.ReadLine());
        for (int i = 0; i < noOfStudent; i++)
        {
            AddStudent(list);
    }
    //RemoveStudent(list, 103);

        foreach(var item in list)
        {
            Console.WriteLine(item);
        }

//UpdateMarks(list, 101, 99);
        
    Console.WriteLine("Top Student is: "+GetTopStudent(list));


    }
}
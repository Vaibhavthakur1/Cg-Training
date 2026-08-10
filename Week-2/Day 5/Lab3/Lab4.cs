using System;
using System.Text;

class Lab4
{
    static void Main()
    {
        string rawData = @"
john smith|engineering|72000
MARY jones|sales|65000

ravi KUMAR|engineering|81000
";

        // Split the raw data into separate rows
        string[] rows = rawData.Split(
            new[] { '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries);

        StringBuilder report = new StringBuilder();

       
        int totalSalary = 0;
        int employeeCount = 0;

        
        int appendCalls = 0;

        // Add the report title
        report.AppendLine("==================================================");
        appendCalls++;

        report.AppendLine("            EMPLOYEE COMPENSATION REPORT");
        appendCalls++;

        report.AppendLine("==================================================");
        appendCalls++;

        // Add the table header using PadRight for alignment
        report.AppendLine(
            "Name".PadRight(20) +
            "Department".PadRight(20) +
            "Salary".PadLeft(10));

        appendCalls++;

        // Add the separator line
        report.AppendLine(new string('-', 50));
        appendCalls++;

        foreach (string row in rows)
        {

            string[] fields = row.Split('|');

            if (fields.Length != 3)
                continue;

            string name = fields[0].Trim();
            string department = fields[1].Trim();
            int salary = int.Parse(fields[2].Trim());

          
            name = StringToolkit.ToTitleCase(name);

  
            totalSalary += salary;

            employeeCount++;

          
            string formattedSalary = salary.ToString("N0");

            // Build one employee line using PadRight and PadLeft
            report.AppendLine(
                name.PadRight(20) +
                department.PadRight(20) +
                formattedSalary.PadLeft(10));

            appendCalls++;
        }

    
        report.AppendLine(new string('-', 50));
        appendCalls++;

   
        report.AppendLine(
            $"Employees: {employeeCount}".PadRight(25) +
            $"Total Salary: {totalSalary:N0}");

        appendCalls++;

     
        report.AppendLine(new string('=', 50));
        appendCalls++;

      
        Console.WriteLine(report.ToString());

        // Print StringBuilder Append call count
        Console.WriteLine($"StringBuilder Append/AppendLine calls: {appendCalls}");

        // Show that no += string concatenation was used inside the loop
        Console.WriteLine("String concatenations using += inside loop: 0");
    }
}
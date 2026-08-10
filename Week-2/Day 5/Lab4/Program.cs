using System;
using System.Text;

class Lab4
{
    static void Main()
    {
        // Raw employee data where each employee is on a separate line
        string rawData = @"
        john smith|engineering|72000
        MARY jones|sales|65000

        ravi KUMAR|engineering|81000
        ";

        // Split the raw data into separate rows
        string[] rows = rawData.Split(
            new[] { '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries);

        // Create StringBuilder to build the complete report
        StringBuilder report = new StringBuilder();

        // Keep track of total salary and number of employees
        int totalSalary = 0;
        int employeeCount = 0;

        // Keep track of StringBuilder Append and AppendLine calls
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

        // Process every employee row
        foreach (string row in rows)
        {
            // Split each row into name, department and salary
            string[] fields = row.Split('|');

            // Skip invalid rows
            if (fields.Length != 3)
                continue;

            // Extract the employee information
            string name = fields[0].Trim();
            string department = fields[1].Trim();
            int salary = int.Parse(fields[2].Trim());

            // Convert employee name to title case
            name = StringToolkit.ToTitleCase(name);

            // Add salary to the total
            totalSalary += salary;

            // Increase employee count
            employeeCount++;

            // Format the salary with comma separators
            string formattedSalary = salary.ToString("N0");

            // Build one employee line using PadRight and PadLeft
            report.AppendLine(
                name.PadRight(20) +
                department.PadRight(20) +
                formattedSalary.PadLeft(10));

            appendCalls++;
        }

        // Add the separator before the footer
        report.AppendLine(new string('-', 50));
        appendCalls++;

        // Add employee count and total salary
        report.AppendLine(
            $"Employees: {employeeCount}".PadRight(25) +
            $"Total Salary: {totalSalary:N0}");

        appendCalls++;

        // Add the final separator
        report.AppendLine(new string('=', 50));
        appendCalls++;

        // Print the complete report
        Console.WriteLine(report.ToString());

        // Print StringBuilder Append call count
        Console.WriteLine($"StringBuilder Append/AppendLine calls: {appendCalls}");

        // Show that no += string concatenation was used inside the loop
        Console.WriteLine("String concatenations using += inside loop: 0");
    }
}
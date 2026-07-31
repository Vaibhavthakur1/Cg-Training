using System;

namespace StringHandlingAssignment
{
    class Program
    {
        static string[] employees =
        {
            "EMP001|John Smith|IT|john.smith@company.com",
            "EMP002|Alice Johnson|HR|alice.johnson@company.com",
            "EMP003|David Wilson|Finance|david.wilson@company.com",
            "EMP004|Emma Brown|IT|emma.brown@company.com",
            "EMP005|James Miller|Sales|james.miller@company.com"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("========== STRING HANDLING ASSIGNMENT ==========\n");

            DisplayEmployees();

            Console.WriteLine("\n------------------------------------");
            DisplayUpperCaseNames();

            Console.WriteLine("\n------------------------------------");
            DisplayInitials();

            Console.WriteLine("\n------------------------------------");
            DisplayITEmployees();

            Console.WriteLine("\n------------------------------------");
            CountEmployees();

            Console.WriteLine("\n------------------------------------");
            SearchEmployee("EMP003");

            Console.WriteLine("\n------------------------------------");
            ValidateEmails();

            Console.WriteLine("\n------------------------------------");
            ReplaceDepartment();

            Console.WriteLine("\n------------------------------------");
            CountNameCharacters();

            Console.WriteLine("\n------------------------------------");
            ExtractEmailUserNames();

            Console.ReadKey();
        }

        static void DisplayEmployees()
        {
            Console.WriteLine("TASK 1 : Employee Details\n");

            foreach (string emp in employees)
            {
                string[] data = emp.Split('|');

                Console.WriteLine("Employee ID : " + data[0]);
                Console.WriteLine("Name        : " + data[1]);
                Console.WriteLine("Department  : " + data[2]);
                Console.WriteLine("Email       : " + data[3]);
                Console.WriteLine();
            }
        }

        static void DisplayUpperCaseNames()
        {
            Console.WriteLine("TASK 2 : Uppercase Names\n");

            foreach (string emp in employees)
            {
                string[] data = emp.Split('|');
                Console.WriteLine(data[1].ToUpper());
            }
        }

        static void DisplayInitials()
        {
            Console.WriteLine("TASK 3 : Employee Initials\n");

            foreach (string emp in employees)
            {
                string[] data = emp.Split('|');

                string[] names = data[1].Split(' ');

                string initials = "";

                foreach (string n in names)
                {
                    initials += n.Substring(0, 1);
                }

                Console.WriteLine(data[1] + " -> " + initials);
            }
        }

        static void DisplayITEmployees()
        {
            Console.WriteLine("TASK 4 : IT Department Employees\n");

            foreach (string emp in employees)
            {
                if (emp.Contains("|IT|"))
                {
                    string[] data = emp.Split('|');
                    Console.WriteLine(data[1]);
                }
            }
        }

        static void CountEmployees()
        {
            Console.WriteLine("TASK 5 : Count Employees\n");

            Console.WriteLine("Total Employees = " + employees.Length);
        }

        static void SearchEmployee(string id)
        {
            Console.WriteLine("TASK 6 : Search Employee\n");

            bool found = false;

            foreach (string emp in employees)
            {
                if (emp.StartsWith(id))
                {
                    string[] data = emp.Split('|');

                    Console.WriteLine("Employee Found");
                    Console.WriteLine("Name : " + data[1]);
                    Console.WriteLine("Department : " + data[2]);
                    Console.WriteLine("Email : " + data[3]);

                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Employee Not Found");
            }
        }

        static void ValidateEmails()
        {
            Console.WriteLine("TASK 7 : Email Validation\n");

            foreach (string emp in employees)
            {
                string[] data = emp.Split('|');

                if (data[3].EndsWith("@company.com"))
                {
                    Console.WriteLine(data[3] + " -> Valid");
                }
                else
                {
                    Console.WriteLine(data[3] + " -> Invalid");
                }
            }
        }

        static void ReplaceDepartment()
        {
            Console.WriteLine("TASK 8 : Replace Department\n");

            foreach (string emp in employees)
            {
                Console.WriteLine(emp.Replace("|IT|", "|Information Technology|"));
            }
        }

        static void CountNameCharacters()
        {
            Console.WriteLine("TASK 9 : Name Character Count\n");

            foreach (string emp in employees)
            {
                string[] data = emp.Split('|');

                Console.WriteLine(data[1] + " = " + data[1].Length);
            }
        }

        static void ExtractEmailUserNames()
        {
            Console.WriteLine("TASK 10 : Email User Names\n");

            foreach (string emp in employees)
            {
                string[] data = emp.Split('|');

                int index = data[3].IndexOf('@');

                string username = data[3].Substring(0, index);

                Console.WriteLine(username);
            }
        }
    }
}


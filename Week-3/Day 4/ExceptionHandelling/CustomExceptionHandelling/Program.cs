using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptionExampleCode
{
    class MyException : Exception
    {
        public MyException() { }
        public MyException(string Message) : base(Message) { }
        public MyException(string Message, Exception inner) : base(Message, inner) { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Validating user age...");
                ValidateAge(15);
            }
            catch (MyException ex)
            {
                Console.WriteLine($"Caught MyException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught General Exception: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Execution completed.");
            }
        }

        static void ValidateAge(int age)
        {
            if (age < 18)
            {
                throw new MyException("User must be at least 18 years old to proceed.");
            }
            Console.WriteLine("User age is valid.");
        }
    }
}
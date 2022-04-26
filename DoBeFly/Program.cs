using System;

namespace DoBeFly
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var ctx = new DoBeFlyContext();
            var e = ctx.Database.EnsureCreated();

            if (e)
                Console.WriteLine("Database has been created.");
            else
                Console.WriteLine("Database already exists");
            Console.WriteLine("Done.");

            //add an employee
            Employee employee = new Employee { PassportNumber = "E6R83932", Salary = 9000, HireDate = DateTime.Now };

            //ctx.EmployeeSet.Add(employee);

            Console.WriteLine();

            ctx.SaveChanges();

        }

    }
}

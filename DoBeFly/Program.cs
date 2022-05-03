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

            Console.WriteLine("Employee added");



            //add an employee
            Pilot pilotOne = new Pilot { Surname = "Jean-Claude Dusse", Salary = 10000 };

            ctx.PilotSet.Add(pilotOne);

            Console.WriteLine("Pilot added");

            ctx.SaveChanges();


            foreach (Flight f in ctx.FlightSet)
                Console.WriteLine("Date: {0}, Destination: {1}, Seats: {2}", f.Date, f.Destination, f.Seats);

            var FlightsToBoraBora = ctx.FlightSet.Where(f => f.Destination == "BoraBora" && f.Seats < 100).ToList<Flight>();

            var FlightToBoraBoraTwo = from Flight in ctx.FlightSet
                                      where Flight.Destination == "BoraBora" && Flight.Seats < 100
                                      select Flight;

            //this is to display
            var FlightToBoraBoraThree = FlightToBoraBoraTwo.ToList();

            foreach (Flight f in ctx.FlightsToBoraBoraTwo)
                Console.WriteLine("Date: {0}, Destination: {1}, Seats: {2}", f.Date, f.Destination, f.Seats);

            Booking b1 = new Booking() { flight = f2, Passenger = new Passenger() { Surname = "Gates", GivenName = "Bill" } };

            Passenger p1 = new Passenger() { Surname = "Musk", GivenName = "Elon" };

            ctx.PassengerSet.Add(p1);
            ctx.BookingSet.Add(b1);


            Booking b2 = new Booking() { flight = f4, Passenger = p1 };
            ctx.BookingSet.Add(b2);

            ctx.SaveChanges();


        }

    }
}

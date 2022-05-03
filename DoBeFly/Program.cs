using System;
using System.Linq;

namespace DoBeFly
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new DoBeFlyContext();
            var e = context.Database.EnsureCreated();

            if (e)
                Console.WriteLine("Database has been created.");
            else
                Console.WriteLine("Database already exists");
            Console.WriteLine("Done.");


            //Create pilots
            Pilot p1 = new Pilot() { Surname = "Jean-Luc", Salary = 10000 };
            context.PilotSet.Add(p1);
            Console.WriteLine("Pilot " + p1.Surname + " created");


            //Create flights and add them into the dbSet
            Flight f1 = new Flight() { Seats = 100, Destination = "BoraBora" };
            Flight f2 = new Flight() { Seats = 200, Destination = "Ireland" };
            context.Flightset.Add(f1);
            Console.WriteLine("Flight to " + f1.Destination + " added");
            context.Flightset.Add(f2);
            Console.WriteLine("Flight to " + f2.Destination + " added");


            //Create booking
            Booking b1 = new Booking() { flight = f2, passenger = new Passenger() { Surname = "Gates", GivenName = "Bill" } };
            Passenger pa1 = new Passenger() { Surname = "Musk", GivenName = "Elon" };
            Booking b2 = new Booking() { flight = f2, passenger = pa1 };
            context.PassengerSet.Add(pa1);
            context.BookingSet.Add(b1);
            context.BookingSet.Add(b2);


            //Update pilot 
            var pilotsList = context.PilotSet.ToList<Pilot>();
            Pilot pilotOneToUpdate = pilotsList.Where(s => s.Surname == "Jean-Luc").FirstOrDefault<Pilot>();
            //pilotOneToUpdate.Salary = 9000;
            //pilotOneToUpdate.HireDate = DateTime.Now;
            //Console.WriteLine("Pilot " + pilotOneToUpdate.Surname + " updated");


            //Delete pilot
            //context.PilotSet.Remove(pilotsList.ElementAt<Pilot>(0));


            //Save changes at the end of the transaction
            context.SaveChanges();


            //For each to print all the flights of the database
            foreach (Flight f in context.Flightset)
                Console.WriteLine("Date: {0}, Destination: {1}, Seats: {2}", f.Date, f.Destination, f.Seats);

            //Get a flight
            var FlightsToBoraBora = context.Flightset.Where(f => f.Destination == "BoraBora").ToList<Flight>();
            Console.WriteLine(FlightsToBoraBora.ToString());
            //Or          
            var FlightsToBoraBoraTwo = from Flight in context.Flightset
                                       where Flight.Destination == "BoraBora" && Flight.Seats > 0
                                       select Flight;

            //this is to display
            var FlightToBoraBoraThree = FlightsToBoraBoraTwo.ToList();


            Console.WriteLine("\nThis are our flights !!!");
            foreach (Flight f in FlightsToBoraBoraTwo)
                Console.WriteLine("Date: {0}, Destination: {1}, Seats: {2}", f.Date, f.Destination, f.Seats);

        }

    }
}

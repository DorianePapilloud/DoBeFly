using System;
using System.Linq;

namespace DoBeFly
{
    class Program
    {
        static void Main(string[] args)
        {
            initializeDatabase();
        }

        private static void initializeDatabase()
        {
            var context = new DoBeFlyContext();
            var e = context.Database.EnsureCreated();
            if (e)
                Console.WriteLine("Database has been created.");
            else
                Console.WriteLine("Database already exists");
            Console.WriteLine("Done.");

            //Create pilots
            //Pilot pilot1 = new Pilot() { FirstName = "Maverick", Salary = 10000, HireDate = new DateTime(2010, 01, 01) };
            //Pilot pilot2 = new Pilot() { FirstName = "Goose", Salary = 8000, HireDate = new DateTime(2014, 07, 01) };
            //Pilot pilot3 = new Pilot() { FirstName = "Iceman", Salary = 90000, HireDate = new DateTime(2015, 09, 01) };
            //context.PilotSet.Add(pilot1);
            //context.PilotSet.Add(pilot2);
            //context.PilotSet.Add(pilot3);


            //Create flights
            //Flight flight1 = new() { BasePrice = 1000, Seats = 100, Departure = "Geneva", Destination = "Tokyo", FreeSeats = 100, Date = new DateTime(2022, 06, 20, 12, 30, 0), PilotId = 1 };
            //Flight flight2 = new() { BasePrice = 1000, Seats = 100, Departure = "Geneva", Destination = "Tokyo", FreeSeats = 10, Date = new DateTime(2022, 06, 20, 12, 30, 0), PilotId = 1 };
            //Flight flight3 = new() { BasePrice = 1000, Seats = 100, Departure = "Geneva", Destination = "Tokyo", FreeSeats = 10, Date = new DateTime(2022, 10, 20, 12, 30, 0), PilotId = 1 };
            //Flight flight4 = new() { BasePrice = 1000, Seats = 100, Departure = "Geneva", Destination = "Tokyo", FreeSeats = 90, Date = new DateTime(2022, 07, 20, 12, 30, 0), PilotId = 1 };
            //Flight flight5 = new() { BasePrice = 1000, Seats = 100, Departure = "Geneva", Destination = "Tokyo", FreeSeats = 90, Date = new DateTime(2022, 06, 25, 12, 30, 0), PilotId = 1 };
            //Flight flight6 = new() { BasePrice = 1000, Seats = 100, Departure = "Geneva", Destination = "Tokyo", FreeSeats = 90, Date = new DateTime(2022, 09, 25, 12, 30, 0), PilotId = 1 };
            //context.FlightSet.Add(flight1);
            //context.FlightSet.Add(flight2);
            //context.FlightSet.Add(flight3);
            //context.FlightSet.Add(flight4);
            //context.FlightSet.Add(flight5);
            //context.FlightSet.Add(flight6);


            //Create booking
            //Booking booking1 = new Booking();
            //booking1.Flight = flight1;
            //booking1.BookingPrice = 350;
            //booking1.Passenger = new Passenger() { LastName = "Gates", FirstName = "Bill" };

            //Passenger passenger1 = new Passenger() { LastName = "Musk", FirstName = "Elon" };
            //Booking booking2 = new Booking() { Flight = flight2, BookingPrice = 200, Passenger = passenger1 };
            //context.PassengerSet.Add(passenger1);
            //context.BookingSet.Add(booking1);
            //context.BookingSet.Add(booking2);

            Passenger passenger1 = new Passenger() { LastName = "Coimbra", FirstName = "Daniel", Email = "Daniel@coimbra.ch" };
            context.PassengerSet.Add(passenger1);

            //Update pilot 
            //var pilotsList = context.PilotSet.ToList<Pilot>();
            //Pilot pilotOneToUpdate = pilotsList.Where(s => s.Surname == "Jean-Luc").FirstOrDefault<Pilot>();
            //pilotOneToUpdate.Salary = 9000;
            //pilotOneToUpdate.HireDate = DateTime.Now;
            //Console.WriteLine("Pilot " + pilotOneToUpdate.Surname + " updated");


            //Delete pilot
            //context.PilotSet.Remove(pilotsList.ElementAt<Pilot>(0));


            //Save changes at the end of the transaction
            context.SaveChanges();


            ////Display all the flights
            //Console.WriteLine("\n==================================================================");
            //Console.WriteLine("ALL OUR FLIGHTS");
            //foreach (Flight flight in context.FlightSet)
            //    Console.WriteLine("Date: {0}, Destination: {1}, Seats: {2}", flight.Date, flight.Destination, flight.Seats);
            //Console.WriteLine("==================================================================\n");


            ////Get the flights to bora 
            //Console.WriteLine("\n==================================================================");
            //Console.WriteLine("ALL OUR FLIGHTS TO BORA BORA");
            //var FlightsToBoraBora = context.FlightSet.Where(f => f.Destination == "BoraBora").ToList<Flight>();
            //Console.WriteLine(FlightsToBoraBora.ToString());
            ////Or          
            //var FlightsToBoraBoraTwo = from Flight in context.FlightSet
            //                           where Flight.Destination == "BoraBora" && Flight.Seats > 0
            //                           select Flight;
            //foreach (Flight f in FlightsToBoraBoraTwo)
            //    Console.WriteLine("Date: {0}, Destination: {1}, Seats: {2}", f.Date, f.Destination, f.Seats);
            //Console.WriteLine("==================================================================\n");


            //this is to display
            //var FlightToBoraBoraThree = FlightsToBoraBoraTwo.ToList();

            //get the pilot of one 
            //var FlightElon = context.FlightSet.Where(f => f.BookingSet.Any(b => b.Passenger == p1));
            //foreach (Flight f in FlightElon)

            //    Console.WriteLine("Date: {0}, Destination: {1}, Pilot: {2}", f.Date, f.Destination, f.Pilot.GivenName);
        }

    }
}

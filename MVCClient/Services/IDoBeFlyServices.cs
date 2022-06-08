using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Services
{
    public interface IDoBeFlyServices
    {

        public Task<IEnumerable<FlightM>> GetFlights();

        public Task<FlightM> GetFlight(int id);

        public Task<IEnumerable<BookingM>> GetAllBookings();

        public Task<IEnumerable<BookingM>> GetBookingForDestination(string destination);

        public Task<bool> BuyTicket(TicketM ticketM);

        public Task<IEnumerable<PassengerM>> GetAllPassengers();

        public Task<List<string>> GetAllDestinations();

        public Task<PassengerM> GetPassenger(int id);

        public Task<int> GetTotalPrice(int idFlight);

        public Task<int> GetAveragePrice(string Destination);



    }
}

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

        //From Flights controller
        public Task<IEnumerable<FlightM>> GetFlights();

        public Task<FlightM> GetFlight(int id);


        //From Bookings controller
        public Task<IEnumerable<BookingM>> GetAllBookings();

        public Task<IEnumerable<BookingM>> GetBookingForDestination(string destination);

        public Task<bool> BuyTicket(TicketM ticketM);

        public Task<List<string>> GetAllDestinations();

        public Task<int> GetTotalPrice(int idFlight);

        public Task<int> GetAveragePrice(string Destination);

        public Task<bool> DeleteBooking(int idBooking);


    }
}

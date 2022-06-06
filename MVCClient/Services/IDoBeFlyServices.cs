﻿using Microsoft.AspNetCore.Mvc;
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

        public Task<TicketM> BuyTicket(TicketM ticketM);

        public Task<IEnumerable<PassengerM>> GetAllPassengers();

        public Task<PassengerM> GetPassenger(int id);



    }
}

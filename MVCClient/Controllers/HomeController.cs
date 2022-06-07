using DoBeFly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCClient.Models;
using MVCClient.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDoBeFlyServices _dobeFly;

        public HomeController(ILogger<HomeController> logger, IDoBeFlyServices dobeFly)
        {
            _logger = logger;
            _dobeFly = dobeFly;
        }

        public async Task<IActionResult> Index()
        {
            var listFlights = await _dobeFly.GetFlights();
            return View(listFlights);
        }

        public async Task<IActionResult> FlightDetails(int id)
        {
            var flight = await _dobeFly.GetFlight(id);
            TicketM ticket = new TicketM();
            ticket.FlightM = flight;
            return View(ticket);
        }

        public async Task<IActionResult> Bookings()
        {
            var allBookings = await _dobeFly.GetAllBookings();
            foreach (BookingM bookingM in allBookings)
            {
                var idFlight = bookingM.Flight.FlightId;
                var flight = await _dobeFly.GetFlight(idFlight);
                bookingM.Flight.Destination = flight.Destination;
            }

            allBookings = allBookings.OrderByDescending(b => b.BookingNo).ToList();
            return View(allBookings);
        }

        [HttpPost]
        public async Task<IActionResult> Bookings(string destination)
        {
            var test = destination;
            if (destination == null)
            {
                var allBookings = await _dobeFly.GetAllBookings();
                foreach (BookingM bookingM in allBookings)
                {
                    var idFlight = bookingM.Flight.FlightId;
                    var flight = await _dobeFly.GetFlight(idFlight);
                    bookingM.Flight.Destination = flight.Destination;
                }
                allBookings = allBookings.OrderByDescending(b => b.BookingNo).ToList();
                return View(allBookings);
            }
            else
            {
                var allBookings = await _dobeFly.GetBookingForDestination(destination);
                foreach (BookingM bookingM in allBookings)
                {
                    var idFlight = bookingM.Flight.FlightId;
                    var flight = await _dobeFly.GetFlight(idFlight);
                    bookingM.Flight.Destination = flight.Destination;
                }
                return View(allBookings);
            }
        }

        [HttpPost]
        public async Task<IActionResult> BuyTicket (TicketM newTicket)
        {
            await _dobeFly.BuyTicket(newTicket);
            return View();
        }

        public async Task<IActionResult> AveragePrice()
        {
            var avg = await _dobeFly.GetAveragePrice("Dublin");
            return View(avg);
        }

        public async Task<IActionResult> TotalPrice()
        {
            var total = await _dobeFly.GetTotalPrice(2);
            return View(total);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

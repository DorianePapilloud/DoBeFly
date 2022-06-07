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


        public IActionResult HomePage()
        {
            return View();
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
            ticket.Average = await _dobeFly.GetAveragePrice(flight.Destination);
            ticket.TotalSalePrice = await _dobeFly.GetTotalPrice(flight.FlightNo);
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

        public async Task<IActionResult> AveragePrice(string destination)
        {
            TicketM ticket = new TicketM();
            ticket.Average = await _dobeFly.GetAveragePrice(destination);
            return View(ticket.Average);
        }

        public async Task<IActionResult> TotalPrice(int idFlight)
        {
            TicketM ticket = new TicketM();
            ticket.TotalSalePrice = await _dobeFly.GetTotalPrice(idFlight);
            return View(ticket.TotalSalePrice);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

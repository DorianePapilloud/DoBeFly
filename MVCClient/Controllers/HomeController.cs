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
            listFlights = listFlights.OrderBy(f => f.Date).ToList();
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
            var model = new BookingMDestination();
            model.listBookingsM = (List<BookingM>)await _dobeFly.GetAllBookings();
            model.listDestinations = await _dobeFly.GetAllDestinations();

            foreach (BookingM bookingM in model.listBookingsM)
            {
                var idFlight = bookingM.Flight.FlightId;
                var flight = await _dobeFly.GetFlight(idFlight);
                bookingM.Flight.Destination = flight.Destination;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Bookings(string destination)
        {
            var model = new BookingMDestination();
            model.listDestinations = await _dobeFly.GetAllDestinations();

            if (destination == null)
                model.listBookingsM = (List<BookingM>)await _dobeFly.GetAllBookings();

            else
                model.listBookingsM = (List<BookingM>)await _dobeFly.GetBookingForDestination(destination);
        
            foreach (BookingM bookingM in model.listBookingsM)
            {
                var idFlight = bookingM.Flight.FlightId;
                var flight = await _dobeFly.GetFlight(idFlight);
                bookingM.Flight.Destination = flight.Destination;
            }
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> BuyTicket (TicketM newTicket)
        {
            await _dobeFly.BuyTicket(newTicket);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

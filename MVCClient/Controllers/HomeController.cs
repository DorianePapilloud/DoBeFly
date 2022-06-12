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
        private readonly IDoBeFlyServices _dobeFlyService;

        public HomeController(ILogger<HomeController> logger, IDoBeFlyServices dobeFly)
        {
            _logger = logger;
            _dobeFlyService = dobeFly;
        }

        //Start up page
        public IActionResult HomePage()
        {
            return View();
        }

        //Main page with all flights
        public async Task<IActionResult> Index()
        {
            var listFlights = await _dobeFlyService.GetFlights();
            listFlights = listFlights.OrderBy(f => f.Date).ToList(); //order the list by date
            return View(listFlights);
        }

        //Page with flight details and where the passenger can enter his information to buy a ticket
        public async Task<IActionResult> FlightDetails(int id)
        {
            var flight = await _dobeFlyService.GetFlight(id);
            TicketM ticket = new TicketM();
            ticket.FlightM = flight;
            ticket.Average = await _dobeFlyService.GetAveragePrice(flight.Destination);
            ticket.TotalSalePrice = await _dobeFlyService.GetTotalPrice(flight.FlightNo);
            return View(ticket);
        }

        //Page with all bookings
        public async Task<IActionResult> Bookings()
        {
            var model = new BookingMDestination(); //used another model to get also the list of all destinations
            model.listBookingsM = (List<BookingM>)await _dobeFlyService.GetAllBookings();
            model.listDestinations = await _dobeFlyService.GetAllDestinations();

            foreach (BookingM bookingM in model.listBookingsM)
            {
                var idFlight = bookingM.Flight.FlightId;
                var flight = await _dobeFlyService.GetFlight(idFlight);
                bookingM.Flight.Destination = flight.Destination;
            }
            return View(model);
        }

        //Post method for the bookings page, the user can filter all bookings
        //for a specific destination he wants to display
        [HttpPost]
        public async Task<IActionResult> Bookings(string destination)
        {
            var model = new BookingMDestination();
            model.listDestinations = await _dobeFlyService.GetAllDestinations();

            if (destination == null) //Reset filter
                model.listBookingsM = (List<BookingM>)await _dobeFlyService.GetAllBookings();

            else //chose with parameter
                model.listBookingsM = (List<BookingM>)await _dobeFlyService.GetBookingForDestination(destination);
        
            foreach (BookingM bookingM in model.listBookingsM)
            {
                var idFlight = bookingM.Flight.FlightId;
                var flight = await _dobeFlyService.GetFlight(idFlight);
                bookingM.Flight.Destination = flight.Destination;
            }
            return View(model);

        }

        //Post method to buy a new ticket for a flight with information on a passenger
        [HttpPost]
        public async Task<IActionResult> BuyTicket (TicketM newTicket)
        {
            await _dobeFlyService.BuyTicket(newTicket);
            return View(newTicket);
        }

        public async Task<IActionResult> DeleteBooking(int id)
        {
            var BookingM = new BookingM();
            BookingM.BookingNo = id;
            await _dobeFlyService.DeleteBooking(id);
            return View(BookingM);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

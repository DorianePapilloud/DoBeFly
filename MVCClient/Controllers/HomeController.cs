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

        public async Task<IActionResult> FlightDetails(int flightNo)
        {
            var listFlights = await _dobeFly.GetFlights();
            foreach (FlightM f in listFlights)
            {
                if (f.FlightNo == flightNo)
                    return View(f);
            }
            return null;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Bookings()
        {
            var listBooking = await _dobeFly.GetAllBookings();
            return View(listBooking);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

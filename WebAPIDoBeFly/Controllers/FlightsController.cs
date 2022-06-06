using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoBeFly;
using WebAPIDoBeFly.Models;
using WebAPIDoBeFly.Extensions;

namespace WebAPIDoBeFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly DoBeFlyContext _context;

        public FlightsController(DoBeFlyContext context)
        {
            _context = context;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightM>>> GetFlightSet()
        {
            var flightList = await _context.FlightSet.ToListAsync();
            List<FlightM> listFlightsMs = new List<FlightM>();

            foreach (Flight f in flightList)
            {
                var fM = f.ConvertToFlightM();
                fM.FlightPrice = calculatePrice(f);
                // Only if the flight still has empty seats
                if (fM.FreeSeats > 0)
                    listFlightsMs.Add(fM);
            }

            return listFlightsMs;
        }

        private int calculatePrice(Flight f)
        {
            DateTime today = DateTime.Now;

            //If the airplane is more than 80% full regardless of the date:.sale price = 150% of the base price
            if (f.FreeSeats / f.Seats < 0.2)
                return (int)(f.BasePrice * 1.5);

            //If the plane is filled less than 20% less than 2 months before departure: sale price = 80% of the base price
            else if (f.FreeSeats / f.Seats > 0.8 && f.Date.Month < today.Month+2)
                return (int)(f.BasePrice * 0.8);

            //If the plane is filled less than 50% less than 1 month before departure: sale price = 70% of the base price
            else if (f.FreeSeats / f.Seats > 0.5 && f.Date.Month < today.Month+1)
                return (int)(f.BasePrice * 0.7);

            //In all other cases: sale price = base price*
            else return f.BasePrice;
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightM>> GetFlight(int id)
        {
            var flight = await _context.FlightSet.FindAsync(id);
            if (flight == null)
                return NotFound();

            else
            {
                var fM = flight.ConvertToFlightM();
                fM.FlightPrice = calculatePrice(flight);
                return fM;
            }
        }

        // PUT: api/Flights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            if (id != flight.FlightId)
            {
                return BadRequest();
            }

            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Flights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(FlightM flightM)
        {
            _context.FlightSet.Add(flightM.ConvertToFlightDoBeFly());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlight", new { id = flightM.FlightNo }, flightM);
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var flight = await _context.FlightSet.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            _context.FlightSet.Remove(flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlightExists(int id)
        {
            return _context.FlightSet.Any(e => e.FlightId == id);
        }
    }
}

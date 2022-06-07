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
    public class BookingsController : ControllerBase
    {
        private readonly DoBeFlyContext _context;

        public BookingsController(DoBeFlyContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingM>>> GetBookingSet()
        {
            var bookingList = await _context.BookingSet.ToListAsync();
            List<BookingM> listBookingMs = new List<BookingM>();
            List<string> destinations = new List<string>();

            foreach (Booking booking in bookingList)
            {
                var bookingM = booking.ConvertToBookingM();

                var id = booking.FlightId;
                bookingM.Flight = await _context.FlightSet.FindAsync(id);

                var idP = booking.PassengerId;
                bookingM.Passenger = await _context.PassengerSet.FindAsync(idP);

                //var destination = bookingM.Flight.Destination
                //bookingM.Destinations.Add(destination);
                //if (!destinations.Equals(destination))
                //{
                //    if (destinations.Count == 0)
                //    {
                //        destinations = new List<string>();
                //        destinations.Add(destination);
                //    }
                //    else
                //    {
                //        destinations.Add(destination);
                //    }
                //}
                //bookingM.Destinations = destinations;
                
                listBookingMs.Add(bookingM);
            }        


            return listBookingMs;
        }

        // GET: api/Bookings
        [Route("GetBookingForDestination/{destination}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingM>>> GetBookingForDestination(string destination)
        {
            var bookingList = _context.BookingSet.Where(f => f.Flight.Destination == destination).ToList<Booking>();
            List<BookingM> listBookingMs = new List<BookingM>();

            foreach (Booking booking in bookingList)
            {
                var bookingM = booking.ConvertToBookingM();

                var id = booking.FlightId;
                bookingM.Flight = await _context.FlightSet.FindAsync(id);

                var idP = booking.PassengerId;
                bookingM.Passenger = await _context.PassengerSet.FindAsync(idP);

                listBookingMs.Add(bookingM);
            }

            return listBookingMs;
        }

        // GET: api/Bookings/5
        [HttpGet("{idBooking}")]
        public async Task<ActionResult<Booking>> GetBooking(int idBooking)
        {
            var booking = await _context.BookingSet.FindAsync(idBooking);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }
        
        [Route("GetTotalPrice/{idFlight}")]
        [HttpGet]
        public int GetTotalPrice (int idFlight)
        {
            var list = _context.BookingSet.Where(f => f.Flight.FlightId == idFlight).ToList<Booking>();
            var total = 0;

            foreach (var booking in list)
                total += booking.BookingPrice;

            return total;
        }

        [Route("GetAveragePrice/{Destination}")]
        [HttpGet]
        public int GetAveragePrice(string Destination)
        {
            var list = _context.BookingSet.Where(f => f.Flight.Destination == Destination).ToList<Booking>();
            var nb = list.Count;
            var avg = 0;

            foreach (var Dest in list)
                avg += Dest.BookingPrice;

            avg /= nb;
            
            return avg;
        }


        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBooking(int id, Booking booking)
        //{
        //    if (id != booking.BookingId)
        //    {
        //        return BadRequest();
        //    }

        //    //_context.BookingSet.Add(booking);
        //    _context.Entry(booking).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookingExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<bool> PostBooking(TicketM ticketM)
        {

            //Create passenger object to insert in the database if doesn't exist
            var passenger = new Passenger()
            {
                FirstName = ticketM.FirstName,
                LastName = ticketM.LastName,
                Email = ticketM.Email,
                Birthday = ticketM.Birthday
            };

            //Check if passenger exists in the database
            var passengersList = await _context.PassengerSet.ToListAsync();
            Passenger passenger_ = passengersList.Where(p => p.Email == ticketM.Email)
                .FirstOrDefault<Passenger>();
            if (passenger_ == null)
            {
                passenger_ = new Passenger() { FirstName = ticketM.FirstName, LastName = ticketM.LastName, Email = ticketM.Email, Birthday = ticketM.Birthday };
                _context.PassengerSet.Add(passenger_);
                await _context.SaveChangesAsync();
            }

            passengersList = await _context.PassengerSet.ToListAsync();
            var passengerNew_ = passengersList.Where(p => p.Email == ticketM.Email)
                                .FirstOrDefault<Passenger>();

            //Create booking object to insert in the database afterwards
            Booking booking = new Booking();
            booking.BookingPrice = ticketM.FlightM.FlightPrice;
            booking.FlightId = ticketM.FlightM.FlightNo;
            booking.PassengerId = passenger_.Person.PersonId;

            //Add booking
            _context.BookingSet.Add(booking);

            //Update free seats for the flight
            var flightsList = await _context.FlightSet.ToListAsync();
            var flightToUpdate = flightsList.Where(p => p.FlightId == ticketM.FlightM.FlightNo)
                .FirstOrDefault<Flight>();
            flightToUpdate.FreeSeats--;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingExists(booking.BookingId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
            //return CreatedAtAction("BuyTicket", new { id = booking.BookingId }, booking);
        }

        // DELETE: api/Bookings/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBooking(int id)
        //{
        //    var booking = await _context.BookingSet.FindAsync(id);
        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.BookingSet.Remove(booking);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool BookingExists(int id)
        {
            return _context.BookingSet.Any(e => e.BookingId == id);
        }
    }
}

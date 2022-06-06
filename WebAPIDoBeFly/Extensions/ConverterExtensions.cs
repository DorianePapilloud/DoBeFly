using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDoBeFly.Extensions
{
    public static class ConverterExtensions
    {

        public static Models.FlightM ConvertToFlightM (this DoBeFly.Flight f)
        { 
            Models.FlightM fM = new Models.FlightM();

            fM.FlightNo = f.FlightId;
            fM.Destination = f.Destination;
            fM.Date = f.Date;
            fM.Seats = f.Seats;
            fM.FreeSeats = f.FreeSeats;

            return fM;
        }


        //public static Models.TicketM ConvertToTicketM(this DoBeFly.Booking b)
        //{
        //    Models.TicketM tM = new Models.TicketM();

        //    tM.FlightNo = b.FlightId;
        //    tM.FirstName = b.Passenger.FirstName;
        //    tM.LastName = b.Passenger.LastName;
        //    tM.SalePrice = b.BookingPrice;

        //    return tM;
        //}

        public static DoBeFly.Flight ConvertToFlightDoBeFly (this Models.FlightM fM)
        {
            DoBeFly.Flight f = new DoBeFly.Flight();

            f.FlightId = fM.FlightNo;
            f.Destination = fM.Destination;
            f.Date = fM.Date;
            f.Seats = f.Seats;
            f.FreeSeats = fM.FreeSeats;

            return f;
        }

        public static Models.BookingM ConvertToBookingM (this DoBeFly.Booking booking)
        {
            Models.BookingM bookingM = new Models.BookingM();

            bookingM.BookingNo = booking.BookingId;
            bookingM.Price = booking.BookingPrice;
            bookingM.Passenger = booking.Passenger;
            bookingM.Flight = booking.Flight;

            return bookingM;
        }

        public static DoBeFly.Booking ConvertToBookingDoBeFly (this Models.BookingM bookingM)
        {
            DoBeFly.Booking booking = new DoBeFly.Booking();

            booking.BookingId = bookingM.BookingNo;
            booking.BookingPrice = bookingM.Price;
            booking.Passenger = bookingM.Passenger;
            booking.Flight = bookingM.Flight;

            return booking;
        }

    }
}

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
            fM.Date = f.Date;
            fM.Departure = f.Departure;
            fM.Destination = f.Destination;
            fM.FlightNo = f.FlightNo;

            return fM;

        }

        public static DoBeFly.Flight ConvertToFlightDoBeFly (this Models.FlightM fM)
        {

            DoBeFly.Flight f = new DoBeFly.Flight();
            f.Date = fM.Date;
            f.Departure = fM.Departure;
            f.Destination = fM.Destination;
            f.FlightNo = fM.FlightNo;

            return f;

        }

    }
}

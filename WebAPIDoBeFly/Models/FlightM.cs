using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDoBeFly.Models
{
    public class FlightM
    {
        public int FlightNo { get; set; }

        public string Destination { get; set; }

        public DateTime Date { get; set; }

        public int Seats { get; set; }

        public int FreeSeats { get; set; }

        public int FlightPrice { get; set; }
    }
}

using DoBeFly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDoBeFly.Models
{
    public class BookingM
    {
        public int BookingNo { get; set; }

        public int Price { get; set; }

        public Passenger Passenger { get; set; }

        public Flight Flight { get; set; }

        public List<string> Destinations { get; set; }

    }
}

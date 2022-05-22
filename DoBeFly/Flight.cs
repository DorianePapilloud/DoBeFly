using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBeFly
{
    [Table("Flight")]
    public class Flight
    {
        public int FlightId { get; set; }

        public int PilotId { get; set; }

        public int CopilotId { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Departure { get; set; }
        
        public string Destination { get; set; }

        public int Seats { get; set; }

        public int FreeSeats { get; set; }

        public int BasePrice { get; set; }


        //public int AircraftTypeID { get; set; }

        //public int AirlineCode { get; set; }

        //public string Memo { get; set; }

        //public Boolean NonSmokingFlight { get; set; }

        //public Boolean Strikebound { get; set; }

        //public DateTime Timestamp { get; set; }

        //public string Utilization { get; set; }

    }
}

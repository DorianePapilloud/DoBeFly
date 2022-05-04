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

        [Key]
        public int FlightNo { get; set; }
        
        public int AircraftTypeID { get; set; }
        
        public int AirlineCode { get; set; }
        
        public int CopilotID { get; set; }
        
        public DateTime Date { get; set; }
        
        public DateTime Departure { get; set; }
        
        public string Destination { get; set; }
        
        public Boolean FreeSeats { get; set; }
        
        public string Memo { get; set; }
        
        public Boolean NonSmokingFlight { get; set; }
        
        public int PilotId { get; set; }
        
        public double Price { get; set; }
        
        public int Seats { get; set; }
        
        public Boolean Strikebound { get; set; }
        
        public DateTime Timestamp { get; set; }
        
        public string Utilization { get; set; }

    }
}

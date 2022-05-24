using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBeFly
{
    [Table("Booking")]
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
            
        public int PassengerId { get; set; }

        public int FlightId { get; set; }

        public int BookingPrice { get; set; }

        public virtual Flight Flight { get; set; }
        
        public virtual Passenger Passenger { get; set; }

    }
}

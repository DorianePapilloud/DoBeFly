using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBeFly
{
    [Table("Booking")]
    public class Booking
    {

        public int FlightNo { get; set; }
        public int PassengerId { get; set; }
        public virtual Flight flight { get; set; }
        public virtual Passenger passenger { get; set; }

    }
}

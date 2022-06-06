using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDoBeFly.Models
{
    public class TicketM
    {
        public FlightM FlightM { get; set; }

        public int SalePrice { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }

    }
}

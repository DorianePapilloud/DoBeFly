using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBeFly
{
    [Table("Pilot")]
    public class Pilot : Employee
    {
        [ForeignKey("Person")]
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        public DateTime FlightHours { get; set; }

        public string FlightSchool { get; set; }
        
        public DateTime LicenseDate { get; set; }


    }
}

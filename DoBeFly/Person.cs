using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBeFly
{
    [Table("Person")]
    public class Person
    {

        public DateTime Birthday { get; set; }
        public string EMail { get; set; }
        public string FullName { get; set; }
        public string GivenName { get; set; }
        public int PersonId { get; set; }
        public string Surname { get; set; }

    }
}

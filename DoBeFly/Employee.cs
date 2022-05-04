using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoBeFly
{
    [Table("Employee")]
    public class Employee : Person
    {  
        public string PassportNumber { get; set; }
       
        public double Salary { get; set; }
        
        public DateTime HireDate { get; set; }

    }
}

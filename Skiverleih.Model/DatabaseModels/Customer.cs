using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Skiverleih.Model.DatabaseModels
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [StringLength(50)]
        public string FName { get; set; }

        [Required]
        [StringLength(50)]
        public string LName { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string Birthdate { get; set; }


        public virtual ICollection<Rental> Rentals { get; set; }
    }
}

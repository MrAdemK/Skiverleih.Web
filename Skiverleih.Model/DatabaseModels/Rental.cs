using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skiverleih.Model.DatabaseModels
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RentedDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReturnDate { get; set; }


        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}

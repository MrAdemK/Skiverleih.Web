using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skiverleih.Model.DatabaseModels;

namespace Skiverleih.Model.ViewModels.RentVM
{
    public class RentCreateEditVM
    {
        public int RentalId { get; set; }
        
        public DateTime? RentedDate { get; set; }
        
        public DateTime? ReturnDate { get; set; }

        public int CustomerId { get; set; }

        public int ArticleId { get; set; }
    }
}

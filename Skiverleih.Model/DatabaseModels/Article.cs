using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skiverleih.Model.DatabaseModels
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [Required]
        [StringLength(100)]
        public string ArticleName { get; set; }

        [Required]
        [StringLength(4)]
        public string RentPriceADay { get; set; }

        public int RentCount { get; set; }


        public int CategoryId { get; set; }
        public virtual Category Categorie { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}

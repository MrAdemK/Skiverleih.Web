using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skiverleih.Model.DatabaseModels
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        /*[Required]
        [Column(TypeName = "bit")]
        public bool Available { get; set; }*/

        [Required]
        [StringLength(3)]
        public string Available { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Skiverleih.Model.ViewModels
{
    public class SelectListItemVm
    {
        [Key]
        public object ValueMember { get; set; }

        public string DisplayMember { get; set; }
    }
}

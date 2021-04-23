using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skiverleih.Model.ViewModels.ArticleVM
{
    public class ArticleIndexVM
    {
        public int ArticleId { get; set; }

        public string ArticleName { get; set; }

        public string RentPriceADay { get; set; }

        public string CategoryName { get; set; }

        public string Available { get; set; }
    }
}

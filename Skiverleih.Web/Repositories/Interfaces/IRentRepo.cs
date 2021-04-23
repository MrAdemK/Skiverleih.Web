using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skiverleih.Model.DatabaseModels;
using Skiverleih.Model.ViewModels.RentVM;

namespace Skiverleih.Web.Repositories.Interfaces
{
    public interface IRentRepo
    {
        Task<List<RentToCustomerVM>> GetAllRent();
        Task<Customer> GetCustomerDetailsById(int? id);
        Task<Rental> GetRentalById(int? id);

        void InsertRental(Rental rental);
        void UpdateRental(Rental rental);
        void DeleteRental(Rental rental);

        Task<IEnumerable> DropdownCustomer();
        Task<IEnumerable> DropdownCategory();
        Task<IEnumerable> DropdownArticle();
    }
}

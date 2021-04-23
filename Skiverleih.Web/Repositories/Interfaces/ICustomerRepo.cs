using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skiverleih.Model.DatabaseModels;

namespace Skiverleih.Web.Repositories.Interfaces
{
    public interface ICustomerRepo
    {
        Task<List<Customer>> GetAllCustomer();
        Task<Customer> GetAllCustomerById(int? id);

        void InsertCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}

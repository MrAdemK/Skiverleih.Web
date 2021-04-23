using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Skiverleih.Model.DatabaseModels;
using Skiverleih.Web.Models;
using Skiverleih.Web.Repositories.Interfaces;

namespace Skiverleih.Web.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<Customer>> GetAllCustomer()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetAllCustomerById(int? id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public void InsertCustomer(Customer customer)
        {
            _dbContext.Customers.Attach(customer);
            _dbContext.Entry(customer).State = EntityState.Added;
        }

        public void UpdateCustomer(Customer customer)
        {
            _dbContext.Customers.Attach(customer);
            _dbContext.Entry(customer).State = EntityState.Modified;
        }

        public void DeleteCustomer(Customer customer)
        {
            _dbContext.Customers.Attach(customer);
            _dbContext.Entry(customer).State = EntityState.Deleted;
        }
    }
}
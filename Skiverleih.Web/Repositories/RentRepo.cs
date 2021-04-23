using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skiverleih.Model.ViewModels.RentVM;
using Skiverleih.Model.DatabaseModels;
using Skiverleih.Model.ViewModels;
using Skiverleih.Web.Models;
using Skiverleih.Web.Repositories.Interfaces;

namespace Skiverleih.Web.Repositories
{
    public class RentRepo : IRentRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public RentRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<RentToCustomerVM>> GetAllRent()
        {
            var temp = await (from r in _dbContext.Rentals
                    .Include(a => a.Article)
                    .Include(c => c.Article.Categorie)
                              select new RentToCustomerVM
                              {
                                  RentalId = r.RentalId,
                                  CategoryName = r.Article.Categorie.CategoryName,
                                  ArticleName = r.Article.ArticleName,
                                  LName = r.Customer.LName,
                                  RentedDate = r.RentedDate,
                                  ReturnDate = r.ReturnDate,
                                  Available = r.Article.Status.Available
                              }).ToListAsync();

            return temp;
        }

        public async Task<Customer> GetCustomerDetailsById(int? id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public async Task<Rental> GetRentalById(int? id)
        {
            return await _dbContext.Rentals.FindAsync(id);
        }

        public void InsertRental(Rental rental)
        {
            _dbContext.Rentals.Attach(rental);
            _dbContext.Entry(rental).State = EntityState.Added;
        }

        public void UpdateRental(Rental rental)
        {
            _dbContext.Rentals.Attach(rental);
            _dbContext.Entry(rental).State = EntityState.Modified;
        }

        public void DeleteRental(Rental rental)
        {
            _dbContext.Rentals.Attach(rental);
            _dbContext.Entry(rental).State = EntityState.Deleted;
        }

        public async Task<IEnumerable> DropdownCustomer()
        {
            var customer = await (from c in _dbContext.Customers
                                  orderby c.LName
                                  select new SelectListItemVm
                                  {
                                      ValueMember = c.CustomerId.ToString(),
                                      DisplayMember = c.LName.ToString()
                                  }).ToListAsync();

            return customer;
        }

        public async Task<IEnumerable> DropdownCategory()
        {
            var category = await (from ca in _dbContext.Categories
                                  orderby ca.CategoryName
                                  select new SelectListItemVm
                                  {
                                      ValueMember = ca.CategoryId.ToString(),
                                      DisplayMember = ca.CategoryName.ToString()
                                  }).ToListAsync();

            return category;
        }

        public async Task<IEnumerable> DropdownArticle()
        {
            var article = await (from a in _dbContext.Articles
                    .Where(x => x.StatusId == 1)
                                 orderby a.ArticleName
                                 select new SelectListItemVm
                                 {
                                     ValueMember = a.ArticleId.ToString(),
                                     DisplayMember = a.ArticleName.ToString()
                                 }).ToListAsync();

            return article;
        }
    }
}
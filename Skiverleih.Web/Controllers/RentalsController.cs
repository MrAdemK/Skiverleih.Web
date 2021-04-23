using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Skiverleih.Model.DatabaseModels;
using Skiverleih.Web.Models;

namespace Skiverleih.Web.Controllers
{
    public class RentalsController : Controller
    {
        private readonly UnitOfWork uow = new UnitOfWork();

        // GET: Rentals
        public async Task<ActionResult> Index()
        {
            /*var rentals = db.Rentals.Include(r => r.Article).Include(r => r.Customer);
            return View(await rentals.ToListAsync());*/

            var rentals = await uow.RentRepo.GetAllRent();
            return View(rentals);
        }

        // GET: Rentals/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = await uow.RentRepo.GetCustomerDetailsById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public async Task<ActionResult> Return(int id)
        {
            var rental = await uow.RentRepo.GetRentalById(id);
            
            rental.ReturnDate = DateTime.Now.Date;
            uow.RentRepo.UpdateRental(rental);

            var article = await uow.ArticleRepo.GetArticleById(rental.ArticleId);
            article.StatusId = 1;
            uow.RentRepo.UpdateRental(rental);

            await uow.CommitAsync();

            return RedirectToAction("Index");
        }

        // GET: Rentals/Create
        public async Task<ActionResult> Create()
        {
            //ViewBag.ArticleId = new SelectList(db.Articles, "ArticleId", "ArticleName");
            ViewBag.ArticleId = new SelectList(await uow.RentRepo.DropdownArticle(), "ValueMember", "DisplayMember");
            //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FName");
            ViewBag.CustomerId = new SelectList(await uow.RentRepo.DropdownCustomer(), "ValueMember", "DisplayMember");
            //ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.CategoryId = new SelectList(await uow.RentRepo.DropdownCategory(), "ValueMember", "DisplayMember");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RentalId,RentedDate,ReturnDate,CustomerId,ArticleId")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                var available = await uow.ArticleRepo.GetArticleById(rental.ArticleId);
                available.StatusId = 2;
                available.RentCount += 1;

                //db.Rentals.Add(rental);
                uow.RentRepo.InsertRental(rental);
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ArticleId = new SelectList(await uow.RentRepo.DropdownArticle(), "ValueMember", "DisplayMember", rental.ArticleId);
            ViewBag.CustomerId = new SelectList(await uow.RentRepo.DropdownCustomer(), "ValueMember", "DisplayMember", rental.CustomerId);
            ViewBag.CategoryId = new SelectList(await uow.RentRepo.DropdownCategory(), "ValueMember", "DisplayMember");
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Rental rental = await uow.RentRepo.GetRentalById(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticleId = new SelectList(await uow.RentRepo.DropdownArticle(), "ValueMember", "DisplayMember", rental.ArticleId);
            ViewBag.CustomerId = new SelectList(await uow.RentRepo.DropdownCustomer(), "ValueMember", "DisplayMember", rental.CustomerId);
            ViewBag.CategoryId = new SelectList(await uow.RentRepo.DropdownCategory(), "ValueMember", "DisplayMember");
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RentalId,RentedDate,ReturnDate,CustomerId,ArticleId")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                uow.RentRepo.UpdateRental(rental);
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ArticleId = new SelectList(await uow.RentRepo.DropdownArticle(), "ValueMember", "DisplayMember", rental.ArticleId);
            ViewBag.CustomerId = new SelectList(await uow.RentRepo.DropdownCustomer(), "ValueMember", "DisplayMember", rental.CustomerId);
            ViewBag.CategoryId = new SelectList(await uow.RentRepo.DropdownCategory(), "ValueMember", "DisplayMember");
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Rental rental = await uow.RentRepo.GetRentalById(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rental rental = await uow.RentRepo.GetRentalById(id);
            uow.RentRepo.DeleteRental(rental);
            await uow.CommitAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

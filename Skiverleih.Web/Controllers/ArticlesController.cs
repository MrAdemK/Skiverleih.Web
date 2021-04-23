using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Skiverleih.Model.DatabaseModels;
using Skiverleih.Model.ViewModels;
using Skiverleih.Model.ViewModels.ArticleVM;
using Skiverleih.Web.Models;

namespace Skiverleih.Web.Controllers
{
    public class ArticlesController : Controller
    {
        //private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly UnitOfWork uow = new UnitOfWork();

        // GET: Articles
        public async Task<ActionResult> Index()
        {
            /*var articles = db.Articles.Include(a => a.Categorie).Include(a => a.Status);
            return View(await articles.ToListAsync());*/

            return View(await uow.ArticleRepo.GetAllArticle());
        }

        // GET: Articles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*Article article = await db.Articles.FindAsync(id);*/
            ArticleDetailsVM article = await uow.ArticleRepo.GetAllArticleById(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public async Task<ActionResult> Create()
        {
            /*ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");*/
            ViewBag.CategoryId = new SelectList(await uow.ArticleRepo.CategorySelect(), "ValueMember", "DisplayMember");
            /*ViewBag.StatusId = new SelectList(db.Status, "StatusId", "StatusId");*/
            ViewBag.StatusId = new SelectList(await uow.ArticleRepo.StatusSelect(), "ValueMember", "DisplayMember");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ArticleId,ArticleName,RentPriceADay,RentCount,CategoryId,StatusId")] Article article)
        {
            if (ModelState.IsValid)
            {
                /*db.Articles.Add(article);*/
                uow.ArticleRepo.InsertArticle(article);
                /*await db.SaveChangesAsync();*/
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }

            /*ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", article.CategoryId);*/
            ViewBag.CategoryId = new SelectList(await uow.ArticleRepo.CategorySelect(), "ValueMember", "DisplayMember");
            /*ViewBag.StatusId = new SelectList(db.Status, "StatusId", "StatusId", article.StatusId);*/
            ViewBag.StatusId = new SelectList(await uow.ArticleRepo.StatusSelect(), "ValueMember", "DisplayMember");
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Article article = await db.Articles.FindAsync(id);
            Article article = await uow.ArticleRepo.GetArticleById(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(await uow.ArticleRepo.CategorySelect(), "ValueMember", "DisplayMember");
            ViewBag.StatusId = new SelectList(await uow.ArticleRepo.StatusSelect(), "ValueMember", "DisplayMember");
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ArticleId,ArticleName,RentPriceADay,RentCount,CategoryId,StatusId")] Article article)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(article).State = EntityState.Modified;
                uow.ArticleRepo.UpdateArticle(article);
                await uow.CommitAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(await uow.ArticleRepo.CategorySelect(), "ValueMember", "DisplayMember");
            ViewBag.StatusId = new SelectList(await uow.ArticleRepo.StatusSelect(), "ValueMember", "DisplayMember");
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = await uow.ArticleRepo.GetArticleById(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Article article = await uow.ArticleRepo.GetArticleById(id);
            uow.ArticleRepo.DeleteArticle(article);
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

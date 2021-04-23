using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Skiverleih.Model.DatabaseModels;
using Skiverleih.Model.ViewModels.ArticleVM;
using Skiverleih.Model.ViewModels;
using Skiverleih.Web.Models;
using Skiverleih.Web.Repositories.Interfaces;
using System.Collections;

namespace Skiverleih.Web.Repositories
{
    public class ArticleRepo : IArticleRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public ArticleRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ArticleIndexVM>> GetAllArticle()
        {
            var temp = await (from a in _dbContext.Articles
                    .Include(c => c.Categorie)
                    .Include(s => s.Status)
                select new ArticleIndexVM()
                {
                    CategoryName = a.Categorie.CategoryName,
                    Available = a.Status.Available,
                    ArticleId = a.ArticleId,
                    ArticleName = a.ArticleName,
                    RentPriceADay = a.RentPriceADay
                }).ToListAsync();

            return temp;
        }

        public async Task<ArticleDetailsVM> GetAllArticleById(int? id)
        {
            var temp = await (from a in _dbContext.Articles
                where a.ArticleId == id
                select new ArticleDetailsVM
                {
                    ArticleId = a.ArticleId,
                    RentCount = a.RentCount
                }).FirstOrDefaultAsync();

            return temp;
        }
        
        public async Task<Article> GetArticleById(int? id)
        {
            return await _dbContext.Articles.FindAsync(id);
        }
        
        

        public void InsertArticle(Article article)
        {
            _dbContext.Articles.Attach(article);
            _dbContext.Entry(article).State = EntityState.Added;
        }

        public void UpdateArticle(Article article)
        {
            _dbContext.Articles.Attach(article);
            _dbContext.Entry(article).State = EntityState.Modified;
        }


        public void DeleteArticle(Article article)
        {
            _dbContext.Articles.Attach(article);
            _dbContext.Entry(article).State = EntityState.Deleted;
        }

        public async Task<IEnumerable> CategorySelect()
        {
            var cs = await (from c in _dbContext.Categories
                orderby c.CategoryName
                select new SelectListItemVm
                {
                    ValueMember = c.CategoryId.ToString(),
                    DisplayMember = c.CategoryName.ToString()
                }).ToListAsync();

            return cs;
        }

        public async Task<IEnumerable> StatusSelect()
        {
            var ss = await (from s in _dbContext.Status
                orderby s.StatusId
                select new SelectListItemVm
                {
                    ValueMember = s.StatusId.ToString(),
                    DisplayMember = s.Available.ToString()
                }).ToListAsync();

            return ss;
        }
    }
}
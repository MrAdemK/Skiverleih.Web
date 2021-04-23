using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skiverleih.Model.DatabaseModels;
using Skiverleih.Model.ViewModels.ArticleVM;

namespace Skiverleih.Web.Repositories.Interfaces
{
    public interface IArticleRepo
    {
        Task<List<ArticleIndexVM>> GetAllArticle();
        Task<ArticleDetailsVM> GetAllArticleById(int? id);
        
        Task<Article> GetArticleById(int? id);

        void InsertArticle(Article article);
        void UpdateArticle(Article article);
        void DeleteArticle(Article article);

        Task<IEnumerable> CategorySelect();
        Task<IEnumerable> StatusSelect();
    }
}

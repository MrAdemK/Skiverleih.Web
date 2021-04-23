using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skiverleih.Web.Models;
using Skiverleih.Web.Repositories;
using Skiverleih.Web.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Skiverleih.Web
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

        private IArticleRepo _articleRepo;
        private ICustomerRepo _customerRepo;
        private IRentRepo _rentRepo;

        public IArticleRepo ArticleRepo
        {
            get
            {
                if (_articleRepo == null) _articleRepo = new ArticleRepo(_dbContext);
                return _articleRepo;
            }
        }

        public ICustomerRepo CustomerRepo
        {
            get
            {
                if (_customerRepo == null) _customerRepo = new CustomerRepo(_dbContext);
                return _customerRepo;
            }
        }

        public IRentRepo RentRepo
        {
            get
            {
                if (_rentRepo == null) _rentRepo = new RentRepo(_dbContext);
                return _rentRepo;
            }
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
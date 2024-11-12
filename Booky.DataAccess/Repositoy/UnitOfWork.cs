using Booky.DataAccess.Data;
using Booky.DataAccess.Repositoy.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booky.DataAccess.Repositoy
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _DbContext;
        public ICategoryRepositoy Category { get; private set; }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
            Category = new CategoryRepository(_DbContext);
        }


        public int Save()
        {
            return _DbContext.SaveChanges();
        }
    }
}

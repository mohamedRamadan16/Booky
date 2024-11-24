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

        public IProductRepository Product { get; private set; }

        public ICompanyRepositoy Company { get; private set; }

        public IShoppingCartRepositoy ShoppingCart { get; private set; }
        public IApplicationUserRepositoy ApplicationUser { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
            Category = new CategoryRepository(_DbContext);
            Product = new ProductRepository(_DbContext);
            Company = new CompanyRepository(_DbContext);
            ShoppingCart = new ShoppingCartRepository(_DbContext);
            ApplicationUser = new ApplicationUserRepository(_DbContext);
        }


        public int Save()
        {
            return _DbContext.SaveChanges();
        }
    }
}

using Booky.DataAccess.Data;
using Booky.DataAccess.Repositoy.IRepository;
using Booky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booky.DataAccess.Repositoy
{
    public class CategoryRepository : Repository<Category>, ICategoryRepositoy
    {
        private readonly ApplicationDbContext _DbContext;
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }
        public int Save()
        {
            return _DbContext.SaveChanges();
        }

        public void Update(Category obj)
        {
            _DbContext.Categories.Update(obj);
        }
    }
}

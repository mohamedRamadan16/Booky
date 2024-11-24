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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepositoy
    {
        private readonly ApplicationDbContext _DbContext;
        public ShoppingCartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }

        public void Update(ShoppingCart obj)
        {
            _DbContext.ShoppingCarts.Update(obj);
        }
    }
}

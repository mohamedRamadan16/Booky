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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepositoy
    {
        private readonly ApplicationDbContext _DbContext;
        public OrderHeaderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }

        public void Update(OrderHeader obj)
        {
            _DbContext.OrderHeaders.Update(obj);
        }
    }
}

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

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var OrderFromDb = _DbContext.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if(OrderFromDb != null)
            {
                OrderFromDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    OrderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var OrderFromDb = _DbContext.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if (!string.IsNullOrEmpty(sessionId))
            {
                OrderFromDb.SessionId = sessionId;
            }

            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                OrderFromDb.PaymentIntentId = paymentIntentId;
                OrderFromDb.PaymentDate = DateTime.Now;
            }
        }
    }
}

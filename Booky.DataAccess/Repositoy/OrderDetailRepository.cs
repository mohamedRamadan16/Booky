﻿using Booky.DataAccess.Data;
using Booky.DataAccess.Repositoy.IRepository;
using Booky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booky.DataAccess.Repositoy
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepositoy
    {
        private readonly ApplicationDbContext _DbContext;
        public OrderDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }

        public void Update(OrderDetail obj)
        {
            _DbContext.OrderDetails.Update(obj);
        }
    }
}

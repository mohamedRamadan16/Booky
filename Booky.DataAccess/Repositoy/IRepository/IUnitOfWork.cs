﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booky.DataAccess.Repositoy.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepositoy Category { get; }
        IProductRepository Product { get; }
        ICompanyRepositoy Company { get; }
        IShoppingCartRepositoy ShoppingCart { get; }
        IApplicationUserRepositoy ApplicationUser { get; }
        IOrderHeaderRepositoy OrderHeader { get; }
        IOrderDetailRepositoy OrderDetail { get; }
        int Save();
    }
}

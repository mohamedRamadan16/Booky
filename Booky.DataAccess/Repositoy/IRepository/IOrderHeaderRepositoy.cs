using Booky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booky.DataAccess.Repositoy.IRepository
{
    public interface IOrderHeaderRepositoy : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
    }
}

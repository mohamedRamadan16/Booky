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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepositoy
    {
        private readonly ApplicationDbContext _DbContext;
        public ApplicationUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }

    }
}

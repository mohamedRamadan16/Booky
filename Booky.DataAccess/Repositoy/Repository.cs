using Booky.DataAccess.Data;
using Booky.DataAccess.Repositoy.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booky.DataAccess.Repositoy
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _DbContext;
        internal DbSet<T> _dbSet; // _dbSet == _DbContext.Categories;
        public Repository(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
            _dbSet = _DbContext.Set<T>();
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query;
            query = _dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query;
            query = _dbSet;
            return query.ToList();
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            _dbSet.RemoveRange(items);
        }
    }
}

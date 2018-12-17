using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.DataAccess
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<T> AddAsync(T entity)
        {
            return (await _context.AddAsync(entity)).Entity;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includeProperties)
        {
            return await FindMany(predicate, includeProperties).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindManyWithPaginationAsync(Expression<Func<T, bool>> predicate, int pageSize, int page, Expression<Func<T, object>>[] includeProperties)
        {
            return await FindMany(predicate, includeProperties).Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
        }

        public async Task<T> FindOneAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includeProperties)
        {
            return await FindMany(predicate, includeProperties).SingleOrDefaultAsync();
        }

        public T Update(T entity)
        {
            return _context.Update(entity).Entity;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).CountAsync();
        }
        
        private IQueryable<T> FindMany(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }
            return query.Where(predicate);
        }
    }
}

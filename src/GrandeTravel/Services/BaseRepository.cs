
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.Services
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private GrandeTravelDbContext _context;
        private DbSet<T> _dbSet;

        public BaseRepository()
        {
            _context = new GrandeTravelDbContext();
            _dbSet = _context.Set<T>();
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}

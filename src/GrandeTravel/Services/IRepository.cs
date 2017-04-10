using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GrandeTravel.Services
{
    public interface IRepository<T>
    {
        void Create(T entity);

        IEnumerable<T> GetAll();

        T GetSingle(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Query(Expression<Func<T, bool>> predicate);

        void Update(T entity);

        void Delete(T entity);


    }
}

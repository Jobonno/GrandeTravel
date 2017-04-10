using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeTravel.Services
{
    public interface IRepository<T>
    {
        void Create(T entity);

        IEnumerable<T> GetAll();
    }
}

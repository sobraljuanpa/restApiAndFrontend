using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TwoDrive.DataAccess
{
    public interface IDataRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        void Add(T entity);
        void Update(T dbEntity, T newEntity);
        void Delete(T entity);
    }
}

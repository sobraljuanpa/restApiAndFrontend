using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TwoDrive.DataAccess
{
    public interface IDataRepository<T>
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        T Get(long id);
        void Add(T entity);
        void Update(T dbEntity, T newEntity);
        void Delete(T entity);
    }
}

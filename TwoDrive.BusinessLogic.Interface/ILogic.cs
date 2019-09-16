using System;
using System.Collections.Generic;
using System.Text;

namespace TwoDrive.BusinessLogic.Interface
{
    public interface ILogic<T>
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        void Add(T entity);
        void Update(T Entity, T newEntity);
        void Delete(T Entity);
    }
}

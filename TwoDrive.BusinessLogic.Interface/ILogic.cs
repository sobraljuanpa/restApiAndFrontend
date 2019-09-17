using System;
using System.Collections.Generic;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic.Interface
{
    public interface ILogic<T>
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        void Add(T entity);
        void Update(T Entity, T newEntity);
        void Delete(T Entity);
        void Move(T Entity, Folder folder);
        void AddReader(T Entity, User user);
        void RemoveReader(T Entity, User user);
    }
}

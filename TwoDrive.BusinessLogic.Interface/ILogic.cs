using System;
using System.Collections.Generic;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic.Interface
{
    public interface ILogic<T>
    {
        User Authenticate(string username, string password);
        long GetUserId(string username);
        long GetUserRootFolderId(string username);
        IEnumerable<T> GetAll();
        T Get(long id);
        T Add(T entity);
        void Update(T Entity, T newEntity);
        void Delete(T Entity);
    }
}

﻿using System.Collections.Generic;
using TwoDrive.Domain;

namespace TwoDrive.DataAccess.Interface
{
    public interface IDataRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        void Add(T entity);
        void Update(T dbEntity, T newEntity);
        void Delete(T entity);
        User Authenticate(string username, string password);
    }
}
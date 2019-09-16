using System;
using System.Collections.Generic;
using System.Text;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.Test.Mocks
{
    public class RepositoryUserMock : IDataRepository<User>
    {
        List<User> users = new List<User>();
        public void Add(User entity)
        {
            users.Add(entity);
        }

        public void Delete(User entity)
        {
            users.Remove(entity);
        }

        public User Get(long id)
        {
            return users.Find(u => u.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return users;
        }

        public void Update(User dbEntity, User newEntity)
        {
            users.Remove(dbEntity);
            users.Add(newEntity);
        }
    }
}

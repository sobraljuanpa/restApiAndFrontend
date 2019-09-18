using System;
using System.Collections.Generic;
using System.Text;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class UserLogic : ILogic<User>
    {
        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void AddReader(User Entity, User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(User Entity)
        {
            throw new NotImplementedException();
        }

        public User Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Move(User Entity, Folder folder)
        {
            throw new NotImplementedException();
        }

        public void RemoveReader(User Entity, User user)
        {
            throw new NotImplementedException();
        }

        public void Update(User Entity, User newEntity)
        {
            throw new NotImplementedException();
        }
    }
}

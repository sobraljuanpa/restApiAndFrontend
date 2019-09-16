using System;
using System.Collections.Generic;
using System.Text;
using TwoDrive.Domain;

namespace TwoDrive.Test.Mocks
{
    public class RepositoryUserMock
    {
        public bool Add(User entity)
        {
            return true;
        }

        public bool Delete(User entity)
        {
            return true;
        }

        public bool Get(long id)
        {
            return true;
        }

        public bool GetAll()
        {
            return true;
        }

        public bool Update(User dbEntity, User newEntity)
        {
            return true;
        }
    }
}

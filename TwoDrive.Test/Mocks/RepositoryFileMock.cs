using System;
using System.Collections.Generic;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.Test.Mocks
{
    public class RepositoryFileMock
    {
        public bool Add(File entity)
        {
            return true;
        }

        public bool Delete(File entity)
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

        public bool Update(File dbEntity, File newEntity)
        {
            return true;
        }
    }
}

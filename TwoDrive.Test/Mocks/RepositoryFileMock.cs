using System;
using System.Collections.Generic;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.Test.Mocks
{
    public class RepositoryFileMock : IDataRepository<File>
    {
        List<File> files = new List<File>();
        public void Add(File entity)
        {
            files.Add(entity);
        }

        public void Delete(File entity)
        {
            files.Remove(entity);
        }

        public File Get(long id)
        {
            return files.Find(f => f.Id == id);
        }

        public IEnumerable<File> GetAll()
        {
            return files;
        }

        public void Update(File dbEntity, File newEntity)
        {
            files.Remove(dbEntity);
            files.Add(newEntity);
        }
    }
}

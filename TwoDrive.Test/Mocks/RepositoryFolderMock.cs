using System;
using System.Collections.Generic;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.Test.Mocks
{
    public class RepositoryFolderMock : IDataRepository<Folder>
    {
        List<Folder> folders = new List<Folder>();
        public void Add(Folder entity)
        {
            folders.Add(entity);
        }

        public void Delete(Folder entity)
        {
            folders.Remove(entity);
        }

        public Folder Get(long id)
        {
            return folders.Find(f => f.Id == id);
        }

        public IEnumerable<Folder> GetAll()
        {
            return folders;
        }

        public void Update(Folder dbEntity, Folder newEntity)
        {
            folders.Remove(dbEntity);
            folders.Add(newEntity);
        }
    }
}

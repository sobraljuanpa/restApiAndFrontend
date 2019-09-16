using TwoDrive.Domain;

namespace TwoDrive.Test.Mocks
{
    public class RepositoryFolderMock
    {
        public bool Add(Folder entity)
        {
            return true;
        }

        public bool Delete(Folder entity)
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

        public bool Update(Folder dbEntity, Folder newEntity)
        {
            return true;
        }
    }
}

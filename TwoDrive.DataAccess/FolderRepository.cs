using System.Linq;
using System.Collections.Generic;
using TwoDrive.Domain;

namespace TwoDrive.DataAccess
{
    public class FolderRepository : IDataRepository<Folder>
    {
        readonly TwoDriveContext _context;

        public FolderRepository(TwoDriveContext context)
        {
            _context = context;
        }

        public IEnumerable<Folder> GetAll()
        {
            return _context.Folders.ToList();
        }

        public Folder Get(long id)
        {
            return _context.Folders.FirstOrDefault(
                f => f.Id == id);
        }

        public void Add(Folder folder)
        {
            _context.Folders.Add(folder);
        }

        public void Update(Folder oldFolder, Folder newFolder)
        {
            oldFolder.Files = newFolder.Files;
            oldFolder.Folders = newFolder.Folders;
            oldFolder.Name = newFolder.Name;
            oldFolder.OwnerId = newFolder.OwnerId;
            oldFolder.Parent = newFolder.Parent;
            oldFolder.Readers = newFolder.Readers;

            _context.SaveChanges();
        }

        public void Delete(Folder folder)
        {
            _context.Folders.Remove(folder);
            _context.SaveChanges();
        }
    }
}

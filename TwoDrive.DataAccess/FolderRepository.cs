using System.Linq;
using System.Collections.Generic;
using TwoDrive.Domain;
using TwoDrive.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace TwoDrive.DataAccess
{
    public class FolderRepository : IDataRepository<Folder>
    {
        readonly TwoDriveContext _context;

        public FolderRepository(TwoDriveContext context)
        {
            _context = context;
        }
        
        public User Authenticate(string a, string b)
        {
            return null;
        }

        public IEnumerable<Folder> GetAll()
        {
            return _context.Folders
                .Include(f => f.Readers)
                .Include(f => f.Parent)
                .Include(f => f.Files)
                .Include(f => f.Folders)
                .ToList();
        }

        public Folder Get(long id)
        {
            var folder =  _context.Folders
                        .Include(f => f.Readers)
                        .Include(f => f.Parent)
                        .Include(f => f.Files)
                        .Include(f => f.Folders)
                        .FirstOrDefault(
                        f => f.Id == id);
            if(folder.Folders != null)
                foreach(var fold in folder.Folders)
                {
                    this.Get(fold.Id);
                }
            return folder;      
        }

        public void Add(Folder folder)
        {
            _context.Folders.Add(folder);
            _context.SaveChanges();
        }

        public void Update(Folder oldFolder, Folder newFolder)
        {
            oldFolder.Files = newFolder.Files;
            oldFolder.Folders = newFolder.Folders;
            oldFolder.Name = newFolder.Name;
            oldFolder.OwnerId = newFolder.OwnerId;
            oldFolder.Parent = newFolder.Parent;
            oldFolder.Readers.Concat(newFolder.Readers);
            _context.SaveChanges();
        }

        public void Delete(Folder folder)
        {
            _context.Folders.Remove(folder);
            _context.SaveChanges();
        }
    }
}

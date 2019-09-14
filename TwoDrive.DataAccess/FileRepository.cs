using System;
using System.Collections.Generic;
using System.Linq;
using TwoDrive.Domain;

namespace TwoDrive.DataAccess
{
    public class FileRepository : IDataRepository<File>
    {
        readonly TwoDriveContext _context;

        public IEnumerable<File> GetAll()
        {
            return _context.Files.ToList();
        }

        public File Get(long id)
        {
            return _context.Files.FirstOrDefault(
                f => f.Id == id);
        }

        public void Add(File file)
        {
            _context.Files.Add(file);
            _context.SaveChanges();
        }

        public void Update(File oldFile, File newFile)
        {
            newFile.Content = oldFile.Content;
            newFile.CreationDate = oldFile.CreationDate;
            newFile.LastModifiedDate = DateTime.Now;
            newFile.Name = oldFile.Name;
            newFile.OwnerId = oldFile.OwnerId;
            newFile.Parent = oldFile.Parent;
            newFile.Readers = oldFile.Readers;

            _context.SaveChanges();
        }

        public void Delete(File file)
        {
            _context.Files.Remove(file);
            _context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.DataAccess
{
    public class FileRepository : IDataRepository<File>
    {
        readonly TwoDriveContext _context;

        public FileRepository(TwoDriveContext context)
        {
            _context = context;
        }

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
            oldFile.Content = newFile.Content;
            oldFile.CreationDate = newFile.CreationDate;
            oldFile.LastModifiedDate = DateTime.Now;
            oldFile.Name = newFile.Name;
            oldFile.OwnerId = newFile.OwnerId;
            oldFile.Parent = newFile.Parent;
            oldFile.Readers = newFile.Readers;

            _context.SaveChanges();
        }

        public void Delete(File file)
        {
            _context.Files.Remove(file);
            _context.SaveChanges();
        }
    }
}

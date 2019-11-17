using Microsoft.EntityFrameworkCore;
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

        public User Authenticate(string a, string b)
        {
            return null;
        }

        public IEnumerable<File> GetAll()
        {
            return _context.Files.Include(f => f.Readers).Include(f => f.Parent).ToList();
        }

        public File Get(long id)
        {
            return _context.Files.Include(f => f.Readers).Include(f => f.Parent).FirstOrDefault(
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
            if (newFile.Name != null) oldFile.Name = newFile.Name;
            if (newFile.OwnerId != 0) oldFile.OwnerId = newFile.OwnerId;
            if (newFile.Parent != null) oldFile.Parent = newFile.Parent;
            if (newFile.Readers != null) oldFile.Readers.Concat(newFile.Readers);
            _context.SaveChanges();
        }

        public void Delete(File file)
        {
            _context.Files.Remove(file);
            _context.SaveChanges();
        }
    }
}

using TwoDrive.Domain;
using TwoDrive.DataAccess.Interface;
using System.Linq;
using System.Collections.Generic;
using System;

namespace TwoDrive.DataAccess
{
    public class LogRepository : IDataRepository<LogItem>
    {
        readonly TwoDriveContext _context;

        public LogRepository(TwoDriveContext context)
        {
            _context = context;
        }

        public User Authenticate(string a, string b)
        {
            return null;
        }

        public IEnumerable<LogItem> GetAll()
        {
            return _context.Logs.ToList();
        }

        public LogItem Get(long id)
        {
            return null;
        }

        public void Add(LogItem log)
        {
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

        public void Update(LogItem a, LogItem b)
        {

        }

        public void Delete(LogItem log)
        {
           
        }
    }
}

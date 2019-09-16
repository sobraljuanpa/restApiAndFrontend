using System;
using System.Collections.Generic;
using System.Text;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class LogicFile : ILogic<File>
    {
        public void Add(File entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(File Entity)
        {
            throw new NotImplementedException();
        }

        public File Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<File> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(File Entity, File newEntity)
        {
            throw new NotImplementedException();
        }
    }
}

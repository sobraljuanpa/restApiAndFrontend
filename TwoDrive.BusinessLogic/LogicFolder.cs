﻿using System;
using System.Collections.Generic;
using System.Text;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class LogicFolder : ILogic<Folder>, ILogic2<Folder>
    {
        public void Add(Folder entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Folder Entity)
        {
            throw new NotImplementedException();
        }

        public Folder Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Folder> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Folder Entity, Folder newEntity)
        {
            throw new NotImplementedException();
        }

        public void Move(Folder Entity, FolderElement folder)
        {
            throw new NotImplementedException();
        }

        public void AddReader(Folder Entity, User user)
        {
            throw new NotImplementedException();
        }

        public void RemoveReader(Folder Entity, User user)
        {
            throw new NotImplementedException();
        }
    }
}

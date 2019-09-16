using System;
using System.Collections.Generic;
using System.Text;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic.Interface
{
    public interface ILogic2<T>
    {
        void Move(T Entity, FolderElement folder);
        void AddReader(T Entity, User user);
        void RemoveReader(T Entity, User user);
    }
}

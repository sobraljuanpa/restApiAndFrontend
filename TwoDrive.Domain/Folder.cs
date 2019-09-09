using System;
using System.Collections.Generic;
using System.Text;

namespace TwoDrive.Domain
{
    public class Folder
    {
        public Guid Id { get; set; }
        public List<Guid> FilesList{ get; set; }
        public List<Guid> FoldersList { get; set; }
        public List<Guid> ReadersList { get; set; }
        public List<Guid> OwnersList { get; set; }
        public Guid ParentFolder { get; set; }

        public void AddToList(List<Guid> list,Guid Id)
        {
            list.Add(Id);
        }

        public void RemoveFromList(List<Guid> list, Guid Id)
        {
            list.Remove(Id);
        }
    }
}

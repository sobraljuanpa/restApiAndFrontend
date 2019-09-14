using System;
using System.Collections.Generic;
using System.Text;

namespace TwoDrive.Domain
{
    public class File : FolderElement
    {

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public User Owner { get; set; }

        public List<User> Readers { get; set; }

        public void AddReader(User user)
        {
            Readers.Add(user);
        }

        public void RemoveReader(User user)
        {
            Readers.Remove(user);
        }
    }
}

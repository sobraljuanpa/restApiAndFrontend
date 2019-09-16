using System;
using System.Collections.Generic;

namespace TwoDrive.Domain
{
    public class File : FolderElement
    {
        public File()
        {
            this.Content = "";
            this.CreationDate = DateTime.Now;
            this.LastModifiedDate = DateTime.Now;
            this.CreationDate = DateTime.Now;
            this.OwnerId.Add(0);
            this.Name = "";
            this.Parent = null;
            this.Readers = null;
        }

        public File(User user, string name, Folder parent, List<User> readers, string Content)
        {
            this.Content = Content;
            this.CreationDate = DateTime.Now;
            this.LastModifiedDate = DateTime.Now;
            this.CreationDate = DateTime.Now;
            this.OwnerId.Add(user.Id);
            this.Name = name;
            this.Parent = parent;
            this.Readers = readers;
        }
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TwoDrive.Domain
{
    public class Folder : FolderElement
    {

        public User Owner { get; set; }

        public List<File> Files { get; set; }

        public List<Folder> Folders { get; set; }

        public List<User> Readers { get; set; }

        public void AddFile(File file)
        {
            Files.Add(file);
        }

        public void RemoveFile(File file)
        {
            Files.Remove(file);
        }

        public void AddFolder(Folder folder)
        {
            Folders.Add(folder);
        }

        public void RemoveFolder(Folder folder)
        {
            Folders.Remove(folder);
        }

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

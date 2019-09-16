using System.Collections.Generic;

namespace TwoDrive.Domain
{
    public class Folder : FolderElement
    {
        public List<File> Files { get; set; }

        public List<Folder> Folders { get; set; }

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

    }
}

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TwoDrive.Domain
{
    [Serializable()]
    public class Folder : FolderElement
    {
        [XmlArrayItem("Files")]
        public List<File> Files { get; set; }
        [XmlArrayItem("Folders")]
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

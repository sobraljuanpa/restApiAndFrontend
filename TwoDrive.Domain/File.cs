using System;
using System.Xml.Serialization;

namespace TwoDrive.Domain
{
    [Serializable()]
    public class File : FolderElement
    {
        [XmlElement("Content")]
        public string Content { get; set; }
        [XmlElement("CreationDate")]
        public DateTime CreationDate { get; set; }
        [XmlElement("LastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }
    }
}

using System;
using System.Xml.Serialization;

namespace TwoDrive.Domain
{
    [Serializable()]
    public class File : FolderElement
    {
        [XmlElementAttribute("Content")]
        public string Content { get; set; }
        [XmlElementAttribute("CreationDate")]
        public DateTime CreationDate { get; set; }
        [XmlElementAttribute("LastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }
    }
}

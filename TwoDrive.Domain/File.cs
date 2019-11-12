using System;
using System.Xml.Serialization;

namespace TwoDrive.Domain
{
    [Serializable()]
    public class File : FolderElement
    {
        [XmlElementAttribute("content")]
        public string Content { get; set; }
        [XmlElementAttribute("creationDate")]
        public DateTime CreationDate { get; set; }
        [XmlElementAttribute("lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }
    }
}

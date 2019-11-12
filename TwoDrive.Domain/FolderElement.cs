using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace TwoDrive.Domain
{
    [Serializable()]
    public abstract class FolderElement 
    {
        [XmlElement("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [XmlElement("OwnerId")]
        [ForeignKey("User")]
        public long OwnerId { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Parent")]
        public Folder Parent { get; set; }
        [XmlElement("Readers")]
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

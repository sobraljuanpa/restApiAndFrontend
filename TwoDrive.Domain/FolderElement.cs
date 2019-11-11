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
        [XmlElementAttribute("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [XmlElementAttribute("OwnerId")]
        [ForeignKey("User")]
        public long OwnerId { get; set; }
        [XmlElementAttribute("Name")]
        public string Name { get; set; }
        [XmlElementAttribute("Parent")]
        public Folder Parent { get; set; }
        [XmlElementAttribute("Readers")]
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

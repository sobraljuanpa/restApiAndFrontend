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
        [XmlElementAttribute("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [XmlElementAttribute("ownerId")]
        [ForeignKey("User")]
        public long OwnerId { get; set; }
        [XmlElementAttribute("name")]
        public string Name { get; set; }
        [XmlElementAttribute("parent")]
        public Folder Parent { get; set; }
        [XmlElementAttribute("readers")]
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

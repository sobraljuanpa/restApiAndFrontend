using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwoDrive.Domain
{
    public abstract class FolderElement
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public User Owner { get; set; }

        public string Name { get; set; }

        public FolderElement Parent { get; set; }

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

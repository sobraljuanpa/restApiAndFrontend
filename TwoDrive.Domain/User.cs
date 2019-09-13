using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwoDrive.Domain
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool Administrator { get; set; }

        public List<long> FriendList { get; set; }

        public void AddToFriendList(long id)
        {
            FriendList.Add(id);
        }

        public void RemoveFromFriendList(long id)
        {
            FriendList.Remove(id);
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwoDrive.Domain
{
    public class User
    {
        public User(string firstName, string lastName, string username, string password, string email, bool administrator, List<User> friendList, Folder rootFolder)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Administrator = administrator;
            this.FriendList = friendList;
            this.RootFolder = rootFolder;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool Administrator { get; set; }

        public List<User> FriendList { get; set; }

        public Folder RootFolder { get; set; }

        public void AddToFriendList(User user)
        {
            FriendList.Add(user);
        }

        public void RemoveFromFriendList(User user)
        {
            FriendList.Remove(user);
        }
    }
}
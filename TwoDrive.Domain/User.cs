using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace TwoDrive.Domain
{
    [Serializable()]
    public class User
    {
        [XmlElementAttribute("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [XmlElementAttribute("firstName")]
        public string FirstName { get; set; }
        [XmlElementAttribute("lastName")]
        public string LastName { get; set; }
        [XmlElementAttribute("username")]
        public string Username { get; set; }
        [XmlElementAttribute("password")]
        public string Password { get; set; }
        [XmlElementAttribute("email")]
        public string Email { get; set; }
        [XmlElementAttribute("role")]
        public string Role { get; set; }
        [XmlElementAttribute("token")]
        public string Token { get; set; }
        [XmlElementAttribute("friendList")]
        public List<User> FriendList { get; set; }
        [XmlElementAttribute("rootFolder")]
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
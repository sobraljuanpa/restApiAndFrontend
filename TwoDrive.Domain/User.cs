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
        [XmlElement("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [XmlElement("FirstName")]
        public string FirstName { get; set; }
        [XmlElement("LastName")]
        public string LastName { get; set; }
        [XmlElement("Username")]
        public string Username { get; set; }
        [XmlElement("Password")]
        public string Password { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }
        [XmlElement("Role")]
        public string Role { get; set; }
        [XmlElement("Token")]
        public string Token { get; set; }
        [XmlElement("FriendList")]
        public List<User> FriendList { get; set; }
        [XmlElement("RootFolder")]
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
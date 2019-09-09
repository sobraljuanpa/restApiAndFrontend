using System;
using System.Collections.Generic;

namespace TwoDrive.Domain
{
    public class User
    {

		public Guid Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

		public bool Administrator { get; set; }

		public List<Guid> FriendList { get; set; }

		public void AddToFriendList(Guid Id)
		{
			FriendList.Add(Id);
		}

		public void RemoveFromFriendList(Guid Id)
		{
			FriendList.Remove(Id);
		}
	}
}

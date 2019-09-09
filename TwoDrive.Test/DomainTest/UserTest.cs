using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoDrive.Domain;

namespace TwoDrive.Test.DomainTest
{
	[TestClass]
	public class UserTest
	{

		User user;

		[TestInitialize]
		public void SetUp()
		{
			user = new User();
			user.FriendList = new List<Guid>();
		}

		[TestMethod]
		public void SetIdTest()
		{
			Guid aux = new Guid("AC8DB7A8-6EBE-4A07-BEB9-B7AC59485B90");
			user.Id = aux;

			Assert.AreEqual(aux, user.Id);
		}

		[TestMethod]
		public void SetFirstNameTest()
		{
			user.FirstName = "Juan";

			Assert.AreEqual("Juan", user.FirstName);
		}

		[TestMethod]
		public void SetLastNameTest()
		{
			user.LastName = "Perez";

			Assert.AreEqual("Perez", user.LastName);
		}

		[TestMethod]
		public void SetUsernameTest()
		{
			user.Username = "juan.perez";

			Assert.AreEqual("juan.perez", user.Username);
		}

		[TestMethod]
		public void SetPasswordTest()
		{
			user.Password = "firulais123";

			Assert.AreEqual("firulais123", user.Password);
		}

		[TestMethod]
		public void SetEmailTest()
		{
			user.Email = "juan.perez@gmail.com";

			Assert.AreEqual("juan.perez@gmail.com", user.Email);
		}

		[TestMethod]
		public void SetAdministratorTest()
		{
			user.Administrator = true;

			Assert.AreEqual(true, user.Administrator);
		}

		[TestMethod]
		public void AddToFriendListTest()
		{
			Guid aux = new Guid("AC8DB7A8-6EBE-4A07-BEB9-B7AC59485B90");
			user.AddToFriendList(aux);

			Assert.AreEqual(1, user.FriendList.Count);
		}

		[TestMethod]
		public void RemoveFromFriendListTest()
		{
			Guid aux = new Guid("AC8DB7A8-6EBE-4A07-BEB9-B7AC59485B90");
			user.AddToFriendList(aux);
			user.RemoveFromFriendList(aux);

			Assert.AreEqual(0, user.FriendList.Count);
		}
	}
}

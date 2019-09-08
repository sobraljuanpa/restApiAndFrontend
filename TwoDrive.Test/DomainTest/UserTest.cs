using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoDrive.Domain.User;

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
		}

		[TestMethod]
		public void SetGuidTest()
		{
			user.Guid = 1;

			Assert.AreEqual(1, user.Guid);
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
			user.Name = "Perez";

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
			user.Username = "juan.perez@gmail.com";

			Assert.AreEqual("juan.perez@gmail.com", user.Email);
		}

		[TestMethod]
		public void SetAdministratorTest()
		{
			user.Administrator = true;

			Assert.AreEqual(true, user.Administrator);
		}

	}
}

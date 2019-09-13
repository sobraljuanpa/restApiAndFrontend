using System.Linq;
using System.Collections.Generic;

using TwoDrive.DataAccess;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class UserRepository : IDataRepository<User>
    {
        readonly TwoDriveContext _context;

        public UserRepository(TwoDriveContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User Get(long id)
        {
            return _context.Users.FirstOrDefault(
                u => u.Id == id);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User oldUser, User newUser)
        {
            newUser.Administrator = oldUser.Administrator;
            newUser.FirstName = oldUser.FirstName;
            newUser.LastName = oldUser.LastName;
            newUser.Email = oldUser.Email;
            newUser.Username = oldUser.Username;
            newUser.Password = oldUser.Password;
            newUser.FriendList = oldUser.FriendList;

            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}

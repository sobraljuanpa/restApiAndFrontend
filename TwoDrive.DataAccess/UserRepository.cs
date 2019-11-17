using System.Linq;
using System.Collections.Generic;
using TwoDrive.Domain;
using TwoDrive.DataAccess.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Microsoft.EntityFrameworkCore;

namespace TwoDrive.DataAccess
{
    public class UserRepository : IDataRepository<User>
    {
        readonly TwoDriveContext _context;

        public UserRepository(TwoDriveContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(
                x => x.Username == username && x.Password == password);

            //Si no encuentro user retorno null
            if(user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Ahora este es nuestro secreto.");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            //saco la pass antes de retornar, esto habria que hacerlo siempre
            user.Password = null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(u => u.FriendList).Include(u => u.RootFolder).ToList();
        }

        public User Get(long id)
        {
            return _context.Users.Include(u => u.FriendList).Include(u => u.RootFolder).FirstOrDefault(
                u => u.Id == id);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User oldUser, User newUser)
        {
            if(newUser.Role != null) oldUser.Role = newUser.Role;
            if (newUser.FirstName != null) oldUser.FirstName = newUser.FirstName;
            if (newUser.LastName != null) oldUser.LastName = newUser.LastName;
            if (newUser.Email != null) oldUser.Email = newUser.Email;
            if (newUser.Username != null) oldUser.Username = newUser.Username;
            if (newUser.Password != null) oldUser.Password = newUser.Password;
            if (newUser.FriendList != null) oldUser.FriendList = newUser.FriendList;
            if (newUser.RootFolder != null) oldUser.RootFolder = newUser.RootFolder;

            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}

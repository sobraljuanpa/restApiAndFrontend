using System;
using System.Collections.Generic;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class UserLogic : ILogic<User>
    {
        IDataRepository<User> _repository;
        public UserLogic(IDataRepository<User> repository)
        {
            _repository = repository;
        }
        public void Add(User entity)
        {
            ValidateFormat(entity);
            NotRepeated(entity);
            _repository.Add(entity);
        }

        public void Delete(User Entity)
        {
            UserExist(Entity.Id);
            _repository.Delete(Entity);
        }

        public User Get(long id)
        {
            UserExist(id);
            return _repository.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(User Entity, User newEntity)
        {
            ValidateFormat(newEntity);
            NotRepeated(newEntity);
            UserExist(Entity.Id);
            _repository.Update(Entity, newEntity);
        }

        private void NotRepeated(User entity)
        {
            if(_repository.Get(entity.Id) != null)
                throw new Exception("El usuario ya se encuentra en el sistema.");
        }

        private void ValidateFormat(User entity)
        {
            if(entity.FirstName == null)
                throw new Exception("El usuario no contiene un nombre.");
            if (entity.LastName == null)
                throw new Exception("El usuario no contiene un apellido.");
            if (entity.Username == null)
                throw new Exception("El usuario no contiene un nombre de usuario.");
            if (entity.Password == null)
                throw new Exception("El usuario no contiene una contrasena.");
            if (entity.Email == null)
                throw new Exception("El usuario no contiene un mail.");
            if (entity.FriendList == null)
                throw new Exception("El usuario no contiene una lista de amigos.");
            if (entity.RootFolder == null)
                throw new Exception("El usuario no contiene una carpeta raiz.");
        }

        private void UserExist(long id)
        {
            if (_repository.Get(id) == null)
                throw new Exception("El usuario no existe en el sistema.");
        }
    }
}

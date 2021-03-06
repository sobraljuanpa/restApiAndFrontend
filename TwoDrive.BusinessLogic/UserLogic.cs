﻿using System;
using System.Collections.Generic;
using System.Linq;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class UserLogic : ILogic<User>
    {
        private IDataRepository<User> _userRepository;
        private IDataRepository<Folder> _folderRepository;
        private IDataRepository<File> _fileRepository;

        public UserLogic(IDataRepository<User> userRepository, IDataRepository<Folder> folderRepository, IDataRepository<File> fileRespoitory)
        {
            _userRepository = userRepository;
            _folderRepository = folderRepository;
            _fileRepository = fileRespoitory;
        }

        public User Authenticate(string username, string password)
        {
            return _userRepository.Authenticate(username, password);
        }

        public long GetUserId(string username)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);

            return user.Id;
        }

        public long GetUserRootFolderId(string username)
        {
            var folder = _folderRepository.GetAll().FirstOrDefault(x => x.Name == username + "-rootFolder");

            return folder.Id;
        }

        public User Add(User user)
        {
            ValidateFormat(user);
            _userRepository.Add(user);

            Folder userRootFolder = new Folder
            {
                Files = new List<File>(),
                Folders = new List<Folder>(),
                Name = user.Username + "-rootFolder",
                Parent = null,
                Readers = new List<User>(),
                OwnerId = GetUserId(user.Username)
            };
            _folderRepository.Add(userRootFolder);

            var auxUser = _userRepository.Get(GetUserId(user.Username));
            auxUser.RootFolder = _folderRepository.Get(GetUserRootFolderId(user.Username));
            _userRepository.Update(user, auxUser);
            return auxUser;
        }

        public void Delete(User Entity)
        {
            ValidateUserInSystem(Entity.Id);
            List<File> list = (from f in _fileRepository.GetAll()
                       where f.OwnerId == Entity.Id
                       select f).ToList();
            foreach (var file in list) _fileRepository.Delete(file);
            List<Folder> listFolder = (from fold in _folderRepository.GetAll()
                               where fold.OwnerId == Entity.Id
                               select fold).ToList();
            foreach (var folder in listFolder) _folderRepository.Delete(folder);
            _userRepository.Delete(Entity);

        }

        public User Get(long id)
        {
            ValidateUserInSystem(id);
            return _userRepository.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public void Update(User Entity, User newEntity)
        {
            ValidateFormat(newEntity);
            NotRepeated(newEntity);
            ValidateUserInSystem(Entity.Id);
            Entity.Id = newEntity.Id;
            _userRepository.Update(Entity, newEntity);
        }

        public void AddFriend(User user, User userFriend)
        {
            User newUser = user;
            ValidateUserInSystem(userFriend.Id);
            newUser.AddToFriendList(userFriend);
            _userRepository.Update(user, newUser);
        }

        public void RemoveFriend(User user, User userFriend)
        {
            ValidateUserInSystem(userFriend.Id);
            user.RemoveFromFriendList(userFriend);
            _userRepository.Update(_userRepository.Get(user.Id), user);
        }

        private void NotRepeated(User entity)
        {
            if(_userRepository.Get(entity.Id) != null)
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
        }

        private void ValidateUserInSystem(long entity)
        {
            if(_userRepository.Get(entity) == null)
                throw new Exception("El usuario no existe en el sistema.");
        }
    }
}

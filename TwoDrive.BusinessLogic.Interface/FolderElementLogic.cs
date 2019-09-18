using System;
using System.Collections.Generic;
using System.Text;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic.Interface
{
    public abstract class FolderElementLogic<T> where T : FolderElement
    {
        protected IDataRepository<T> _repository;
        protected IDataRepository<User> _userRepository;

        public T Get(long id)
        {
            FolderElementExists(id);
            return _repository.Get(id);
        }
        public void Delete(T Entity)
        {
            FolderElementExists(Entity.Id);
            _repository.Delete(Entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public void AddReader(T Entity, long userId)
        {
            User user = _userRepository.Get(userId);
            UserExist(user.Id);
            Entity.AddReader(user);
        }

        public void RemoveReader(T Entity, long userId)
        {
            User user = _userRepository.Get(userId);
            UserExist(user.Id);
            Entity.RemoveReader(user);
        }

        protected void FolderElementExists(long id)
        {
            if (_repository.Get(id) == null)
                throw new Exception("El elemento no existe.");
        }

        protected void HasTheSameLocation(T entity, T newEntity)
        {
            if (!entity.Parent.Equals(newEntity.Parent))
                throw new Exception("Las ubicaciones no coinciden.");
        }

        protected void AlreadyInFolder(T entity, Folder folder)
        {
            if (entity.Parent.Equals(folder))
                throw new Exception("El archivo ya esta en esa carpeta.");
        }

        protected void ReadersExist(List<User> readers)
        {
            foreach (User reader in readers)
            {
                if (_userRepository.Get(reader.Id) == null)
                    throw new Exception("Uno de los lectores especificados no existe.");
            }
        }

        protected void OwnerExists(long owners)
        {
            if (_userRepository.Get(owners) == null)
                throw new Exception("Uno de los dueños especificado no existe.");
        }

        protected void NameIsNull(string name)
        {
            if (name == "")
                throw new Exception("El nombre del archivo está vacio.");
        }

        protected void ReadersIsNull(List<User> readers)
        {
            if (readers.Count == 0)
                throw new Exception("El archivo no tiene ningun lector.");
        }

        protected void ParentIsNull(Folder parent)
        {
            if (parent == null)
                throw new Exception("No existe una carpeta padre para el archivo.");
        }

        private void UserExist(long id)
        {
            if (_userRepository.Get(id) == null)
                throw new Exception("El usuario no existe.");
        }
    }
}

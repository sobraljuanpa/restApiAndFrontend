﻿using System;
using System.Collections.Generic;
using System.Linq;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic.Interface
{
    public abstract class FolderElementLogic<T> where T : FolderElement
    {
        protected IDataRepository<T> _repository;
        protected IDataRepository<User> _userRepository;

        public abstract T Add(T entity);
        public abstract void Update(T Entity, T newEntity);
        public abstract void Move(long EntityId, long folderId);
        public abstract void Delete(T Entity);
        public abstract List<File> FilesShared(long userId);

        public T GetByName(string nameEntity, long userId)
        {
            return (from r in _repository.GetAll()
                   where r.OwnerId == userId
                   where r.Name == nameEntity
                   select r).First();
        }

        public T Get(long id)
        {
            FolderElementExists(id);
            return _repository.Get(id);
        }

        public IEnumerable<T> GetAllUser(long userId)
        {
            List<T> list = new List<T>();
            foreach(var entity in this.GetAll())
            {
                if (entity.OwnerId == userId) list.Add(entity);
            }
            return list;
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public void AddReader(T Entity, long userId)
        {
            UserExist(userId);
            ListReadersNotNull(Entity);
            T fileUpdate = Entity;
            User user = _userRepository.Get(userId);
            fileUpdate.AddReader(user);
            _repository.Update(Entity, fileUpdate);
        }

        public void RemoveReader(T Entity, long userId)
        {
            UserExist(userId);
            ListReadersNotNull(Entity);
            T fileUpdate = Entity;
            User user = _userRepository.Get(userId);
            Entity.RemoveReader(user);
            _repository.Update(Entity, fileUpdate);
        }

        public int NumberOfFoldersParents(Folder folder)
        {
            if (folder == null) return 0;
            return 1 + NumberOfFoldersParents(folder.Parent);
        }

        protected void FolderElementExists(long id)
        {
            if (_repository.Get(id) == null)
                throw new Exception("El elemento no existe.");
        }

        protected void ReadersExist(List<User> readers)
        {
            if(readers.Count != 0)
            {
                foreach (User reader in readers)
                {
                    if (_userRepository.Get(reader.Id) == null)
                        throw new Exception("Uno de los lectores especificados no existe.");
                }
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

        protected void ParentIsNull(T Entity)
        {
            if (Entity == null)
                throw new Exception("No existe una carpeta padre para el archivo.");
        }

        private void UserExist(long id)
        {
            if (_userRepository.Get(id) == null)
                throw new Exception("El usuario no existe.");
        }

        private void ListReadersNotNull(T entity)
        {
            if (entity.Readers == null)
                entity.Readers = new List<User>();
        }
    }
}

using System;
using System.Collections.Generic;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class FileLogic : ILogic<File>
    {
        IDataRepository<File> _fileRepository;
        IDataRepository<User> _userRepository;
        IDataRepository<Folder> _folderRepository;
        User _user;
        public FileLogic(IDataRepository<File> fileRepository, IDataRepository<User> userRepository, IDataRepository<Folder> folderRepository, User user)
        {
            _fileRepository = fileRepository;
            _userRepository = userRepository;
            _folderRepository = folderRepository;
            _user = user;
        }
        public void Add(File entity)
        {
            ValidateFormat(entity);
            UserHasPermissions(entity.OwnerId);
            entity.LastModifiedDate = DateTime.Now;
            entity.CreationDate = DateTime.Now;
            Folder folderBefore = entity.Parent;
            Folder folderAfter = entity.Parent;
            folderAfter.AddFile(entity);
            _fileRepository.Add(entity);
        }

        public void Delete(File Entity)
        {
            FileExists(Entity.Id);
            UserHasPermissions(Entity.OwnerId);
            _fileRepository.Delete(Entity);
        }

        public File Get(long id)
        {
            FileExists(id);
            UserHasPermissions(_fileRepository.Get(id).OwnerId);
            return _fileRepository.Get(id);
        }

        public IEnumerable<File> GetAll()
        {
            return _fileRepository.GetAll();
        }

        public void Update(File Entity, File newEntity)
        {
            newEntity.LastModifiedDate = DateTime.Now;
            ValidateFormat(newEntity);
            FileExists(Entity.Id);
            UserHasPermissions(Entity.OwnerId);
            HasTheSameLocation(Entity, newEntity);
            _fileRepository.Update(Entity, newEntity);
        }

        public void Move(File Entity, Folder folder)
        {
            FileExists(Entity.Id);
            FolderExists(folder.Id);
            FileAlreadyInFolder(Entity, folder);
            UserHasPermissions(Entity.OwnerId);
            UserHasPermissions(folder.OwnerId);
            Folder folderWhereFileWas = Entity.Parent;
            Folder folderWhereIsIt = folder;
            File filePrevious = Entity;
            Entity.Parent.RemoveFile(Entity);
            _folderRepository.Update(folderWhereFileWas, Entity.Parent);
            Entity.Parent = folder;
            folder.AddFile(Entity);
            _fileRepository.Update(filePrevious,Entity);
            _folderRepository.Update(folderWhereIsIt,folder);
        }

        public void AddReader(File Entity, User user)
        {
            UserHasPermissions(Entity.OwnerId);
            FolderExists(user);
            Entity.AddReader(user);
        }

        public void RemoveReader(File Entity, User user)
        {
            UserHasPermissions(Entity.OwnerId);
            FolderExists(user);
            Entity.RemoveReader(user);
        }

        private void HasTheSameLocation(File entity, File newEntity)
        {
            if(!entity.Parent.Equals(newEntity.Parent))
                throw new Exception("Las ubicaciones no coinciden.");
        }

        private void FolderExists(User user)
        {
            if(_userRepository.Get(user.Id) == null)
                throw new Exception("El usuario al que quiere agregar a los lectores no existe.");
        }

        private void UserHasPermissions(long ownerId)
        {
            if(_user.Id == ownerId)
                throw new Exception("El usuario no tiene los permisos para esta accion.");
        }

        private void FileAlreadyInFolder(File entity, FolderElement folder)
        {
            if(entity.Parent.Equals(folder))
                throw new Exception("El archivo ya esta en esa carpeta.");
        }

        private void FolderExists(long id)
        {
            if(_folderRepository.Get(id) == null)
                throw new Exception("La carpeta no existe.");
        }

        private void ValidateFormat(File entity)
        {
            NameIsNull(entity.Name);
            ParentIsNull(entity.Parent);
            ReadersIsNull(entity.Readers);
            ContentIsNull(entity.Content);
            OwnerExists(entity.OwnerId);
            ReadersExist(entity.Readers);
        }

        private void ReadersExist(List<User> readers)
        {
            foreach(User reader in readers)
            {
                if (_userRepository.Get(reader.Id) == null)
                    throw new Exception("Uno de los lectores especificados no existe.");
            }
        }

        private void OwnerExists(long owners)
        {
            if (_userRepository.Get(owners) == null)
                throw new Exception("Uno de los dueños especificado no existe.");
        }

        private void ContentIsNull(string content)
        {
            if (content == "")
                throw new Exception("El archivo no contiene nada.");
        }

        private void ReadersIsNull(List<User> readers)
        {
            if (readers.Count == 0)
                throw new Exception("El archivo no tiene ningun lector.");
        }

        private void ParentIsNull(Folder parent)
        {
            if (parent == null)
                throw new Exception("No existe una carpeta padre para el archivo.");
        }

        private void NameIsNull(string name)
        {
            if(name == "")
                throw new Exception("El nombre del archivo está vacio.");
        }

        private void FileExists(long id)
        {
            if(_fileRepository.Get(id) == null)
                throw new Exception("El archivo no existe.");
        }
    }
}

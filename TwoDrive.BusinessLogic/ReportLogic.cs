﻿using System;
using System.Collections.Generic;
using System.Linq;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class ReportLogic : IReport<File>
    {
        private IDataRepository<File> _repository;
        private IDataRepository<LogItem> _logRepository;
        private IDataRepository<User> _userRepository;
        public ReportLogic(IDataRepository<File> repository, IDataRepository<LogItem> logRepository, IDataRepository<User> userRepository)
        {
            _repository = repository;
            _logRepository = logRepository;
            _userRepository = userRepository;
        }

        public List<File> GetAllSortedFiles(string sortOrder = null, string fileName = null)
        {
            var files = from f in _repository.GetAll() select f;

            if (fileName != null)
            {
                files = files.Where(f => f.Name.Contains(fileName));
            }

            if (sortOrder == null)
            {
                sortOrder = "name_desc";
            }

            switch (sortOrder)
            {
                case "name_desc":
                    files = files.OrderByDescending(f => f.Name);
                    break;
                case "name_asc":
                    files = files.OrderBy(f => f.Name);
                    break;
                case "created_desc":
                    files = files.OrderByDescending(f => f.CreationDate);
                    break;
                case "created_asc":
                    files = files.OrderBy(f => f.CreationDate);
                    break;
                case "modified_desc":
                    files = files.OrderByDescending(f => f.LastModifiedDate);
                    break;
                case "modified_asc":
                    files = files.OrderBy(f => f.LastModifiedDate);
                    break;
                default:
                    files = files.OrderByDescending(f => f.Name);
                    break;
            }

            return files.ToList();
        }

        public List<File> GetSortedFiles(long userId, string sortOrder = null, string fileName = null)
        {
            var files = from f in _repository.GetAll() select f;

            files = files.Where(f => f.OwnerId == userId);

            if (fileName != null)
            {
                files = files.Where(f => f.Name.Contains(fileName));
            }

            if (sortOrder == null)
            {
                sortOrder = "name_desc";
            }

            switch (sortOrder)
            {
                case "name_desc":
                    files = files.OrderByDescending(f => f.Name);
                    break;
                case "name_asc":
                    files = files.OrderBy(f => f.Name);
                    break;
                case "created_desc":
                    files = files.OrderByDescending(f => f.CreationDate);
                    break;
                case "created_asc":
                    files = files.OrderBy(f => f.CreationDate);
                    break;
                case "modified_desc":
                    files = files.OrderByDescending(f => f.LastModifiedDate);
                    break;
                case "modified_asc":
                    files = files.OrderBy(f => f.LastModifiedDate);
                    break;
                default:
                    files = files.OrderByDescending(f => f.Name);
                    break;
            }

            return files.ToList();
        }

        public List<User> GetTop10FileOwners()
        {
            var tuples = from f in _repository.GetAll()
                         group f by f.OwnerId into fileGroups
                         select (fileGroups.Key, fileGroups.Count());
            tuples = tuples.OrderByDescending(
                t => t.Item2).Distinct().Take(10).ToList();

            var userList = new List<User>();

            foreach(var t in tuples){
                userList.Add(_userRepository.Get(t.Key));
            }

            return userList;
        }

        public int GetUserModifications(DateTime start, DateTime finish, User user)
        {
            int countMod = (from f in _logRepository.GetAll()
                            where (f.Date > start || f.Date < finish) && f.UserId == user.Id
                            select f).Count();

            return countMod / (finish - start).Days;
        }

        public int GetUserModifications(string start, string finish, User user)
        {
            var startDate = DateTime.UtcNow.AddDays(-7);
            var finishDate = DateTime.UtcNow;

            int countMod = (from f in _logRepository.GetAll()
                            where (f.Date > startDate || f.Date < finishDate) && f.UserId == user.Id
                            select f).Count();

            return countMod / (finishDate - startDate).Days;
        }

        public int GetUserModificationsFolders(User user)
        {
            var list = (from f in _logRepository.GetAll()
                        where (f.UserId == user.Id)
                        select f).ToList();
            int count = 0;
            foreach(var elem in list)
            {
                count += elem.Count;
            }
            return count;
        }
    }
}

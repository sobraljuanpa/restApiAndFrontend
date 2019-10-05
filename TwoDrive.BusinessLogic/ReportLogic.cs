using System;
using System.Collections.Generic;
using System.Linq;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic
{
    public class ReportLogic : IReport<File>
    {
        IDataRepository<File> _repository;
        public ReportLogic(IDataRepository<File> repository)
        {
            _repository = repository;
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
            }

            return files.ToList();
        }

        public IEnumerable<(long, int)> GetTop10FileOwners()
        {
            var tuples = from f in _repository.GetAll()
                         group f by f.OwnerId into fileGroups
                         select (fileGroups.Key, fileGroups.Count());
            tuples = tuples.OrderByDescending(
                t => t.Item2).Distinct().Take(10).ToList();

            return tuples;
        }
    }
}

using System;
using System.Collections.Generic;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic.Interface
{
    public interface IReport<T>
    {
        List<T> GetAllSortedFiles(string sortOrder = null, string fileName = null);
        List<T> GetSortedFiles(long userId, string sortOrder = null, string fileName = null);
        IEnumerable<(long,int)> GetTop10FileOwners();
        int GetUserModifications(DateTime start, DateTime finish, User user);

        int GetUserModifications(string start, string finish, User user);

    }
}

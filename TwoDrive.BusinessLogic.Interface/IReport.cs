using System.Collections.Generic;

namespace TwoDrive.BusinessLogic.Interface
{
    public interface IReport<T>
    {
        List<T> GetAllSortedFiles(string sortOrder = null, string fileName = null);
        List<T> GetSortedFiles(long userId, string sortOrder = null, string fileName = null);
        IEnumerable<(long,int)> GetTop10FileOwners();
    }
}

using System.Collections.Generic;
using TwoDrive.Domain;

namespace TwoDrive.ImportingStrategy
{
    public interface IMigration
    {
        User GiveMeUser();
        List<Folder> GiveMeFolders();
        List<File> GiveMeFiles();
    }
}

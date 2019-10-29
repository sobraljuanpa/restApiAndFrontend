using System;
using System.Collections.Generic;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic.Interface
{
    public interface IMigration
    {
        User GiveMeUser();
        List<Folder> GiveMeFolders();
        List<File> GiveMeFiles();
    }
}

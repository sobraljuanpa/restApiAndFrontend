using System;
using System.Collections.Generic;
using System.Text;

namespace TwoDrive.BusinessLogic.Interface
{
    public interface IMigrationLogic
    {
        void SaveUser();
        void SaveAllFolders();
        void SaveAllFiles();
    }
}

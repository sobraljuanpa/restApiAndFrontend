using System;
using System.Collections.Generic;
using System.Text;

namespace TwoDrive.BusinessLogic.Interface
{
    public interface IMigrationController
    {
        void SaveUser();
        void SaveAllFolders();
        void SaveAllFiles();
    }
}

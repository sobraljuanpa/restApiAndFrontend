using System;
using System.Collections.Generic;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic.Interface
{
    public interface IMigration
    {
        //Voy a buscar la clase por: todas las que implementan esta interfaz y la del nombre ..
        void specifyRoute(string path);
        User GiveMeUser();
        List<Folder> GiveMeFolders();
        List<File> GiveMeFiles();
    }
}

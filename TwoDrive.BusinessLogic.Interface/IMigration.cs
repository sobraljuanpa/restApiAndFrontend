using System;
using System.Collections.Generic;
using System.Text;
using TwoDrive.Domain;

namespace TwoDrive.BusinessLogic.Interface
{
    public interface IMigration
    {
        //Voy a buscar la clase por: todas las que implementan esta interfaz y la del nombre ..
        User GiveMeUser();
        //PARA EL ROOT FOLDER SOLO SE PONE EL NOMBRE -rootfolder NO NECESITA CREARLO
        List<Folder> GiveMeFolders();
        List<File> GiveMeFiles();
    }
}

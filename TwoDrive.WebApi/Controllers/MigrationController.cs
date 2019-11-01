using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.WebApi.Controllers
{
    [Route("/api/migration")]
    [Authorize]//esto hace q todos los endpoint del controller esten protegidos por defecto
    [ApiController]
    public class MigrationController : ControllerBase
    {
        private readonly FolderElementLogic<File> _fileLogic;
        private readonly FolderElementLogic<Folder> _folderLogic;
        private readonly ILogic<User> _userLogic;

        public MigrationController(FolderElementLogic<File> fileLogic, FolderElementLogic<Folder> folderLogic, ILogic<User> userLogic)
        {
            _userLogic = userLogic;
            _fileLogic = fileLogic;
            _folderLogic = folderLogic;
        }

        //POST: /api/migration/type
        [Authorize(Roles = Role.Admin)]
        [HttpPost("{type}")]
        public IActionResult PostMigration(string type, [FromBody] object[] param)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var migrationType = assembly.GetType("TwoDrive.BusinessLogic."+type+"Migration");
                IMigration migration = (IMigration)Activator.CreateInstance(migrationType, param);
                IMigrationController controller = new TwoDrive.BusinessLogic.MigrationController(migration, _userLogic, _folderLogic, _fileLogic);
                controller.SaveUser();
                controller.SaveAllFolders();
                controller.SaveAllFiles();
                return Ok("Migration completed successfully");
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

    }
}

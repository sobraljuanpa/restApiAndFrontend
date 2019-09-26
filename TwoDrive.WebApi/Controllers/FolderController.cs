using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TwoDrive.BusinessLogic;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.WebApi.Controllers
{
    [Route("/api/folders")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly FolderLogic _folderLogic;

        public FolderController(IDataRepository<Folder> repository, IDataRepository<User> userRepository)
        {
            _folderLogic = new FolderLogic(repository,userRepository);
        }

        //GET: /api/folders
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Folder> folder = _folderLogic.GetAll();
                return Ok(folder);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //GET: /api/folders/5
        [HttpGet("{folderId}", Name = "GetFolder")]
        public IActionResult Get(long folderId)
        {
            try
            {
                Folder folder = _folderLogic.Get(folderId);
                return Ok(folder);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //POST: /api/folders
        [HttpPost]
        public IActionResult Post([FromBody] Folder folders)
        {
            try
            {
                var folderAux = _folderLogic.Add(folders);
                return CreatedAtRoute(
                    routeName: "GetFolder",
                    routeValues: new { Id = folderAux.Id },
                    value: folderAux
                    );
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //POST: /api/folders/3/users/2
        [HttpPost("{folderId}/users/{idUsers}")]
        public IActionResult PostReaders(long folderId, long idUsers)
        {
            try
            {
                _folderLogic.AddReader(_folderLogic.Get(folderId), idUsers);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //PUT: /api/folders/5
        [HttpPut("{folderId}")]
        public IActionResult Put(long folderId, [FromBody] Folder folder)
        {
            try
            {
                _folderLogic.Update(_folderLogic.Get(folderId), folder);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //PUT: /api/folders/5/folders/3
        [HttpPut("{folderId}/folder/{idFolder}")]
        public IActionResult Move(long folderId, long idFolder)
        {
            try
            {
                _folderLogic.Move(folderId, idFolder);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //DELETE: /api/folders/5
        [HttpDelete("{folderId}")]
        public IActionResult Delete(long folderId)
        {
            try
            {
                _folderLogic.Delete(_folderLogic.Get(folderId));
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        //DELETE: /api/folders/5/users/3
        [HttpDelete("{folderId}/users/{idReader}")]
        public IActionResult DeleteReaders(long folderId, long idReader)
        {
            try
            {
                _folderLogic.RemoveReader(_folderLogic.Get(folderId), idReader);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }
}

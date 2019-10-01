using Microsoft.AspNetCore.Authorization;
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
        private readonly IDataRepository<User> _users;

        public FolderController(IDataRepository<Folder> repository, IDataRepository<User> userRepository, IDataRepository<File> fileRepository)
        {
            _folderLogic = new FolderLogic(repository,userRepository, fileRepository);
            _users = userRepository;
        }

        //GET: /api/folders
        [Authorize(Roles = Role.Admin)]
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
                bool canRead = false;
                var currentUser = _users.Get(int.Parse(User.Identity.Name));
                Folder folder = _folderLogic.Get(folderId);
                if (currentUser.Id == folder.OwnerId || currentUser.Role == "Admin") canRead = true;
                else
                {
                    foreach (var user in folder.Readers)
                    {
                        if (user.Id == currentUser.Id) canRead = true;
                    }
                }
                if (canRead) return Ok(folder);
                else return Unauthorized();
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
                if (_folderLogic.Get(folders.Parent.Id).OwnerId == int.Parse(User.Identity.Name))
                {
                    var folderAux = _folderLogic.Add(folders);
                    return CreatedAtRoute(
                        routeName: "GetFolder",
                        routeValues: new { folderId = folderAux.Id },
                        value: folderAux
                        );
                }
                else return Unauthorized();
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
                if (_folderLogic.Get(folderId).OwnerId == int.Parse(User.Identity.Name))
                {
                    _folderLogic.AddReader(_folderLogic.Get(folderId), idUsers);
                    return NoContent();
                }
                else return Unauthorized();
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
                if (_folderLogic.Get(folderId).OwnerId == int.Parse(User.Identity.Name))
                {
                    _folderLogic.Update(_folderLogic.Get(folderId), folder);
                    return NoContent();
                }
                else return Unauthorized();
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
                if (_folderLogic.Get(folderId).OwnerId == int.Parse(User.Identity.Name))
                {
                    _folderLogic.Move(folderId, idFolder);
                    return NoContent();
                }
                else return Unauthorized();
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
                if (_folderLogic.Get(folderId).OwnerId == int.Parse(User.Identity.Name))
                {
                    _folderLogic.Delete(_folderLogic.Get(folderId));
                    return NoContent();
                }
                else return Unauthorized();
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
                if (_folderLogic.Get(folderId).OwnerId == int.Parse(User.Identity.Name))
                {
                    _folderLogic.RemoveReader(_folderLogic.Get(folderId), idReader);
                    return NoContent();
                }
                else return Unauthorized();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }
}

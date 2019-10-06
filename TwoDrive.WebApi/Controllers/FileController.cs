using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.WebApi.Controllers
{
    [Route("/api/files")]
    [Authorize]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FolderElementLogic<File> _fileLogic;
        private readonly FolderElementLogic<Folder> _folderLogic;
        private readonly IReport<File> _reportLogic;
        private readonly IDataRepository<User> _users;

        public FileController(IReport<File> reportLogic, FolderElementLogic<File> fileLogic, FolderElementLogic<Folder> folderLogic, IDataRepository<User> userRepository)
        {
            _reportLogic = reportLogic;
            _fileLogic = fileLogic;
            _folderLogic = folderLogic;
            _users = userRepository;
        }

        //GET: /api/files?fileName=a&sortOrder=b
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult Get(string fileName, string sortOrder)
        {
            try
            {
                List<File> files = _reportLogic.GetAllSortedFiles(sortOrder, fileName);
                return Ok(files);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //GET: /api/files/top10
        [Authorize(Roles = Role.Admin)]
        [HttpGet("top10")]
        public IActionResult Get()
        {
            try
            {
                var top10 = _reportLogic.GetTop10FileOwners();
                return Ok(top10);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //GET: /api/files/5
        [HttpGet("{id}", Name = "GetFile")]
        public IActionResult Get(long id)
        {
            try
            {
                bool canRead = false;
                var currentUser = _users.Get(int.Parse(User.Identity.Name));
                File file = _fileLogic.Get(id);
                if (currentUser.Id == file.OwnerId || currentUser.Role == "Admin") canRead = true;
                else
                {
                    foreach (var user in file.Readers)
                    {
                        if (user.Id == currentUser.Id) canRead = true;
                    }
                }
                if (canRead) return Ok(file);
                else return Unauthorized();

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }            
        }

        //GET: /api/files/view?fileName=a&sortOrder=b
        [HttpGet("view")]
        public IActionResult GetSorted(string fileName, string sortOrder)
        {
            try
            {
                List<File> files = _reportLogic.GetSortedFiles(int.Parse(User.Identity.Name), sortOrder, fileName);
                return Ok(files);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //POST: /api/files
        [HttpPost]
        public IActionResult Post([FromBody] File file)
        {
            try
            {
                if (_folderLogic.Get(file.Parent.Id).OwnerId == int.Parse(User.Identity.Name))
                {
                    var fileAux = _fileLogic.Add(file);
                    return CreatedAtRoute(
                        routeName: "GetFile",
                        routeValues: new { id = fileAux.Id },
                        value: fileAux
                        );
                }
                else return Unauthorized();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //POST: /api/files/3/users/2
        [HttpPost("{fileId}/users/{idUsers}")]
        public IActionResult PostReaders(long fileId, long idUsers)
        {
            try
            {
                bool friendList = false;
                User user = _users.Get(int.Parse(User.Identity.Name));
                if (_fileLogic.Get(fileId).OwnerId == user.Id)
                {
                    foreach(var users in user.FriendList)
                    {
                        if (users.Id == idUsers) friendList = true;
                    }
                    if (friendList)
                    {
                        _fileLogic.AddReader(_fileLogic.Get(fileId), idUsers);
                        return NoContent();
                    }
                    else return Unauthorized("The user you are trying to add as a reader is not on your friendlist.");
                }
                else return Unauthorized();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //PUT: /api/files/5
        [HttpPut("{fileId}")]
        public IActionResult Put(long fileId, [FromBody] File file)
        {
            try
            {
                if (_fileLogic.Get(fileId).OwnerId == int.Parse(User.Identity.Name))
                {
                    _fileLogic.Update(_fileLogic.Get(fileId), file);
                    return NoContent();
                }
                else return Unauthorized();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //PUT: /api/files/5/folders/3
        [HttpPut("{fileId}/folder/{idFolder}")]
        public IActionResult Move(long fileId, long idFolder)
        {
            try
            {
                if (_fileLogic.Get(fileId).OwnerId == int.Parse(User.Identity.Name))
                {
                    _fileLogic.Move(fileId, idFolder);
                    return NoContent();
                }
                else return Unauthorized();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //DELETE: /api/files/5
        [HttpDelete("{fileId}")]
        public IActionResult Delete(long fileId)
        {
            try
            {
                if (_fileLogic.Get(fileId).OwnerId == int.Parse(User.Identity.Name) || User.IsInRole(Role.Admin))
                {
                    _fileLogic.Delete(_fileLogic.Get(fileId));
                    return NoContent();
                }
                else return Unauthorized();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            
        }

        //DELETE: /api/files/5/users/3
        [HttpDelete("{fileId}/users/{idReader}")]
        public IActionResult DeleteReaders(long fileId, long idReader)
        {
            try
            {
                if (_fileLogic.Get(fileId).OwnerId == int.Parse(User.Identity.Name))
                {
                    _fileLogic.RemoveReader(_fileLogic.Get(fileId), idReader);
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TwoDrive.BusinessLogic;
using TwoDrive.DataAccess.Interface;
using TwoDrive.Domain;

namespace TwoDrive.WebApi.Controllers
{
    [Route("/api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileLogic _fileLogic;
        private readonly IDataRepository<User> _users;

        public FileController(IDataRepository<File> repository, IDataRepository<Folder> folderRepository, IDataRepository<User> userRepository)
        {
            _fileLogic = new FileLogic(repository,folderRepository,userRepository);
            _users = userRepository;
        }

        //GET: /api/files
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<File> files = _fileLogic.GetAll();
                return Ok(files);
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

        //GET: /api/files/view?fileName=a&lastName=b
        [HttpGet("view")]
        public IActionResult GetSorted(string fileName, string sortOrder)
        {
            try
            {
                List<File> files = _fileLogic.GetSortedFiles(int.Parse(User.Identity.Name), sortOrder, fileName);
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
                if (_fileLogic.Get(file.Parent.Id).OwnerId == int.Parse(User.Identity.Name))
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
                if (_fileLogic.Get(fileId).OwnerId == int.Parse(User.Identity.Name))
                {
                    _fileLogic.AddReader(_fileLogic.Get(fileId), idUsers);
                    return NoContent();
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
                if (_fileLogic.Get(fileId).OwnerId == int.Parse(User.Identity.Name))
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

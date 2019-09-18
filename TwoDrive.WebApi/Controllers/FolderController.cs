using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            try
            {
                Folder folder = _folderLogic.Get(id);
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
                _folderLogic.Add(folders);
                return CreatedAtRoute("Get", new { Id = folders.Id }, folders);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //POST: /api/folders/3/users/2
        [HttpPost("{id}/users/{idUsers}")]
        public IActionResult PostReaders(long id, long idUsers)
        {
            try
            {
                _folderLogic.AddReader(_folderLogic.Get(id), idUsers);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //PUT: /api/folders/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Folder folder)
        {
            try
            {
                _folderLogic.Update(_folderLogic.Get(id), folder);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //PUT: /api/folders/5/folders/3
        [HttpPut("{id}/folder/{idFolder}")]
        public IActionResult Move(long id, long idFolder)
        {
            try
            {
                _folderLogic.Move(id, idFolder);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //DELETE: /api/folders/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                _folderLogic.Delete(_folderLogic.Get(id));
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        //DELETE: /api/folders/5/users/3
        [HttpDelete("{id}/users/{idReader}")]
        public IActionResult DeleteReaders(long id, long idReader)
        {
            try
            {
                _folderLogic.RemoveReader(_folderLogic.Get(id), idReader);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }
}

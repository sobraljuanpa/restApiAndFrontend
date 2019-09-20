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

        public FileController(IDataRepository<File> repository, IDataRepository<Folder> folderRepository, IDataRepository<User> userRepository)
        {
            _fileLogic = new FileLogic(repository,folderRepository,userRepository);
        }

        //GET: /api/files
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
        [HttpGet("{id}")]
        public IActionResult Get(long fileId)
        {
            try
            {
                File file = _fileLogic.Get(fileId);
                return Ok(file);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }            
        }

        //POST: /api/files
        [HttpPost]
        public IActionResult Post([FromBody] File file)
        {
            try
            {
                _fileLogic.Add(file);
                return CreatedAtRoute("Get", new { Id = file.Id }, file);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //POST: /api/files/3/users/2
        [HttpPost("{id}/users/{idUsers}")]
        public IActionResult PostReaders(long fileId, long idUsers)
        {
            try
            {
                _fileLogic.AddReader(_fileLogic.Get(fileId), idUsers);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //PUT: /api/files/5
        [HttpPut("{id}")]
        public IActionResult Put(long fileId, [FromBody] File file)
        {
            try
            {
                _fileLogic.Update(_fileLogic.Get(fileId), file);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //PUT: /api/files/5/folders/3
        [HttpPut("{id}/folder/{idFolder}")]
        public IActionResult Move(long fileId, long idFolder)
        {
            try
            {
                _fileLogic.Move(fileId, idFolder);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //DELETE: /api/files/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long fileId)
        {
            try
            {
                _fileLogic.Delete(_fileLogic.Get(fileId));
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            
        }

        //DELETE: /api/files/5/users/3
        [HttpDelete("{id}/users/{idReader}")]
        public IActionResult DeleteReaders(long fileId, long idReader)
        {
            try
            {
                _fileLogic.RemoveReader(_fileLogic.Get(fileId),idReader);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }
}

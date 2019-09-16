using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.WebApi.Controllers
{
    [Route("/api/users")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ILogic<File> _fileLogic;
        private readonly ILogic<Folder> _folderLogic;
        private readonly ILogic<User> _userLogic;

        public FileController(ILogic<File> fileLogic, ILogic<Folder> folderLogic, ILogic<User> userLogic)
        {
            _fileLogic = fileLogic;
            _folderLogic = folderLogic;
            _userLogic = userLogic;
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
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            try
            {
                File file = _fileLogic.Get(id);
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
        [HttpPost("{id},{id}")]
        public IActionResult PostReaders(long idFile, long idReader)
        {
            try
            {
                _fileLogic.AddReader(_fileLogic.Get(idFile), _userLogic.Get(idReader));
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //PUT: /api/files/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] File file)
        {
            try
            {
                _fileLogic.Update(_fileLogic.Get(id), file);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //PUT: /api/files/5/folder/3
        [HttpPut("{id},{id}")]
        public IActionResult Move(long idFile, long idFolder)
        {
            try
            {
                _fileLogic.Move(_fileLogic.Get(idFile), _folderLogic.Get(idFolder));
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //DELETE: /api/files/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                _fileLogic.Delete(_fileLogic.Get(id));
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            
        }

        //DELETE: /api/files/5/users/3
        [HttpDelete("{id}")]
        public IActionResult DeleteReaders(long idFile,long idReader)
        {
            try
            {
                _fileLogic.RemoveReader(_fileLogic.Get(idFile), _userLogic.Get(idReader));
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }
}

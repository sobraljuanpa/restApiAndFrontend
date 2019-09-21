using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.WebApi.Controllers
{
    [Route("/api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogic<User> _userLogic;

        public UserController(ILogic<User> userLogic)
        {
            _userLogic = userLogic;
        }

        //GET: /api/users
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<User> users = _userLogic.GetAll();
                return Ok(users);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //GET: /api/users/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                User user = _userLogic.Get(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //POST: /api/users
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                _userLogic.Add(user);
                return CreatedAtRoute("Get", new { Id = user.Id }, user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //PUT: /api/users/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] User user)
        {
            try
            {
                _userLogic.Update(_userLogic.Get(id), user);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //DELETE: /api/users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                _userLogic.Delete(_userLogic.Get(id));
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}

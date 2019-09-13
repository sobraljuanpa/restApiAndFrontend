using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwoDrive.DataAccess;
using TwoDrive.Domain;

namespace TwoDrive.WebApi.Controllers
{
    [Route("/api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IDataRepository<User> _userRepository;

        public UserController(IDataRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        //GET: /api/users
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<User> users = _userRepository.GetAll();
            return Ok(users);
        }

        //GET: /api/users/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            User user = _userRepository.Get(id);

            if(user == null)
            {
                return NotFound("There is no user record with a matching id.");
            }

            return Ok(user);
        }

        //POST: /api/users
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if(user == null)
            {
                return BadRequest("User is null.");
            }

            _userRepository.Add(user);
            return CreatedAtRoute("Get", new { Id = user.Id }, user);
        }

        //PUT: /api/users/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] User user)
        {
            if(user == null)
            {
                return BadRequest("User is null.");
            }

            User toUpdate = _userRepository.Get(id);
            if(toUpdate == null)
            {
                return NotFound("Provided id does not match any users.");
            }

            _userRepository.Update(toUpdate, user);
            return NoContent();
        }

        //DELETE: /api/users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            User user = _userRepository.Get(id);
            if(user == null)
            {
                return NotFound("Provided id does not match any users.");
            }

            _userRepository.Delete(user);
            return NoContent();
        }
    }
}

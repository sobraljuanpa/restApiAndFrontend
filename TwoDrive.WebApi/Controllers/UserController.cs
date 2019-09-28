using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.WebApi.Controllers
{
    [Route("/api/users")]
    //[Authorize]//esto hace q todos los endpoint del controller esten protegidos por defecto
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogic<User> _userLogic;

        public UserController(ILogic<User> userLogic)
        {
            _userLogic = userLogic;
        }

        //Endpoint para autenticacion
        [AllowAnonymous]//este cualquiera puede tirarle
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParams)
        {
            var user = _userLogic.Authenticate(userParams.Username, userParams.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }

        //GET: /api/users
        [Authorize(Roles = Role.Admin)]//solo dejo que los admins accedan a todos los usuarios
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
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(long id)
        {
            try
            {
                User user = _userLogic.Get(id);
                //Si no hay un usuario con ese id retorno 404 (Not found)
                if (user == null)
                {
                    return NotFound();
                }

                var currentUserId = int.Parse(User.Identity.Name);
                //Si el usuario autenticado no es el mismo que el del id/un admin retorno 401 (Unauthorized)
                if (id != currentUserId && !User.IsInRole(Role.Admin))
                {
                    return Unauthorized();
                }

                return Ok(User);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //POST: /api/users
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                var auxUser = _userLogic.Add(user);
                return CreatedAtRoute(
                    routeName: "GetUser",
                    routeValues: new { Id = auxUser.Id },
                    value: auxUser
                    );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //PUT: /api/users/5
        [Authorize(Roles = Role.Admin)]
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
        [Authorize(Roles = Role.Admin)]
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

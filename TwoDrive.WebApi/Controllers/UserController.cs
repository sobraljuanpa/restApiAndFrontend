﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwoDrive.BusinessLogic;
using TwoDrive.BusinessLogic.Interface;
using TwoDrive.Domain;

namespace TwoDrive.WebApi.Controllers
{
    [Route("/api/users")]
    [Authorize]//esto hace q todos los endpoint del controller esten protegidos por defecto
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
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var user = _userLogic.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            //solo dejo que los admins accedan a otros usuarios por Id
            var currentUserId = int.Parse(User.Identity.Name);//esto en realidad trae toda la info del usuario?
            if (id != currentUserId && !User.IsInRole(Role.Admin))
            {
                return Forbid();
            }

            return Ok(user);
            //try
            //{
            //    User user = _userLogic.Get(id);
            //    return Ok(user);
            //}
            //catch (Exception e)
            //{
            //    return NotFound(e.Message);
            //}
        }

        //POST: /api/users
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                var auxUser = _userLogic.Add(user);
                return CreatedAtRoute("Get", new { Id = auxUser.Id }, auxUser);
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

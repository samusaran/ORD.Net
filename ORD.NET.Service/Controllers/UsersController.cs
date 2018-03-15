using Microsoft.AspNetCore.Mvc;
using ORD.NET.DAL;
using ORD.NET.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORD.NET.Service.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private IUserRepository _users;

        public UsersController(IUserRepository u)
        {
            _users = u;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("{utente:alpha}/email")]
        public async Task<IActionResult> GetUserEmail(string utente)
        {
            if (utente == null)
                return BadRequest();

            var result = await _users.GetUserEmailAsync(utente);
            if (result == null)
                return NotFound(utente);
            else
                return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("{name:alpha}")]
        public async Task<IActionResult> GetUser(string name, [FromQuery] bool registeredOnly)
        {
            if (name == null)
                return BadRequest();

            var result = await _users.GetUserByNameAsync(name);
            if (result == null)
                return NotFound(name);
            else
                return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _users.GetAllUsersAsync();
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ORD.NET.DAL;
using ORD.NET.Model.Business;
using ORD.NET.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORD.NET.Service.Controllers
{
    [Route("api/groups")]
    public class GroupsController : Controller
    {
        private IGroupsRepository _groups;

        public GroupsController(IGroupsRepository g)
        {
            _groups = g;
        }

        [HttpGet("{user:alpha}")]
        [ProducesResponseType(typeof(IEnumerable<Group>), 200)]
        public async Task<IActionResult> GetAvailableGroups(string user)
        {
            var result = await _groups.GetGroups(user);
            return Ok(result);
        }
    }
}

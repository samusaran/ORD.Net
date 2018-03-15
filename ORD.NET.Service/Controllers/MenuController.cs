using Microsoft.AspNetCore.Mvc;
using ORD.NET.DAL;
using ORD.NET.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORD.NET.Service.Controllers
{
    [Route("api/menu")]
    public class MenuController : Controller
    {
        private IDishRepository _menu;

        public MenuController(IDishRepository d)
        {
            _menu = d;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DishEntry>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMenu(int id)
        {
            var result = await _menu.GetMenu(id);

            if (result == null)
                return NotFound(id);
            else
                return Ok(id);
        }
    }
}

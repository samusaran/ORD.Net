using Microsoft.AspNetCore.Mvc;
using ORD.NET.DAL;
using ORD.NET.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORD.NET.Service.Controllers
{
    [Route("api/dishtypes")]
    public class DishTypeController : Controller
    {
        private IDishTypeRepository _types;

        public DishTypeController(IDishTypeRepository t)
        {
            _types = t;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DishType>), 200)]
        public async Task<IActionResult> GetDishTypes()
        {
            return Ok(await _types.GetDishTypesAsync(null));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(DishType), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDishType(int id)
        {
            var result = await _types.GetDishTypeAsync(id);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet]
        [Route("zeppelin/{zeppelin}")]
        [ProducesResponseType(typeof(List<DishType>), 200)]
        public async Task<IActionResult> GetDishTypes(int zeppelin)
        {
            var result = await _types.GetDishTypesAsync(zeppelin);
            return Ok(result);
        }

        [HttpGet]
        [Route("_calculate")]
        [ProducesResponseType(typeof(DishType), 200)]
        public async Task<IActionResult> CalculateDishType([FromQuery] string order, [FromQuery] int zeppelin)
        {
            var result = await _types.CalculateDishType(order, zeppelin);
            return Ok(result);
        }
    }
}

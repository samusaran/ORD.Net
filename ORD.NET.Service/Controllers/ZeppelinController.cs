using Microsoft.AspNetCore.Mvc;
using ORD.NET.DAL;
using ORD.NET.Model.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORD.NET.Service.Controllers
{
    [Route("api/zeppelin")]
    public class ZeppelinController : Controller
    {
        private IZeppelinRepository _zeppelins;

        public ZeppelinController(IZeppelinRepository z)
        {
            _zeppelins = z;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Zeppelin>), 200)]
        public async Task<IActionResult> GetAllZeppelins()
        {
            return Ok(await _zeppelins.GetAllZeppelinsAsync());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Zeppelin), 200)]
        public async Task<IActionResult> GetZeppelin(int id)
        {
            return Ok(await _zeppelins.GetZeppelinAsync(id));
        }

        [HttpGet("{name:alpha}")]
        [ProducesResponseType(typeof(Zeppelin), 200)]
        public async Task<IActionResult> GetZeppelinByName(string name)
        {
            var result = await _zeppelins.GetZeppelinAsync(name);
            return Ok(result);
        }
    }
}

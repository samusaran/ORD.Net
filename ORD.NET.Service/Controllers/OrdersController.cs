using Microsoft.AspNetCore.Mvc;
using ORD.NET.DAL;
using ORD.NET.Model.Business;
using ORD.NET.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace ORD.NET.Service.Controllers
{
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orders;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository o, AutoMapper.IMapper m)
        {
            _orders = o;
            _mapper = m;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(Order), 201)]
        public async Task<IActionResult> InsertOrder(OrderDTO o)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (o == null)
                return BadRequest();

            var result = await _orders.InsertAsync(o);

            return CreatedAtRoute("GetOrder", new { id = result.IdOrdinazione }, result);
        }

        [HttpGet("group/{group:int}")]
        [ProducesResponseType(typeof(IEnumerable<OrderDTO>), 200)]
        public async Task<IActionResult> GetOrders(int group, [FromQuery] bool? includeMissing)
        {
            var result = await _orders.GetOrdersAsync(group, null, null, includeMissing ?? false);

            var mappedResult = _mapper.Map<List<OrderDTO>>(result);
            return Ok(mappedResult);
        }

        [HttpGet("group/{group:int}/zeppelin/{zp:int}")]
        [ProducesResponseType(typeof(IEnumerable<OrderDTO>), 200)]
        public async Task<IActionResult> GetOrdersByZeppelin(int group, int zp, [FromQuery] bool? includeMissing)
        {
            var result = await _orders.GetOrdersAsync(group, zp, null, includeMissing ?? false);
            var mappedResult = _mapper.Map<List<OrderDTO>>(result);
            return Ok(mappedResult);
        }

        [HttpGet("{id:int}", Name = "GetOrder")]
        [ProducesResponseType(typeof(Order), 200)]
        public async Task<IActionResult> GetOrder(int id)
        {
            var result = await _orders.GetOrderAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(400)]
        [Route("user/{name:alpha}")]
        public async Task<IActionResult> GetUserOrder(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }

            return Ok(await _orders.GetOrderOfUserAsync(name));
        }

        [HttpHead]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Route("user/{name:alpha}")]
        public async Task<IActionResult> HasUserOrdered(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }

            var exists = await _orders.HasUserOrdered(name);
            if (exists)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _orders.DeleteAsync(id);

            if (deleted)
                return NoContent(); // 204
            else
                return NotFound();
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(bool), 200)]
        [Route("sent/user/{name:alpha}")]
        public async Task<IActionResult> IsOrderAlreadySent(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }

            bool found = false;
            found = await _orders.IsOrderAlreadySentAsync(name);

            return Ok(found);
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(bool), 200)]
        [Route("sent/zeppelin/{id:int}")]
        public async Task<IActionResult> IsOrderAlreadySent(int id)
        {
            bool found = false;
            found = await _orders.IsOrderAlreadySentAsync(id);

            return Ok(found);
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(int), 200)]
        [Route("_count")]
        public async Task<IActionResult> GetOrderCount([FromQuery]int? zeppelin)
        {
            if (!zeppelin.HasValue)
            {
                return BadRequest();
            }

            int result = await _orders.GetOrderCountAsync(zeppelin.Value);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ChartOrderItem>), 200)]
        [Route("_forGraph")]
        public async Task<IActionResult> GetOrdersForGraph()
        {
            var result = await _orders.GetOrdersForGraphAsync();

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [Route("_history")]
        public async Task<IActionResult> GetUserOrders([FromQuery] string user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var result = await _orders.GetUserHistoryAsync(user);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(int), 200)]
        [Route("setmissing/user/{name:alpha}")]
        public async Task<IActionResult> SetUserAsMissing(string name)
        {
            var result = await _orders.SetUserAsMissingAsync(name);

            return Accepted(result);
        }
    }
}

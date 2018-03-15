using Microsoft.AspNetCore.Mvc;
using ORD.NET.DAL;
using ORD.NET.Model.Business;
using System;
using System.Threading.Tasks;

namespace ORD.NET.Service.Controllers
{
    [Route("api/mail")]
    public class MailController : Controller
    {
        private ILogMailRepository _mail;

        public MailController(ILogMailRepository m)
        {
            _mail = m;
        }

        [HttpPost]
        [Route("zeppelin/{id:int}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> LogMail(int id)
        {
            var result = await _mail.AddLogMail(id, DateTime.Now);
            return Ok();
        }
    }
}

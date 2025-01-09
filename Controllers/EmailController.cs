using aHoang.Entities;
using aHoang.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aHoang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost]
        public async Task<ActionResult> sendEmail(string rcv, string subject, string body)
        {
            var res= await _emailService.SendEmail(rcv, subject, body);
            if (res)
            return Ok();
            return BadRequest();
        }
    }
}

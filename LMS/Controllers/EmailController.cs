using LMS.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            // Initialize the EmailController with IEmailService dependency
            this.emailService = emailService;
        }

       // [HttpPost]
      //  public IActionResult SendEmail(RequestDTO request)
       // {
            // Call the SendEmail method of the EmailService to send the email
        //    var response = emailService.SendEmail(request);

            // Return the response from the SendEmail method
       //     return Ok(response);
       // }
    }
}

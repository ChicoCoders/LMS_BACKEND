using Hangfire;
using LMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IResourceService _resourceService;
      

        public AutoController(IReservationService reservationService,IResourceService resourceService)
        {
            _reservationService = reservationService;
            _resourceService = resourceService;

            
        }

        [HttpGet]
        [Route("RecurringJob")]

        //[Authorize]  // Add authorization if needed
        public IActionResult RecurringJobs()
        {
            RecurringJob.AddOrUpdate("SetOverdueJob", () => _reservationService.setOverdue(), Cron.Daily());
            RecurringJob.AddOrUpdate("Add Update", () => _resourceService.WeeklyBookUpdates(), Cron.Weekly());
            
            return Ok("Recurring job scheduled.");
        }

       
    }
}

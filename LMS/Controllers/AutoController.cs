using Hangfire;
using LMS.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public AutoController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }


        [HttpGet]
        [Route("RecurringJob")]
        
        public string RecurringJobs()
        {
            RecurringJob.AddOrUpdate(() =>  _reservationService.setOverdue(), Cron.Minutely());
            return "yes";
        }

    }
}

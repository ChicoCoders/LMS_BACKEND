using LMS.DTOs;
using LMS.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }



       [HttpPost("getAdminDashboradData")]
        public async Task<DashboardStatics> getAdminDashboradData()
        {
            return await _dashboardService.getAdminDashboradData();
        }

        [HttpPost("getOverDueList")]
        public async Task<List<ReservationDto>> getOverdueList()
        {
            return await _dashboardService.getOverdueList();
        }

        [HttpPost("getLastWeekReservations")]
        public async Task<List<LastWeekReservations>> getLastWeekReservations()
        {
            return await _dashboardService.getLastWeekReservations();
        }
    }
}

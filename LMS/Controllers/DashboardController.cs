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



       [HttpGet("getAdminDashboradData")]
        public async Task<DashboardStatics> getAdminDashboradData()
        {
            return await _dashboardService.getAdminDashboradData();
        }

        [HttpGet("getOverDueList")]
        public async Task<List<ReservationDto>> getOverdueList()
        {
            return await _dashboardService.getOverdueList();
        }

        [HttpGet("getLastWeekReservations")]
        public async Task<List<LastWeekReservations>> getLastWeekReservations()
        {
            return await _dashboardService.getLastWeekReservations();
        }
    }
}

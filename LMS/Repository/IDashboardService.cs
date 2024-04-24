using LMS.DTOs;

namespace LMS.Repository
{
    public interface IDashboardService
    {
        Task<DashboardStatics> getAdminDashboradData();
        Task<List<ReservationDto>> getOverdueList();
        Task<List<LastWeekReservations>> getLastWeekReservations();
    }
}

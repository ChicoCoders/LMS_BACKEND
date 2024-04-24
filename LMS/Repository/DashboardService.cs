using LMS.DTOs;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LMS.Repository
{
    public class DashboardService : IDashboardService
    {
        private readonly DataContext _dataContext;

        public DashboardService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<DashboardStatics> getAdminDashboradData()
        {
            var Statics = new DashboardStatics
            {
                Total = _dataContext.Resources.Count(),
                Navels = _dataContext.Resources.Where(e => e.Type == "Navel").Count(),
                Journals = _dataContext.Resources.Where(e => e.Type == "Jounals").Count(),
                Ebooks = _dataContext.Resources.Where(e => e.Type == "Ebooks").Count(),
                Users = _dataContext.Users.Count(),
                Reservations = _dataContext.Reservations.Count(),
                Requests = _dataContext.Requests.Count(),
                OverDue = _dataContext.Reservations.Where(e => e.Status == "overdue").Count()
            };

            return Statics;
        }
        public async Task<List<ReservationDto>> getOverdueList()
        {
            var k = new List<Reservation>();
            k = _dataContext.Reservations.Where(e => e.Status == "overdue").ToList();

            List<ReservationDto> reservationlist = new List<ReservationDto>();
            foreach (var x in k)
            {
                var userob = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName == x.BorrowerID);
                var res = new ReservationDto
                {
                    reservationNo = x.Id,
                    Resource = x.ResourceId,
                    BorrowerName = x.BorrowerID,
                    UserName = userob.FName + " " + userob.LName,
                    DueDate = x.DueDate,
                    Status = x.Status//need to look due or not
                };
                reservationlist.Add(res);
            }
            return reservationlist;

        }

        public async Task<List<LastWeekReservations>> getLastWeekReservations()
        {
            var lastWeekList = new List<LastWeekReservations>();
            for (int x = 6; x >= 0; x--)
            {
                DateOnly issueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-x));
                int count = _dataContext.Reservations.Where(e => e.IssuedDate == issueDate).Count();

                var a = new LastWeekReservations
                {
                    day = issueDate.ToString("MM-dd"),
                    y = count,
                };
                lastWeekList.Add(a);
            }
            return lastWeekList;
        }

        public async Task<List<LastWeekReservations>> getLastWeekUsers()
        {
            var lastWeekList = new List<LastWeekReservations>();
            for (int x = 6; x >= 0; x--)
            {
                DateOnly issueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-x));
                int count = _dataContext.Reservations.Where(e => e.IssuedDate == issueDate).Count();

                var a = new LastWeekReservations
                {
                    day = issueDate.ToString("MM-dd"),
                    y = count,
                };
                lastWeekList.Add(a);
            }
            return lastWeekList;
        }
    }
}

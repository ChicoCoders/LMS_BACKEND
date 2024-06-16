using LMS.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
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
            var count =_dataContext.Resources.Sum(e=>e.Quantity)+ _dataContext.Resources.Sum(e => e.Borrowed);
            var Statics = new DashboardStatics
            {
                Total = count,
                IssueToday = _dataContext.Reservations.Where(e => e.IssuedDate== DateOnly.FromDateTime(DateTime.Now)).Count(),
                ReturnToday = _dataContext.Reservations.Where(e => e.ReturnDate == DateOnly.FromDateTime(DateTime.Now)).Count(),
                Locations = _dataContext.Cupboard.Count(),
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
                int count = _dataContext.Users.Where(e => e.AddedDate == issueDate).Count();

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

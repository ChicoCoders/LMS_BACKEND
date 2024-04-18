using LMS.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repository
{
    public class NotificationService:INotificationService
    {
        private readonly DataContext _Context;
 


        public NotificationService(DataContext Context)
        {
            _Context = Context;
            
        }

        public async Task<bool> NewNotice(NewNoticeDto newnotice)
        {
            var notice = new Notification
            {
                Title=newnotice.Subject,
                ToUser=newnotice.UserName,
                Description=newnotice.Description,
                Date=new DateOnly(2023,11,02),
                time=new TimeOnly(22,11)
                
            };
            _Context.Notifications.Add(notice); 
            await _Context.SaveChangesAsync();
            return true;
        }


        public async Task<List<NewNoticeDto>> GetNotification(string username)
        {
            var notifications = new List<Notification>();
            if (username == "all")
            {
                notifications = _Context.Notifications.ToList();
            }
            else
            {
                notifications = _Context.Notifications.Where(e => e.ToUser == username).ToList();
            }
            var notificationlist = new List<NewNoticeDto>();
            if (notifications != null)
            {
                foreach(var x in notifications)
                {
                    var y = new NewNoticeDto
                    {
                        UserName=x.ToUser,
                        Subject = x.Title,
                        Date = x.Date,
                        Description = x.Description,

                    };
                    notificationlist.Add(y);
                }
            }
            return notificationlist;
        }

        public async Task<bool> SetRemind(RemindNotification remind)
        {
            var pastremind= _Context.RemindNotifications.FirstOrDefault();
            pastremind.Content = remind.Content;
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IssueNotification(int reservationNo)
        {
            var reservation = await _Context.Reservations.FirstOrDefaultAsync(e => e.Id == reservationNo);
            if (reservation != null)
            {
                
                var notification = new Notification
                {
                    Title = "About Your ReservationNo : "+reservation.Id,
                    Description = "Reservation No : " + reservation.ReservationNo + ", User Id : " + reservation.BorrowerID + ", Date : " + reservation.IssuedDate + ", Due Date : " + reservation.DueDate,
                    ToUser = reservation.BorrowerID,
                    Date = reservation.IssuedDate,
                    time = new TimeOnly(22,11),
                    Type="reservation"
                    
                };
                _Context.Notifications.Add(notification);
                await _Context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
            
        }

        public async Task<bool> ReturnNotification(int reservationNo)
        {
            var reservation = await _Context.Reservations.FirstOrDefaultAsync(e => e.Id == reservationNo);
            if (reservation != null)
            {

                
                var notification = new Notification
                {
                    Title = "Return Resource Successfully.ReservationNo : "+reservation.Id,
                    Description = "Reservation No : " + reservation.ReservationNo + ", User Id : " + reservation.BorrowerID + ", Return Date : " + reservation.ReturnDate + ", Due Date : " + reservation.DueDate,
                    ToUser = reservation.BorrowerID,
                    Date = reservation.IssuedDate
                    
                };
                _Context.Notifications.Add(notification);
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}

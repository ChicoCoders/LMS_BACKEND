using LMS.DTOs;

namespace LMS.Repository
{
    public interface INotificationService
    {
        Task<bool> NewNotice(NoticeDto newnotice);
        Task<List<NewNoticeDto>> GetNotification();
        Task<List<MyNotificationDto>> GetMyNotification(HttpContext httpContext);
        Task<bool> SetRemind(Reservation reservation);
        Task<bool> IssueNotification(int reservationNo);
        Task<bool> ReturnNotification(int reservationNo);
        Task<bool> RemoveNotification(int reservationNo);
        Task<bool> SetFireBaseToken(SetToken setToken);
        Task<bool> RemoveFireBaseToken(SetToken setToken);
        Task<bool> MarkAsRead(int id);
        Task<int> UnreadCount(HttpContext httpContext);
        Task<bool> BookAddedNotifications();
    }
}

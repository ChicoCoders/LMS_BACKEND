using Azure.Core;
using LMS.DTOs;
using LMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpPost("NewNotice")]
        public async Task<bool> NewNotice(NewNoticeDto newnotice)
        {
            return await _notificationService.NewNotice(newnotice);
        }

        [HttpPost("SetRemind")]
        public async Task<bool> SetRemind(RemindNotification remind)
        {
            return await _notificationService.SetRemind(remind);
        }

        [HttpGet("GetNotificatons")]
        public async Task<List<NewNoticeDto>> GetNotification(string username)
        {
           return await _notificationService.GetNotification(username);
        }
    }
}

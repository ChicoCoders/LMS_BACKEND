﻿using LMS.DTOs;

namespace LMS.Repository
{
    public interface INotificationService
    {
        Task<bool> NewNotice(NewNoticeDto newnotice);
        Task<List<NewNoticeDto>> GetNotification(string username);
        Task<bool> SetRemind(RemindNotification remind);
        Task<bool> IssueNotification(int reservationNo);
        Task<bool> ReturnNotification(int reservationNo);
    }
}

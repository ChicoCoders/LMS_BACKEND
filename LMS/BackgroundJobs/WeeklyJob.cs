using LMS.Repository;
using Microsoft.VisualBasic;
using Quartz;

namespace LMS.BackgroundJobs
{
    public class WeeklyJob:IJob
    {
        private readonly INotificationService _notificationService;
        public WeeklyJob(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task Execute(IJobExecutionContext context)
        {

            Console.WriteLine("Add book notify");
            await _notificationService.BookAddedNotifications();
           
        }
    }
}

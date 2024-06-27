using LMS.Repository;
using Quartz;

namespace LMS.BackgroundJobs
{
    public class DailyJob:IJob
    {
        private readonly IReservationService _reservationService;
        public DailyJob(IReservationService reservationService)
        {

            _reservationService = reservationService;

        }
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Overdue Job is running");
            await _reservationService.addPenalty();
            // Your job logic here
            await _reservationService.setOverdue();
        }
    }
}

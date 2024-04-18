namespace LMS.Repository
{
    public class ReportService : IReportService
    {

       private readonly DataContext _context;

       public ReportService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> generateReport()
        {

            return true;
        }
    }
}

namespace LMS.Repository
{
    public interface IReportService
    {
        Task<bool> generateReport();
       
    }
}

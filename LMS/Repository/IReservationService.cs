using LMS.DTOs;

namespace LMS.Repository
{
    public interface IReservationService
    {
        Task<IssueBookFormResponseDto> LoadIssueForm(string isbn);
        Task<IssueBookResponseDto> IssueBook(IssueBookRequestDto request,HttpContext httpContext);
        Task<AboutReservationDto> AboutReservation(int resId);
        Task<bool> ReturnBook(ReturnBookDto request,HttpContext httpContext);
        Task<List<ReservationDto>> SearchReservation(SearchDetails details,HttpContext httpContext);
        Task<bool> deleteReservation(int id);
        Task<bool> extendDue(int id,string due);
        Task setOverdue();
    }
}

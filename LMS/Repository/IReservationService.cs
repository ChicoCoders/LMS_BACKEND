using LMS.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Repository
{
    public interface IReservationService
    {
        Task<IActionResult> LoadIssueForm(string isbn);
        Task<IActionResult> IssueBook(IssueBookRequestDto request,HttpContext httpContext);
        Task<IActionResult> AboutReservation(int resId);
        Task<IActionResult> ReturnBook(ReturnBookDto request,HttpContext httpContext);
        Task<IActionResult> SearchReservation(SearchDetails details,HttpContext httpContext);
        Task<IActionResult> deleteReservation(int id);
        Task<IActionResult> extendDue(int id,string due);
        Task setOverdue();
    }
}

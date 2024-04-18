using LMS.DTOs;
using LMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace LMS.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("Issuebook")]
        public async Task<IssueBookResponseDto> IssueBook(IssueBookRequestDto request)
        {
            var httpContext = HttpContext;
            return await _reservationService.IssueBook(request,httpContext);
        }

        [HttpPost("Returnbook")]
        public async Task<bool> ReturnBook(ReturnBookDto request)
        {
            var httpContext = HttpContext;
            return await _reservationService.ReturnBook(request,httpContext);
        }

        [HttpGet("LoadReservation")]
        public async Task<IssueBookFormResponseDto> LoadIssueForm(string isbn)
        {
            return await _reservationService.LoadIssueForm(isbn);
        }

        [HttpGet("About")]
        public async Task<AboutReservationDto> AboutReservation(int resId)
        {
            return await _reservationService.AboutReservation(resId);
        }

        [HttpPost("SearchReservation")]
        public async Task<List<ReservationDto>> SearchReservation(SearchDetails details)
        {
            var httpContext = HttpContext;
            return await _reservationService.SearchReservation(details,httpContext);
        }

        [HttpDelete("DeleteReservation")]
        public async Task<bool> deleteReservation(int id)
        {
            return await _reservationService.deleteReservation(id);
        }

        [HttpPut("ExtendDue")]
        public async Task<bool> extendDue(int id,string due)
        {
            return await _reservationService.extendDue(id,due);
        }

    }
}

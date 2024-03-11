using LMS.DTOs;
using LMS.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IssueBookResponseDto> IssueBook(IssueBookRequestDto request)
        {
            return await _reservationService.IssueBook(request);
        }

        [HttpGet]
        public async Task<IssueBookFormResponseDto> LoadIssueForm(string isbn)
        {
            return await _reservationService.LoadIssueForm(isbn);
        }

    }
}

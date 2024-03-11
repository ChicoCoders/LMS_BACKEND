using LMS.DTOs;

namespace LMS.Repository
{
    public interface IReservationService
    {
        Task<IssueBookFormResponseDto> LoadIssueForm(string isbn);
        Task<IssueBookResponseDto> IssueBook(IssueBookRequestDto request);

    }
}

using LMS.DTOs;

namespace LMS.Repository
{
    public interface IRequestService
    {
        Task<bool> AddRequest(AddRequestDto request, HttpContext httpContex);
        Task<List<GetRequestDto>> GetRequestList(HttpContext httpContext);
        Task<bool> RemoveRequestList(int id);
    }
}

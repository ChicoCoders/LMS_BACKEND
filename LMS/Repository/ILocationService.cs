using LMS.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Repository
{
    public interface ILocationService
    {
        Task<IActionResult> GetAllLocation(string cupboardname);
        Task<IActionResult> SearchResources(SearchbookcupDto request);
        Task<IActionResult> AddLocation(AddLocationDto location);
    }
}

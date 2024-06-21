using LMS.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Repository
{
    public interface IAuthService
    {

        Task<IActionResult> Login(AuthDto request);
        Task<IActionResult> Refresh(TokenRequest tokenRequest);

        Task<IActionResult> SelectUserType(string userType, HttpContext httpContext);

    }
}

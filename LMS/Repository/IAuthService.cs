using LMS.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Repository
{
    public interface IAuthService
    {

        Task<IActionResult> Login(AuthDto request);
        Task<IActionResult> Refresh(TokenRequest tokenRequest);
        Task<IActionResult> MobileLogin([FromBody] AuthDto request);
        Task<IActionResult> SelectUserType(string userType, HttpContext httpContext);

    }
}

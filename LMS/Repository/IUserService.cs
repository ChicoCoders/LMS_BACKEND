﻿

using LMS.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Repository
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> AddUser(CreateUserRequestDto userdto,HttpContext httpContext);
        Task<User> GetById( string userName);
        Task<bool> DeleteUser(string username);
        Task<AboutUserDto> AboutUser(string username);
        Task<bool> EditUser(EditUserRequestDto edituser, HttpContext httpContext);

        Task<List<UserListDto>> SearchUser(SearchUserDto searchUser);

        Task<bool> ChangePassword( ChangePasswordDto request,HttpContext httpContext);

        Task<AboutUserDto> GetMyData(HttpContext httpContext);
        Task<String> GetEmail(HttpContext httpContext);

        Task<bool> ChangeEmail(string newEmail, HttpContext httpContext);
        Task<bool> SendForgotPasswordEmail(string email);
        Task<bool> ResetPassword(ChangePasswordDto request, HttpContext httpContext);
    }
}

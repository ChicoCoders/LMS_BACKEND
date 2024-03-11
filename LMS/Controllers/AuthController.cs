﻿using LMS.DTOs;
using LMS.Helpers;
using LMS.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _Context;
        private readonly JWTService _jwtService;
        private readonly IUserService _userService;
        public AuthController(DataContext context, JWTService jwt, IUserService userService)
        {
            _Context = context;
            _jwtService = jwt;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthDto request )
        {
            var user = await _Context.Users.FirstOrDefaultAsync(u => u.UserName == request.userName);

            if (user==null)
            {
                return BadRequest("User not found");
            }
            if(user.Password != request.password)
            {
                return BadRequest("Wrong Password");

            }
            var k = new AuthDto
            {
                userName = user.UserName,
                password = user.Password,
            };

            var jwt = _jwtService.Generate(user.UserName);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            }
            );

            return Ok(new{
                message="success"
            });
        }


        [HttpGet("user")]
        public async Task<IActionResult> User()
        {
            try { 
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            string userName = token.Issuer;
            var user = _userService.GetById(userName);

            return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> LogOut()
        {
            Response.Cookies.Delete("jwt");

            return Ok(
                new
                {
                    message = "success"
                }
            );
        }

    }
}
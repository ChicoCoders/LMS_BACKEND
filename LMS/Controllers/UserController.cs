using LMS.DTOs;
using LMS.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class UserController : ControllerBase
    {
        //Create IUserService Field
        private readonly IUserService _userService;

        //Constructor
        public UserController(IUserService userService)
        {
            _userService = userService;  
        }

        [HttpPost]
        public async Task<CreateUserResponseDto> AddUser(CreateUserRequestDto userdto){
            return await _userService.AddUser(userdto);
        }
    }
}

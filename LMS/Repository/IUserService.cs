

using LMS.DTOs;

namespace LMS.Repository
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> AddUser(CreateUserRequestDto userdto);
        Task<User> GetById( string userName); 
    }
}

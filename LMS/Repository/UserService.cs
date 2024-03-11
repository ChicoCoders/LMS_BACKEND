using LMS.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LMS.Repository
{
    public class UserService:IUserService
    {

        //Create _Context Field
        private readonly DataContext _Context;


        //Contructor of the UserService
        public UserService(DataContext Context)
        {
            _Context= Context; 
        }
        

        //Create User Service
        public async Task<CreateUserResponseDto> AddUser(CreateUserRequestDto userdto)
        {

            var AddedBy = await _Context.Users.FirstOrDefaultAsync(u => u.UserName == userdto.AddedById);
            //Pass data from dto to new user object
            var user = new User
            {
                UserName = userdto.Email,
                FName = userdto.FName,
                LName = userdto.LName,
                Email = userdto.Email,
                DOB = userdto.DOB,
                Address = userdto.Address,
                PhoneNumber = userdto.PhoneNumber,
                Password = userdto.Password,
                NIC = userdto.NIC,
                UserType = userdto.UserType,
                AddedById = userdto.AddedById,
                Status="free",
            };
            
            
           
            //Add user object to _Context
            _Context.Users.Add(user);

            
            //Database update
            await _Context.SaveChangesAsync();

            var responsedto = new CreateUserResponseDto
            {
                UserName=user.UserName,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                            };
            

            //Return Creating User
            return responsedto;
        }
        public async Task<User> GetById(string userName)
        {
            return  _Context.Users.FirstOrDefault(u => u.UserName == userName);
        }
    }
}

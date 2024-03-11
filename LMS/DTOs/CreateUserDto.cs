namespace LMS.DTOs
{
    public class CreateUserRequestDto
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string NIC { get; set; }
        public string UserType { get; set; }
        public string AddedById { get; set; }
    }

    public class CreateUserResponseDto
    {
        public string UserName {  get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
      
    }

}

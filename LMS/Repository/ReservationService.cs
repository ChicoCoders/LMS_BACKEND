using LMS.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repository
{
    public class ReservationService : IReservationService
    {

        private readonly DataContext _Context;


        //Contructor of the UserService
        public ReservationService(DataContext Context)
        {
            _Context = Context;
        }


        public async Task<IssueBookFormResponseDto> LoadIssueForm(string isbn)
        {

            var resource = await _Context.Resources.FirstOrDefaultAsync(u => u.ISBN == isbn);

            if (resource == null)  //Check is there any resource
            {
                throw new Exception("ResourceNotFound");
            }
            else //If Resource found past data to port
            {
                var resourcedto=new IssueBookFormResponseDto
                {
                    ISBN=resource.ISBN,
                    URL = resource.ImageURL,
                };
                return resourcedto;
            }
        }
        public async Task<IssueBookResponseDto> IssueBook(IssueBookRequestDto request)
        {

            var resource = await _Context.Resources.FirstOrDefaultAsync(u => u.ISBN == request.ISBN);
            var borrower = await _Context.Users.FirstOrDefaultAsync(u => u.UserName == request.BorrowerID);
            
            //Decrease Quantity of resource by 1
            resource.Quantity = resource.Quantity - 1;
            await _Context.SaveChangesAsync();


            if (resource.Quantity < 0) //If not enough resources
            {
                resource.Quantity = resource.Quantity + 1;
                await _Context.SaveChangesAsync();
                throw new Exception("No of Books not enough");
            }
            else if (borrower.Status == "Loan") //If User in a loan
            {
                resource.Quantity = resource.Quantity + 1;
                await _Context.SaveChangesAsync();
                throw new Exception("User in a loan");
            }
            else  //If User Can borrow the book
            {
                var reservation = new Reservation
                {
                    ReservationNo="123456",
                    BorrowerID = request.BorrowerID,
                    Status = "Borrowed",
                    IssuedByID = request.IssuedID,
                    IssuedDate = "2001",
                    DueDate = request.DueDate,
                    ReturnDate = "dsfsdfs",
                    
                };

                _Context.Reservations.Add(reservation);//Add the Reservation
                borrower.Status = "Loan";
                
                await _Context.SaveChangesAsync();

                var responsefromform = new IssueBookResponseDto //Intialize response dto
                {
                    BorrowerId = reservation.BorrowerID,
                    ISBN = resource.ISBN,
                    ReservationId = reservation.Id,
                };

                return responsefromform; // return response dto to port
            }
        }
    }
}

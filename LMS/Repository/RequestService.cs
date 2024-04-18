using LMS.DTOs;
using LMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static Azure.Core.HttpHeader;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LMS.Repository
{
    public class RequestService:IRequestService
    {
        private readonly DataContext _Context;


        //Contructor of the RequestService
        public RequestService(DataContext Context)
        {
            _Context = Context;
        }
        public async Task<bool> AddRequest(AddRequestDto request)
        {
            var resource = await _Context.Resources.FirstOrDefaultAsync(u => u.ISBN == request.ISBN);
            var borrower = await _Context.Users.FirstOrDefaultAsync(u => u.UserName == request.BorrowerID);

            //Decrease Quantity of resource by 1
            resource.Quantity = resource.Quantity - 1;
            resource.Borrowed = resource.Borrowed + 1;
            await _Context.SaveChangesAsync();


            if (resource.Quantity < 0) //If not enough resources
            {
                resource.Quantity = resource.Quantity + 1;
                resource.Borrowed = resource.Borrowed - 1;
                await _Context.SaveChangesAsync();
                throw new Exception("No of Books not enough");
            }
            else if (borrower.Status == "Loan") //If User in a loan
            {
                resource.Quantity = resource.Quantity + 1;
                resource.Borrowed = resource.Borrowed - 1;//nedd to add request field to db
                await _Context.SaveChangesAsync();
                throw new Exception("User in a loan");
            }
            else  //If User Can borrow the book
            {
                var newrequest = new RequestResource
                {
                    ResourceId= request.ISBN,
                    UserId=request.BorrowerID,
                    Date=new DateOnly(2023,05,01),
                    Time=new TimeOnly(22,11)
                };

                
                

                _Context.Requests.Add(newrequest);//Add the Reservation
                borrower.Status = "Loan";
                await _Context.SaveChangesAsync();

                return true;
            }
        }
        public async Task<List<GetRequestDto>> GetRequestList()
        {
            var allrequest =  _Context.Requests.ToList();
            List<GetRequestDto> requestlist = new List<GetRequestDto>();  
            if (allrequest != null)
            {
                foreach (var r in allrequest)
                {
                    var req = new GetRequestDto
                    {
                        id = r.Id,
                        BorrowerID = r.UserId,
                        ISBN = r.ResourceId,
                        Title = _Context.Resources.FirstOrDefault(e => e.ISBN == r.ResourceId).Title,
                        Date = r.Date
                    };
                    requestlist.Add(req);
                }
            }
            return requestlist;
        }
        public async Task<bool> RemoveRequestList(int id)
        {
            var request = await _Context.Requests.FirstOrDefaultAsync(e => e.Id == id);
            if (request == null)
            {
                throw new Exception("No Request Found");
            }
            else
            {
                var resource = await _Context.Resources.FirstOrDefaultAsync(e => e.ISBN == request.ResourceId);
                var user = await _Context.Users.FirstOrDefaultAsync(e => e.UserName == request.UserId);
                resource.Quantity=resource.Quantity+1;
                resource.Borrowed = resource.Borrowed-1;//need to fixed
                user.Status = "free";
                _Context.Requests.Remove(request);
                 await _Context.SaveChangesAsync();
                 return true;
            }
        }

        public void DeleteExpiredRequests()
        {
            var request = _Context.Requests.FirstOrDefault();
            _Context.Requests.Remove(request);
            _Context.SaveChanges();
        }
    }
}

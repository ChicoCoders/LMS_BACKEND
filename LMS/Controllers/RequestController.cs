using LMS.DTOs;
using LMS.Repository;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cmp;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost("RequestResource")]
        public async Task<bool> AddRequest(AddRequestDto request)
        {
            var x = HttpContext;
            return await _requestService.AddRequest(request,x);
        }


        [HttpPost("DisplayRequest")]
        public async Task<List<GetRequestDto>> GetRequestList()
        {
            var x = HttpContext;
            return await _requestService.GetRequestList(x);
        }
        [HttpDelete("RemoveRequest")]
        public async Task<bool> RemoveRequestList(int id)
        {
            return await _requestService.RemoveRequestList(id);
        }
    }
}

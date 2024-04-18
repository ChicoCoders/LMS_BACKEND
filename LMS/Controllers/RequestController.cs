using LMS.DTOs;
using LMS.Repository;
using Microsoft.AspNetCore.Mvc;

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
            return await _requestService.AddRequest(request);
        }

        [HttpGet("DisplayRequest")]
        public async Task<List<GetRequestDto>> GetRequestList()
        {
            return await _requestService.GetRequestList();
        }
        [HttpGet("RemoveRequest")]
        public async Task<bool> RemoveRequestList(int id)
        {
            return await _requestService.RemoveRequestList(id);
        }
    }
}

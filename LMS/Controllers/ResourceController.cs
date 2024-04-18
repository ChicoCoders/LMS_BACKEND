using LMS.DTOs;
using LMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
             //Create IResourceService Field
             private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpPost("AddResource")]
        public async Task<AddBookResponseDto> AddResource(AddBookRequestDto book)
        {
            return await _resourceService.AddResource(book);
        }

        [HttpPut("EditResource")]
        public async Task<bool> EditResource(AddBookRequestDto book)
        {
            return await _resourceService.EditResource(book);
        }

        [HttpGet("DeleteResource")]
        public async Task<bool> DeleteResource(string isbn)
        {
            return await _resourceService.DeleteResource(isbn);
        }

        [HttpGet("AbouteResource")]
        public async Task<AboutResourceDto> AboutResource(string isbn)
        {
            return await _resourceService.AboutResource(isbn);
        }

        [HttpPost("SearchResource")]
        public async Task<List< ResourceListDto>> SearchResource(string Title)
        {
            return await _resourceService.SearchResource(Title);
        }

        [HttpGet("GetAllResource")]
        public async Task<List<ResourceListDto>> GetAllResource()
        {
            return await _resourceService.GetAllResource();
        }


    }
   
}

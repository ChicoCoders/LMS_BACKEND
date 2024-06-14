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
            var x = HttpContext;
            return await _resourceService.AddResource(book,x);
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

        [HttpPost("AbouteResource")]
        public async Task<AboutResourceDto> AboutResource(string isbn)
        {
            return await _resourceService.AboutResource(isbn);
        }


        [HttpPost("SearchResources")]
        public async Task<List<ResourceListDto>> SearchResources(SearchbookDto searchbookDto)
        {
            return await _resourceService.SearchResources(searchbookDto);
        }


    }
   
}

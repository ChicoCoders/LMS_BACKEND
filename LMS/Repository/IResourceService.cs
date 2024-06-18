using LMS.DTOs;

namespace LMS.Repository
{
    public interface IResourceService
    {
        Task<AddBookResponseDto> AddResource(AddBookRequestDto book,HttpContext httpContext);
        Task<bool> DeleteResource(string isbn);
        Task<List<ResourceListDto>> SearchResources(SearchbookDto searchbookDto);
        Task<bool> EditResource(AddBookRequestDto book);
        Task<AboutResourceDto> AboutResource(string isbn);
        Task WeeklyBookUpdates();
    }
}

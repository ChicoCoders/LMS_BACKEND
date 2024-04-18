using LMS.DTOs;

namespace LMS.Repository
{
    public interface IResourceService
    {
        Task<AddBookResponseDto> AddResource(AddBookRequestDto book);
        Task<bool> DeleteResource(string isbn);
        Task<List<ResourceListDto>> SearchResource(string Title);
        Task<List<ResourceListDto>> GetAllResource();
        Task<bool> EditResource(AddBookRequestDto book);
        Task<AboutResourceDto> AboutResource(string isbn);
    }
}

using Backend.src.DTO;
using Backend.src.Shared;

namespace Backend.src.Service.Impl
{
    public interface ICategoryService
    {
        Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createDto);
        Task<bool> DeleteOneASync(Guid id);
        Task<IEnumerable<CategoryReadDto>> GetAllAsync(GetAllOptions getAllOptions);

        Task<CategoryReadDto> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, CategoryUpdateDto updateDto);
    }
}
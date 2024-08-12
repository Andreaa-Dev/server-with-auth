using Backend.src.DTO;
using Backend.src.Shared;

namespace Backend.src.Service.Impl
{
    public interface IProductService
    {
        Task<ProductReadDto> CreateOneAsync(ProductCreateDto createDto);
        Task<bool> DeleteOneASync(Guid id);
        Task<IEnumerable<ProductReadDto>> GetAllAsync(GetAllOptions getAllOptions);
        Task<ProductReadDto> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, ProductUpdateDto updateDto);
    }
}
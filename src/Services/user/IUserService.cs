using Backend.src.DTO;
using Backend.src.Entity;
using Backend.src.Shared;

namespace Backend.src.Service.Impl
{
    public interface IUserService
    {
        Task<UserReadDto> CreateOneAsync(UserCreateDto createDto);
        Task<UserReadDto> CreateAdminAsync(UserCreateDto createDto);
        Task<string> SignInAsync(UserSignInDto userSignInDto);
        Task<bool> DeleteOneASync(Guid id);
        Task<IEnumerable<UserReadDto>> GetAllAsync(GetAllOptions getAllOptions);
        Task<UserReadDto> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, UserUpdateDto updateDto);
        Task<bool> UpdateAdminAsync(Guid id);
        Task<UserReadDto> FindByEmailAsync(string email);
    }
}
using Backend.src.Abstraction;
using Backend.src.DTO;
using Backend.src.Entity;

namespace Backend.src.Abstraction
{
    public interface IUserRepo : IBaseRepo<User>
    {
        Task<User?> FindByEmailAsync(string email);


    }
}
using System;
using Backend.src.Entity;
using Backend.src.Shared;

namespace Backend.src.Abstraction
{

    public interface IBaseRepo<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(GetAllOptions getAllOptions);
        Task<T?> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(T updateObject);
        Task<bool> DeleteOneAsync(T deleteObject);
        Task<T> CreateOneAsync(T createObject);
    }

}
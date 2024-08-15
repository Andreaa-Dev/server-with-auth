using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.DTO;
using Backend.src.Shared;

namespace Backend.src.Service
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetailReadDto>> GetAllAsync(GetAllOptions getAllOptions);
        Task<OrderDetailReadDto> GetByIdAsync(Guid id);
    }
}
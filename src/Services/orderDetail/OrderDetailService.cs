using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.src.Abstraction;
using Backend.src.DTO;
using Backend.src.Entity;
using Backend.src.Shared;

namespace Backend.src.Service.Impl
{
    public class OrderDetailService : IOrderDetailService
    {
        protected readonly IOrderDetailRepo _orderDetailRepo;
        protected readonly IMapper _mapper;
        public OrderDetailService(IOrderDetailRepo orderDetailRepo, IMapper mapper)
        {
            _orderDetailRepo = orderDetailRepo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OrderDetailReadDto>> GetAllAsync(GetAllOptions getAllOptions)
        {
            var orderDetailList = await _orderDetailRepo.GetAllAsync(getAllOptions);
            return _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailReadDto>>(orderDetailList);
        }

        public Task<OrderDetailReadDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using Backend.src.Shared;
using Backend.src.Entity;
using Backend.src.Database;
using Backend.src.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.Repository
{

    public class OrderDetailRepo : IBaseRepo<OrderDetail>
    {
        protected readonly DbSet<OrderDetail> _orderDetail;
        protected readonly DatabaseContext _databaseContext;

        public OrderDetailRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _orderDetail = _databaseContext.Set<OrderDetail>();
        }
        public async Task<OrderDetail> CreateOneAsync(OrderDetail createObject)
        {
            await _orderDetail.AddAsync(createObject);
            await _databaseContext.SaveChangesAsync();
            return createObject;
        }

        public async Task<bool> DeleteOneAsync(OrderDetail deleteObject)
        {
            _orderDetail.Remove(deleteObject);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllAsync(GetAllOptions getAllOptions)
        {
            return await _orderDetail.AsNoTracking()
            //.Include(o => o.Product).ThenInclude(p => p.Category)
            .Skip(getAllOptions.Offset)
            .Take(getAllOptions.Limit)
            .ToListAsync();
        }

        public async Task<OrderDetail?> GetByIdAsync(Guid id)
        {
            return await _orderDetail.FindAsync(id);
        }

        public async Task<bool> UpdateOneAsync(OrderDetail updateObject)
        {
            _orderDetail.Update(updateObject);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
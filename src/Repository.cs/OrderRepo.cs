using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Backend.src.Abstraction;
using Backend.src.Database;
using Backend.src.Entity;
using Backend.src.Shared;


namespace Backend.src.Repository
{
    public class OrderRepo : IOrderRepo
    {
        protected readonly DbSet<Order> _orders;
        protected readonly DbSet<Product> _products;
        protected readonly DbSet<OrderDetail> _orderDetails;
        protected readonly DatabaseContext _databaseContext;
        public OrderRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _orders = _databaseContext.Set<Order>();
            _orderDetails = _databaseContext.Set<OrderDetail>();
            _products = _databaseContext.Set<Product>();
        }

        public async Task<Order> CreateOneAsync(Order createObject)
        {
            await _orders.AddAsync(createObject);
            await _databaseContext.SaveChangesAsync();

            // order detail is array/collection
            await _orders.Entry(createObject).Collection(o => o.OrderDetails).LoadAsync();
            // Optionally, load related Products for each OrderDetail
            foreach (var detail in createObject.OrderDetails)
            {
                await _databaseContext.Entry(detail).Reference(od => od.Product).LoadAsync();
            }
            return createObject;

        }

        public async Task<bool> DeleteOneAsync(Order deleteObject)
        {
            _orders.Remove(deleteObject);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(GetAllOptions getAllOptions)
        {
            return await _orders.AsNoTracking()
            .Skip(getAllOptions.Offset)
            .Take(getAllOptions.Limit)
            .ToArrayAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _orders.FindAsync(id);
        }

        public async Task<bool> UpdateOneAsync(Order updateObject)
        {
            _orders.Update(updateObject);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
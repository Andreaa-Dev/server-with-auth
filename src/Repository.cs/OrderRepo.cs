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
    public class OrderRepo : IBaseRepo<Order>
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

        // ???
        public async Task<Order> CreateOneAsync(Order createObject)
        {
            using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
            {
                try
                {

                    foreach (OrderDetail detail in createObject.OrderDetails)
                    {
                        var foundProduct = await _products.FindAsync(detail.ProductId);
                        if (foundProduct is null)
                        {
                            throw CustomException.NotFound($"ProductId {detail.ProductId} is not found");
                        }
                        else if (detail.Quantity > foundProduct.Inventory)
                        {
                            throw CustomException.BadRequest("Do not have enough quantity");
                        }
                        else
                        {
                            foundProduct.Inventory -= detail.Quantity;
                            await _databaseContext.SaveChangesAsync();
                            await _orderDetails.AddAsync(detail);
                        }

                        await _orders.AddAsync(createObject);
                        await _databaseContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    return createObject;
                }
                catch (CustomException e)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
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
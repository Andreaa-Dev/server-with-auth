using System;
using Backend.src.Shared;
using Backend.src.Entity;
using Backend.src.Database;
using Backend.src.Abstraction;
using Microsoft.EntityFrameworkCore;


public class ProductRepo : IBaseRepo<Product>
{
    protected readonly DbSet<Product> _product;
    protected readonly DatabaseContext _databaseContext;

    public ProductRepo(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _product = databaseContext.Set<Product>();

    }
    public async Task<Product> CreateOneAsync(Product createObject)
    {
        await _product.AddAsync(createObject);
        await _databaseContext.SaveChangesAsync();
        // Load the Category explicitly
        await _product.Entry(createObject).Reference(p => p.Category).LoadAsync();

        return createObject;
    }

    public async Task<bool> DeleteOneAsync(Product deleteObject)
    {
        _product.Remove(deleteObject);
        await _databaseContext.SaveChangesAsync();
        return true;
    }


    public async Task<IEnumerable<Product>> GetAllAsync(GetAllOptions getAllOptions)
    {
        return await _product.Skip(getAllOptions.Offset).Take(getAllOptions.Limit).Include(p => p.Category).ToArrayAsync();
    }


    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _product.FindAsync(id);
    }

    public async Task<bool> UpdateOneAsync(Product updateObject)
    {
        _product.Update(updateObject);
        await _databaseContext.SaveChangesAsync();
        return true;
    }
}
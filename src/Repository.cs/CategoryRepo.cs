using System;
using Backend.src.Shared;
using Backend.src.Entity;
using Backend.src.Database;
using Backend.src.Abstraction;
using Microsoft.EntityFrameworkCore;


public class CategoryRepo : IBaseRepo<Category>
{
    protected readonly DbSet<Category> _category;
    protected readonly DatabaseContext _databaseContext;

    public CategoryRepo(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _category = databaseContext.Set<Category>();

    }
    // task: return type of async 
    public async Task<Category> CreateOneAsync(Category createObject)
    {
        await _category.AddAsync(createObject);
        await _databaseContext.SaveChangesAsync();
        return createObject;
    }

    public async Task<bool> DeleteOneAsync(Category deleteObject)
    {
        _category.Remove(deleteObject);
        await _databaseContext.SaveChangesAsync();
        return true;
    }


    public async Task<IEnumerable<Category>> GetAllAsync(GetAllOptions getAllOptions)
    {
        // return await _category.ToListAsync();
        return await _category.Skip(getAllOptions.Offset).Take(getAllOptions.Limit).ToArrayAsync();
    }


    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _category.FindAsync(id);
    }

    public async Task<bool> UpdateOneAsync(Category updateObject)
    {
        _category.Update(updateObject);
        await _databaseContext.SaveChangesAsync();
        return true;
    }
}
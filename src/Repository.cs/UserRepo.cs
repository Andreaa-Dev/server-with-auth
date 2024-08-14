using System;
using Backend.src.Shared;
using Backend.src.Entity;
using Backend.src.Database;
using Backend.src.Abstraction;
using Microsoft.EntityFrameworkCore;
using Backend.src.DTO;


public class UserRepo : IUserRepo
{
    protected readonly DbSet<User> _user;
    protected readonly DatabaseContext _databaseContext;

    public UserRepo(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _user = databaseContext.Set<User>();

    }

    // register
    public async Task<User> CreateOneAsync(User createObject)
    {
        await _user.AddAsync(createObject);
        await _databaseContext.SaveChangesAsync();
        return createObject;
    }

    public async Task<bool> DeleteOneAsync(User deleteObject)
    {
        _user.Remove(deleteObject);
        await _databaseContext.SaveChangesAsync();
        return true;
    }


    public async Task<IEnumerable<User>> GetAllAsync(GetAllOptions getAllOptions)
    {
        return await _user.Skip(getAllOptions.Offset).Take(getAllOptions.Limit).ToArrayAsync();
    }


    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _user.FindAsync(id);
    }

    public async Task<bool> UpdateOneAsync(User updateObject)
    {
        _user.Update(updateObject);
        await _databaseContext.SaveChangesAsync();
        return true;
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _user.FirstOrDefaultAsync(u => u.Email == email);
    }



}


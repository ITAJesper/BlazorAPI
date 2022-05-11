using BlazorAPI.Server.Data;
using BlazorAPI.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAPI.Server.Services;

public class UserService : IUserService
{
    private DataContext db { get; }

    public UserService(DataContext db)
    {
        this.db = db;
    }
    
    public async Task<IResult> CreateUserAsync(User userToCreate)
    {
        
        db.Users.Add(userToCreate);
        await db.SaveChangesAsync();
        return Results.Created($"/posts/{userToCreate.UserId}", userToCreate);
    }

    public Task<IResult> GetByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllUserAsync()
    {
        List<User> allUsers = await db.Users
            .ToListAsync();

        return allUsers;
    }

    public Task<IResult> UpdateUserAsync(int userId, User updatedUser)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> DelteUserAsync(int userId)
    {
        User? userToDelete = await db.Users.FindAsync(userId);

        if (userToDelete != null)
        {
            db.Users.Remove(userToDelete);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }
        else
        {
            return Results.NotFound();
        }
    }
}
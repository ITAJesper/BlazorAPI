using BlazorAPI.Shared.Models;

namespace BlazorAPI.Server.Services;

public interface IUserService
{
    public Task<IResult> CreateUserAsync(User userToCreate);

    public Task<IResult> GetByIdAsync(int userId);

    public Task<IEnumerable<User>> GetAllUserAsync();
    
    public Task<IResult> UpdateUserAsync(int userId, User updatedUser);

    public Task<IResult> DelteUserAsync(int userId);
}
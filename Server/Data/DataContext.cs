using BlazorAPI.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAPI.Server.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    
    public DbSet<Topic> Topics => Set<Topic>();
    
    public DbSet<Question> Questions => Set<Question>();
    
    public DbSet<Comment> Comments => Set<Comment>();

    public DataContext (DbContextOptions<DataContext> options)
        : base(options)
    {

    }
}
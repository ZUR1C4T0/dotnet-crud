using dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Context;

public class DbAppContext : DbContext
{
    public DbAppContext(DbContextOptions<DbAppContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
}

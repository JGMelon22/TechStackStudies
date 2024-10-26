using Microsoft.EntityFrameworkCore;
using TechStackStudies.Models;

namespace TechStackStudies.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Technology> Technologies { get; set; }
}

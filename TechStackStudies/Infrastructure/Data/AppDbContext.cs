using Microsoft.EntityFrameworkCore;
using TechStackStudies.Infrastructure.Configuration;
using TechStackStudies.Models;

namespace TechStackStudies.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Technology> Technologies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TechnologyConfiguration());
    }
}

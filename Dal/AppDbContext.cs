using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Complaint> Complaints { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<UserComplaint> UserComplaints { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
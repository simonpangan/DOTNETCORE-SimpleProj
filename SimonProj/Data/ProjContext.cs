using Microsoft.EntityFrameworkCore;
using SimonProj.Models;

namespace SimonProj.Data;

public class ProjContext : DbContext
{
    public ProjContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Teacher>().ToTable("Teacher");
    }
}
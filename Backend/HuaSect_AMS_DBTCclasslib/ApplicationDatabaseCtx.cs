using Microsoft.EntityFrameworkCore;
using HuaSect_AMS_DBTCclasslib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HuaSect_AMS_DBTCclasslib.DbCtx;

public class ApplicationDatabaseCtx : IdentityDbContext<IdentityUser>
{
    public DbSet<Student> Student { get; set; }
    public DbSet<Teacher> Teacher { get; set; }
    public DbSet<Course> Course { get; set; }
    public DbSet<Attendance> Attendance { get; set; }
    public ApplicationDatabaseCtx(DbContextOptions<ApplicationDatabaseCtx> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Optional: customize table names for PostgreSQL conventions
        builder.Entity<IdentityUser>(entity => entity.ToTable(name: "Users"));
        builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
    }
}

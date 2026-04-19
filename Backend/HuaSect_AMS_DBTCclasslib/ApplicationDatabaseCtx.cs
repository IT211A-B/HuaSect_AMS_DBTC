using Microsoft.EntityFrameworkCore;
using HuaSect_AMS_DBTCclasslib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HuaSect_AMS_DBTCclasslib.Interfaces;

namespace HuaSect_AMS_DBTCclasslib.DbCtx;

public class ApplicationDatabaseCtx : IdentityDbContext<IdentityUser>
{
    private readonly IEncryptionService _encryptionService;

    public DbSet<StudentProfile> StudentProfile { get; set; }
    public DbSet<TeacherProfile> TeacherProfile { get; set; }
    public DbSet<Course> Course { get; set; }
    public DbSet<Attendance> Attendance { get; set; }
    public ApplicationDatabaseCtx(DbContextOptions<ApplicationDatabaseCtx> options, IEncryptionService encryptionService) : base(options)
    {
        _encryptionService = encryptionService;
    }

    public ApplicationDatabaseCtx() { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityUser>(entity => entity.ToTable(name: "Users"));
        builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
        builder.AddGlobalStringEncryption(_encryptionService);
    }
}

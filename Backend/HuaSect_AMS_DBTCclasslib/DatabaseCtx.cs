using Microsoft.EntityFrameworkCore;
using HuaSect_AMS_DBTCclasslib.Models;

namespace HuaSect_AMS_DBTCclasslib.DbCtx;

public class DatabaseCtx: DbContext
{
    public DbSet<Student> Student { get; set; }
    public DbSet<Teacher> Teacher { get; set; }
    public DbSet<Course> Course { get; set; }
    public DatabaseCtx(DbContextOptions<DatabaseCtx> options): base(options) {}
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class EComDbContext : DbContext
{
    private readonly string _connectionString;
    private readonly string _migrationAssembly;

    public EComDbContext(string connectionString,
        string migrationAssembly)
    {
        _connectionString = connectionString;
        _migrationAssembly = migrationAssembly;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString,
                x => x.MigrationsAssembly(_migrationAssembly));
        }
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<IdempotencyKeyRecord> IdempotencyKeyRecords { get; set; }
    public DbSet<QueuedEmail> QueuedEmails { get; set; }
}

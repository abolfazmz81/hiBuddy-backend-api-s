using IAM.Domain;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.UserRepository;

public class SQLServerContext : DbContext
{
    public SQLServerContext() {}
    public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the composite primary key
        modelBuilder.Entity<Domain.User>()
            .HasKey(ul => ul.user_id);
    }

    public virtual DbSet<User> Hibuddy_user { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"data source=localhost, 1434;initial catalog=HiBuddy;user id=sa;password=Scotflop1381;TrustServerCertificate=True");
    }
}
using Microsoft.EntityFrameworkCore;
using User.Domain;

namespace User.Infrastructure.UserRepository;

public class SQLServerContext : DbContext
{
    public SQLServerContext() {}
    public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options)
    {
    }

    public virtual DbSet<Domain.User> Hibuddy_user { get; set; }
    public virtual DbSet<User_location> user_locations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the composite primary key
        modelBuilder.Entity<User_location>()
            .HasKey(ul => new { ul.user_id, ul.location_id });
        modelBuilder.Entity<Domain.User>()
            .HasKey(ul => ul.user_id);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"data source=localhost, 1434;initial catalog=HiBuddy;user id=sa;password=Scotflop1381;TrustServerCertificate=True");
    }
}
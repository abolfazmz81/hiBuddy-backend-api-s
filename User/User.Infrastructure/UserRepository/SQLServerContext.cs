using Microsoft.EntityFrameworkCore;

namespace User.Infrastructure.UserRepository;

public class SQLServerContext : DbContext
{
    public SQLServerContext() {}
    public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options)
    {
    }

    public virtual DbSet<Domain.User> Hibuddy_user { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"data source=localhost, 1434;initial catalog=HiBuddy;user id=sa;password=Scotflop1381;TrustServerCertificate=True");
    }
}
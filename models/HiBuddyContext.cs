using Microsoft.EntityFrameworkCore;
using hiBuddy.Models;

namespace hiBuddy.Data
{
    public class HiBuddyContext : DbContext
    {
        public HiBuddyContext(DbContextOptions<HiBuddyContext> options) : base(options)
        {
        }

        public DbSet<UserManagement> Hibuddy_user { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=DESKTOP-QN1UJLM;initial catalog=HiBuddy;trusted_connection=true;TrustServerCertificate=True");
        }
    }
}
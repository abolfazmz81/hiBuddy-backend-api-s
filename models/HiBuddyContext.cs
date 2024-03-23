using hiBuddy.models;
using Microsoft.EntityFrameworkCore;
using hiBuddy.Models;

namespace hiBuddy.Data
{
    public class HiBuddyContext : DbContext
    {
        public HiBuddyContext() {}
        public HiBuddyContext(DbContextOptions<HiBuddyContext> options) : base(options)
        {
        }

        public virtual DbSet<UserManagement> Hibuddy_user { get; set; }
        public virtual DbSet<locations> locations { get; set; }
        public virtual DbSet<userloc> user_locations{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=localhost, 1434;initial catalog=HiBuddy;user id=sa;password=Scotflop1381;TrustServerCertificate=True");
        }
    }
}
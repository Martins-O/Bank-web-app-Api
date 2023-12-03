using BankAppWebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankAppWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountUser>()
            .Property(a => a.Balance)
            .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<AccountUser> AccountUsers { get; set; }
    }
}

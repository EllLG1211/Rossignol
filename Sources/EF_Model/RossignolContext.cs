using EF_Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_Model
{
    public class RossignolContext : DbContext
    {
        public DbSet<ProprietaryEntryEntity>? ProprietaryEntriesSet { get; set; }
        public DbSet<LocalUserEntity>? LocalUserSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            // The password shall be an env variable
            => optionsBuilder.UseSqlite($"Data Source=Rossignol.bd;Password=password");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalUserEntity>().HasKey(n => n.Uid);

            modelBuilder.Entity<ProprietaryEntryEntity>().HasKey(n => n.Uid);
        }
    }
}
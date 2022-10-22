using EF_Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_Model
{
    public class RossignolContext : DbContext
    {
        public DbSet<LocalUserEntity> LocalUserSet { get; set; }
        public DbSet<EntryEntity> EncryptedEntriesSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=Rossignol.bd");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntryEntity>().HasKey(n => n.Uid);
            modelBuilder.Entity<LocalUserEntity>().HasKey(n => n.Uid);
            base.OnModelCreating(modelBuilder);
        }
    }
}
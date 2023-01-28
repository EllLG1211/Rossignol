using EF_Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_Model
{
    public class RossignolContextLocal : DbContext
    {
        public DbSet<LocalUserEntity> LocalUsers { get; set; }
        public DbSet<EntryEntity> EntriesSet { get; set; }

        public RossignolContextLocal()
        {
            Database.EnsureCreated();
        }

        public RossignolContextLocal(DbContextOptions<RossignolContextLocal> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source=LocalRossignol.bd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntryEntity>().HasKey(n => n.Uid);
            modelBuilder.Entity<ConnectedUserEntity>().HasBaseType(typeof(LocalUserEntity));    //Entity type hierarchy mapping
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ConnectedUserEntity>().HasKey(n => n.Uid);
        }
    }
}
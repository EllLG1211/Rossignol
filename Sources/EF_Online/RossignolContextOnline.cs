using EF_Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_Online
{
    public class RossignolContextOnline : DbContext
    {
        public DbSet<ConnectedUserEntity> OnlinesUsers { get; set; }
        public DbSet<LocalUserEntity> ReferencedUsers { get; set; }
        public DbSet<EntryEntity> EntriesSet { get; set; }

        public RossignolContextOnline()
        {
            Database.EnsureCreated();
        }

        public RossignolContextOnline(DbContextOptions<RossignolContextOnline> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source=OnlineRossignol.bd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntryEntity>().HasKey(n => n.Uid);
            modelBuilder.Entity<LocalUserEntity>().HasKey(n => n.Uid);
            //modelBuilder.Entity<ConnectedUserEntity>().HasKey(n => n.Uid);
            modelBuilder.Entity<ConnectedUserEntity>().HasBaseType(typeof(LocalUserEntity));    //Entity type hierarchy mapping
            base.OnModelCreating(modelBuilder);
        }
    }
}
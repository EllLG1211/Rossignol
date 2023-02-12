using EF_Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
            //Database.Migrate();
        }

        public RossignolContextOnline(DbContextOptions<RossignolContextOnline> options) : base(options)
        {
            Database.EnsureCreated();
            //Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source=OnlineRossignol.bd");
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntryEntity>().HasKey(n => n.Uid);
            //modelBuilder.Entity<EntryEntity>().HasKey(n => n.SharedWith);
            modelBuilder.Entity<LocalUserEntity>().HasKey(n => n.Uid);
            //modelBuilder.Entity<ConnectedUserEntity>().HasKey(n => n.Uid);
            modelBuilder.Entity<ConnectedUserEntity>().HasBaseType(typeof(LocalUserEntity));    //Entity type hierarchy mapping
            base.OnModelCreating(modelBuilder);
        }
    }
}
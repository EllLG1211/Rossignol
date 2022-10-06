using EF_Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_Model
{
    public class RossignolContext : DbContext
    {
        public DbSet<ProprietaryEntryEntity> ProprietaryEntries { get; set; }
        public DbSet<LocalUserEntity> LocalUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source=Rossignol.bd");
    }
}
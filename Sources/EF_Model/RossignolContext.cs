using EF_Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_Model
{
    public class RossignolContext : DbContext
    {
        public DbSet<ProprietaryEntryEntity> ProprietaryEntries { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=Rossignol.bd");
    }
}
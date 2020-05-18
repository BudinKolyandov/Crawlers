using Microsoft.EntityFrameworkCore;
using NewEggCrawler.Data.Models;

namespace NewEggCrawler.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(Config.ConnectionString);

        public DbSet<Cpu> Cpus { get; set; }

        public DbSet<Memory> Memories { get; set; }

        public DbSet<Motherboard> Motherboards { get; set; }

        public DbSet<VideoCard> VideoCards { get; set; }

        public DbSet<Case> Cases { get; set; }

        public DbSet<AirCooler> AirCoolers { get; set; }

        public DbSet<WaterCooler> WaterCoolers { get; set; }
    }
}

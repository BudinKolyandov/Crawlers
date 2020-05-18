namespace Database
{
    using Database.Models;
    using Microsoft.EntityFrameworkCore;

    public class PcDatabaseContext : DbContext
    {
        public PcDatabaseContext(DbContextOptions<PcDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Cpu> Cpus { get; set; }

        public DbSet<Memory> Memories { get; set; }
    }
}

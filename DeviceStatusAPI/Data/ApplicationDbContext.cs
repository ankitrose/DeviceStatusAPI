using Microsoft.EntityFrameworkCore;
using DeviceStatusAPI.Models;

namespace DeviceStatusAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceHistory> DeviceHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasIndex(d => d.DeviceName)
                .IsUnique();

            modelBuilder.Entity<DeviceHistory>()
                .HasKey(dh => dh.HistoryID);
        }
    }
}

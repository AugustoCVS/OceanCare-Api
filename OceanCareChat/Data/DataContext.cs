using Microsoft.EntityFrameworkCore;
using OceanCareChat.Models;

namespace OceanCareChat.Data
{
    public class DataContext : DbContext
    {


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<OceanUser> OceanUser { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<Events> Events { get; set; }

        public DbSet<UserEvent> UserEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OceanUser>()
                .HasKey(u => u.Id);

            builder.Entity<UserEvent>()
                .HasKey(ue => new { ue.OceanUserId, ue.EventId });

            builder.Entity<UserEvent>()
                .HasOne(ue => ue.OceanUser)
                .WithMany(ou => ou.UserEvents)
                .HasForeignKey(ue => ue.OceanUserId);

            builder.Entity<UserEvent>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.UserEvents)
                .HasForeignKey(ue => ue.EventId);

        }
    }
}


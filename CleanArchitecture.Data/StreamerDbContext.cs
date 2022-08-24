
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure
{
    public class StreamerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=StreamerDb;INtegrated Security=true")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Streamer>()
                .HasMany(m => m.Videos)
                .WithOne(m => m.Streamer)
                .HasForeignKey(m => m.StreamerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Video>()
                .HasMany(p => p.Actores)
                .WithMany(p => p.Videos)
                .UsingEntity<VideoActor>(pt => pt.HasKey(e => new { e.ActorId, e.VideoId }));
        }

        public DbSet<Streamer>? Streamers{ get; set; }
        public DbSet<Video>? Videos { get; set; }
        public DbSet<Actor>? Actores { get; set; }
        public DbSet<Director>? Directores { get; set; }
        public DbSet<VideoActor>? VideosActores { get; set; }
    }
}

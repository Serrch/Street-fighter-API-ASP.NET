using Microsoft.EntityFrameworkCore;
using SF_API.Models;

namespace SF_API.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Fighter> Fighters { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<FighterVersion> FighterVersions { get; set; }
        public DbSet<FighterMove> FighterMoves { get; set; }
        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FighterVersion>()
                .HasOne(fv => fv.Fighter)
                .WithMany(f => f.FighterVersions)
                .HasForeignKey(fv => fv.IdFighter)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FighterVersion>()
                .HasOne(fv => fv.Game)
                .WithMany(g => g.FighterVersions)
                .HasForeignKey(fv => fv.IdGame)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FighterMove>()
                .HasOne(fm => fm.FighterVersion)
                .WithMany(fv => fv.FighterMoves)
                .HasForeignKey(fm => fm.IdFighterVersion)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }



    }
}

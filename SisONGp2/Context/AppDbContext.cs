using Microsoft.EntityFrameworkCore;
using SisONGp2.Model;

namespace SisONGp2.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Doador> Doadores { get; set; }
        public DbSet<Doacao> Doacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doacao>()
                .HasOne<Doador>()
                .WithMany()
                .HasForeignKey(d => d.DoadorId);
        }
    }
}

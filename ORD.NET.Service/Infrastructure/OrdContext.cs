using Microsoft.EntityFrameworkCore;
using ORD.NET.Model.Tables;

namespace ORD.NET
{
    public class OrdContext : DbContext
    {
        public OrdContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DizionarioPiatti>()
                .HasKey(c => new { c.TipoPiatto, c.Parola });

            modelBuilder.Entity<LogMail>()
                .HasKey(c => new { c.Zeppelin, c.DataInvio });

            modelBuilder.Entity<Ordinazioni>()
                .HasKey(c => new { c.Data, c.Utente });

            modelBuilder.Entity<Menu>()
                .HasKey(c => new { c.IDZeppelin, c.Progressivo });

            modelBuilder.Entity<RelTipiPiattiZeppelin>()
                .HasKey(c => new { c.Zeppelin, c.TipoPiatto });

            modelBuilder.Entity<RelGruppiUtenti>()
                .HasKey(c => new { c.Gruppo, c.Utente });

            modelBuilder.Entity<Gruppi>()
                .HasKey(c => new { c.Id });
        }

        public DbSet<Ordinazioni> Orders { get; set; }
        public DbSet<Utenti> Users { get; set; }
        public DbSet<TipoPiatti> DishType { get; set; }
        public DbSet<Zeppelin> Zeppelin { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<LogMail> LogMail { get; set; }
        public DbSet<RelTipiPiattiZeppelin> RelTipiPiattiZeppelin { get; set; }
        public DbSet<DizionarioPiatti> DizionarioPiatti { get; set; }
        public DbSet<Gruppi> Gruppi { get; set; }
        public DbSet<RelGruppiUtenti> RelGruppiUtenti { get; set; }
    }
}

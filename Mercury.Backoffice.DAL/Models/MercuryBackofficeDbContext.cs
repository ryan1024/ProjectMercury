namespace Mercury.Backoffice.DAL.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MercuryBackofficeDbContext : DbContext
    {
        public MercuryBackofficeDbContext()
            : base("name=MercuryBackofficeDbContext")
        {
        }

        public virtual DbSet<AspNetUsersExtension> AspNetUsersExtension { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuItem> MenuItem { get; set; }
        public virtual DbSet<RaceEvent> RaceEvent { get; set; }
        public virtual DbSet<RaceEventType> RaceEventType { get; set; }
        public virtual DbSet<RaceResult> RaceResult { get; set; }
        public virtual DbSet<UserRaceResult> UserRaceResult { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RaceEvent>()
                .Property(e => e.Latitude)
                .HasPrecision(9, 6);

            modelBuilder.Entity<RaceEvent>()
                .Property(e => e.Longitude)
                .HasPrecision(9, 6);

            modelBuilder.Entity<RaceResult>()
                .Property(e => e.Nationality)
                .IsFixedLength();
        }
    }
}

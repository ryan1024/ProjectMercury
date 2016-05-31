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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

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

        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<UserRoleMenu> UserRoleMenus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoleMenu>()
                .Property(e => e.AccessLevel)
                .IsUnicode(false);
        }
    }
}

namespace Mercury.Backoffice.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            MenuItem = new HashSet<MenuItem>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MenuId { get; set; }

        [StringLength(100)]
        public string MenuName { get; set; }

        public bool? IsClickable { get; set; }

        [StringLength(100)]
        public string PageCode { get; set; }

        [StringLength(100)]
        public string Action { get; set; }

        [StringLength(100)]
        public string Controller { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(100)]
        public string ImagePath { get; set; }

        [StringLength(50)]
        public string Target { get; set; }

        [StringLength(200)]
        public string Roles { get; set; }

        public int? SequenceNo { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuItem> MenuItem { get; set; }
    }
}

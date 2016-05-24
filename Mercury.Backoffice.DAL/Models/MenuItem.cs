namespace Mercury.Backoffice.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuItem")]
    public partial class MenuItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MenuItemId { get; set; }

        public int? MenuId { get; set; }

        [StringLength(100)]
        public string MenuItemName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Action { get; set; }

        [StringLength(100)]
        public string Controller { get; set; }

        [StringLength(100)]
        public string ImagePath { get; set; }

        [StringLength(50)]
        public string Target { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual Menu Menu { get; set; }
    }
}

namespace Mercury.Backoffice.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RaceEventType")]
    public partial class RaceEventType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RaceEventType()
        {
            RaceEvent = new HashSet<RaceEvent>();
        }

        [Key]
        public int EventTypeId { get; set; }

        [StringLength(100)]
        public string EventTypeName { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RaceEvent> RaceEvent { get; set; }
    }
}

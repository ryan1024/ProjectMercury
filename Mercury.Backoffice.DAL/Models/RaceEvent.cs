namespace Mercury.Backoffice.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RaceEvent")]
    public partial class RaceEvent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RaceEvent()
        {
            RaceResult = new HashSet<RaceResult>();
        }

        [Key]
        public string EventId { get; set; }

        [StringLength(100)]
        public string EventName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? EventTypeId { get; set; }

        public bool? IsCharity { get; set; }

        public bool? IsTimingChip { get; set; }

        public DateTime? EventStartOn { get; set; }

        public DateTime? EventEndOn { get; set; }

        [StringLength(500)]
        public string EventVenue { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        [StringLength(100)]
        public string EventOrganiser { get; set; }

        [StringLength(100)]
        public string FacebookURL { get; set; }

        [StringLength(100)]
        public string TwitterURL { get; set; }

        [StringLength(100)]
        public string InstagramURL { get; set; }

        [StringLength(100)]
        public string GooglePlusURL { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual RaceEventType RaceEventType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RaceResult> RaceResult { get; set; }
    }
}

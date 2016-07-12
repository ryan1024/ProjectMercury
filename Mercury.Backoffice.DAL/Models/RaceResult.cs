namespace Mercury.Backoffice.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RaceResult")]
    public partial class RaceResult
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RaceResult()
        {
            UserRaceResult = new HashSet<UserRaceResult>();
        }

        [Key]
        public string ResultId { get; set; }

        [StringLength(128)]
        public string EventId { get; set; }

        [StringLength(100)]
        public string BIB { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string CategoryDesc { get; set; }

        [StringLength(10)]
        public string Nationality { get; set; }

        public int? RankingByCategory { get; set; }

        public TimeSpan? GunTime { get; set; }

        public bool? IsClaimed { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual RaceEvent RaceEvent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRaceResult> UserRaceResult { get; set; }
    }
}

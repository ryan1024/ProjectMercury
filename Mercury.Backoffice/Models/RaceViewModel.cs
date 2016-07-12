using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Mercury.Backoffice.Models
{
    public class RaceEventViewModel
    {
        [Key]
        public string EventId { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Display(Name = "Event Type")]
        public int? EventTypeId { get; set; }

        [Display(Name = "Charity")]
        public bool? IsCharity { get; set; }

        [Display(Name = "Timing Chip")]
        public bool? IsTimingChip { get; set; }

        [Display(Name = "Event Start On")]
        public DateTime? EventStartOn { get; set; }

        [Display(Name = "Event End On")]
        public DateTime? EventEndOn { get; set; }

        [StringLength(500)]
        [Display(Name = "Event Venue")]
        public string EventVenue { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        [StringLength(100)]
        [Display(Name = "Event Organiser")]
        public string EventOrganiser { get; set; }

        [StringLength(100)]
        [Display(Name = "Facebook URL")]
        public string FacebookURL { get; set; }

        [StringLength(100)]
        [Display(Name = "Twitter URL")]
        public string TwitterURL { get; set; }

        [StringLength(100)]
        [Display(Name = "Instagram URL")]
        public string InstagramURL { get; set; }

        [StringLength(100)]
        [Display(Name = "Google+ URL")]
        public string GooglePlusURL { get; set; }

        [Display(Name = "Active")]
        public bool? IsActive { get; set; }

        [StringLength(50)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Updated On")]
        public DateTime? UpdatedOn { get; set; }

        public virtual RaceEventTypeViewModel RaceEventType { get; set; }

        //Foreign Key
        public string EventTypeName { get; set; }
    }

    public class RaceEventTypeViewModel
    {
        [Key]
        public int EventTypeId { get; set; }

        [StringLength(100)]
        [Display(Name = "Event Type Name")]
        public string EventTypeName { get; set; }

        [StringLength(100)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Is Active")]
        public bool? IsActive { get; set; }

        [StringLength(50)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Updated On")]
        public DateTime? UpdatedOn { get; set; }
    }

    public class RaceResultViewModel
    {
        [Key]
        public string ResultId { get; set; }

        [StringLength(128)]
        public string EventId { get; set; }

        [StringLength(100)]
        public string BIB { get; set; }

        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [StringLength(100)]
        [Display(Name = "Category")]
        public string CategoryDesc { get; set; }

        [StringLength(10)]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "Ranking by Category")]
        public int? RankingByCategory { get; set; }

        [Display(Name = "Gun Time")]
        public TimeSpan? GunTime { get; set; }

        [Display(Name = "Is Active")]
        public bool? IsActive { get; set; }

        [StringLength(50)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Updated On")]
        public DateTime? UpdatedOn { get; set; }

        public virtual RaceEventViewModel RaceEvent { get; set; }

        //public virtual ICollection<UserRaceResult> UserRaceResult { get; set; }

        //Foreign Key
        public string EventName { get; set; }
    }
}
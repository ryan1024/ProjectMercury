using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Mercury.Backoffice.Models
{
    public class DashboardsViewModel
    {
        public Int64 TotalRegisteredUsers { get; set; }

        public Int64 TotalRaceResult { get; set; }
        public Int64 TotalRaceResultClaimed { get; set; }

        public decimal TotalRaceResultClaimedPercentage { get; set; }

        public Int64 TotalRaceEvent { get; set; }

    }

    //public class UserRegistrationHistory
    //{
    //    public 
    //}
}
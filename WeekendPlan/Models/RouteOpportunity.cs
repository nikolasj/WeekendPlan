using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class RouteOpportunity
    {
        [Key]
        [Column("route_opportunity_id")]
        public Int32 RouteOpportunityId { get; set; }
        [Column("route_id")]
        public Int32 RouteId { get; set; }
        [Column("opportunity_id")]
        public Int32 OpportunityId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class Route
    {
        [Key]
        [Column("route_id")]
        public Int32 RouteId { get; set; }
        [Column("name")]
        public String Name { get; set; }
        [Column("event_cost")]
        public String EventCost { get; set; }
        [Column("transport_cost")]
        public String TransportCost { get; set; }
        [Column("total_cost")]
        public String TotalCost { get; set; }
        [Column("duration")]
        public String Duration { get; set; }
        [Column("rating")]
        public String Rating { get; set; }
        [Column("user_id")]
        internal Int32 UserId { get; set; }
        [Column("route_dates_to")]
        public DateTime RouteDatesTo { get; set; }
        [Column("route_dates_from")]
        public DateTime RouteDatesFrom { get; set; }

        public User Author { get; set; }
        public List<Opportunity> Opportunities { get; set; }
        public List<Comment> Comments { get; set; }
        public List<String> Tags { get; set; }
    }
}
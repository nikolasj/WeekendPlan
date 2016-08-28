using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class Opportunity
    {
        [Key]
        [Column("opportunity_id")]
        public Int32 OpportunityId { get; set; }
        [Column("title")]
        public String Title { get; set; }
        [Column("description")]
        public String Description { get; set; }
        [Column("date_from")]
        public DateTime DateFrom { get; set; }
        [Column("date_to")]
        public DateTime DateTo { get; set; }
        [Column("duration")]
        public String Duration { get; set; }
        [Column("cost")]
        public String Cost { get; set; }
        [Column("place_id")]
        public Int32 PlaceId { get; set; }
        [Column("coordinates")]
        public Double Coordinates { get; set; }
        [Column("rating")]
        public String Rating { get; set; }
        [Column("show_id")]
        public Int32 ShowId { get; set; }
        [Column("event_id")]
        public Int32 EventId { get; set; }

        public List<Category> Categories { get; set; }
        public Place CurrentPlace { get; set; }
        public Show CurrentShow { get; set; }
        public Event CurrentEvent { get; set; }
        public List<Comment> Comments { get; set; }
        public List<String> Tags { get; set; }
    }
}
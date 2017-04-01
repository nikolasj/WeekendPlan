using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class RouteViewModel
    {
        public Int32 RouteId { get; set; }
        public String Name { get; set; }
        public String EventCost { get; set; }
        public String TransportCost { get; set; }
        public String TotalCost { get; set; }
        public String Duration { get; set; }
        public String Rating { get; set; }
        internal Int32 UserId { get; set; }
        public DateTime RouteDatesTo { get; set; }
        public DateTime RouteDatesFrom { get; set; }

        public UserProfile Author { get; set; }
        public List<Opportunity> Opportunities { get; set; }
        public List<Comment> Comments { get; set; }
        public List<String> Tags { get; set; }

        public RouteViewModel(Route r)
        {
            RouteId = r.RouteId;
            Name = r.Name;
            EventCost = r.EventCost;
            TransportCost = r.TransportCost;
            TotalCost = r.TotalCost;
            Duration = r.Duration;
            Rating = r.Rating;
            UserId = r.UserId;
            RouteDatesTo = r.RouteDatesTo;
            RouteDatesFrom = r.RouteDatesFrom;

            Author = r.Author;
            Opportunities = r.Opportunities;
            Comments = r.Comments;
            Tags = r.Tags;

        }
    }
}
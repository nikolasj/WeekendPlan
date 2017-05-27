using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class OpportunityViewModel
    {
        public Int32 OpportunityId { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public String Duration { get; set; }
        public String Cost { get; set; }
        public Int32 PlaceId { get; set; }
        //public Double Coordinates { get; set; }
        public String Rating { get; set; }
        public Int32 ShowId { get; set; }
        public Int32 EventId { get; set; }
        public Coords Coords { get; set; }
        
        public String Image { get; set; }
        public List<Category> Categories { get; set; }
        public Place CurrentPlace { get; set; }
        public Show CurrentShow { get; set; }
        public Event CurrentEvent { get; set; }
        public List<Comment> Comments { get; set; }
        public List<String> Tags { get; set; }
        public Int32 TimeHour { get; set; }
        public Int32 TypeVacation { get; set; }

        public OpportunityViewModel(Opportunity o)
        {
            OpportunityId = o.OpportunityId;
            Title = o.Title;
            Description = Regex.Replace(o.Description, "<[^>]+>", string.Empty);
            DateFrom = o.DateFrom.AddHours(2);
            DateTo = o.DateTo;
            Duration = o.Duration;
            Cost = o.Cost;
            PlaceId = o.PlaceId;
            Image = o.Image;
            if (String.IsNullOrWhiteSpace(o.CoordsStr)) Coords = new Coords() { Lat = "0", Lon = "0" };
            else
            {
                var arr = o.CoordsStr.Split(new char[] { ';' });
                Coords = new Coords() { Lat = arr[0], Lon = arr[1] };
            }

            Rating = o.Rating;
            ShowId = o.ShowId;
            EventId = o.EventId;
            TypeVacation = o.TypeVacation;
        }

        public OpportunityViewModel()
        { }
    }
}
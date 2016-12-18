using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class EventViewModel
    {
        public Int32 EventId { get; set; }
        public Int32 Id { get; set; }
        public DateTime PublicationDate { get; set; }
        public String Dates { get; set; }
        public String Title { get; set; }
        public String ShortTitle { get; set; }
        public String Slug { get; set; }
        public String Place { get; set; }
        public String Description { get; set; }
        public String BodyText { get; set; }
        public String Location { get; set; }
        public String Categories { get; set; }
        public String Tagline { get; set; }
        public String AgeRestriction { get; set; }
        public String Price { get; set; }
        public Boolean IsFree { get; set; }
        public String Images { get; set; }
        public Int32 FavoritesCount { get; set; }
        public Int32 CommentsCount { get; set; }
        public String SiteUrl { get; set; }
        public String Tag { get; set; }
        public String Participants { get; set; }

        public List<String> Tags { get; set; }
        public List<Comment> Comments { get; set; }

        public EventViewModel(Event ev)
        {
            EventId = ev.EventId;
            Id = ev.Id;
            PublicationDate = ev.PublicationDate;
            //Dates = ev.Dates;
            Title = ev.Title;
            ShortTitle = ev.ShortTitle;
            Slug = ev.Slug;
            Place = ev.Place;
            Description = ev.Description;
            BodyText = Regex.Replace(ev.BodyText, "<[^>]+>", string.Empty);
            Location = ev.Location;
            Categories = ev.Categories;
            Tagline = ev.Tagline;
            AgeRestriction = ev.AgeRestriction;
            Price = ev.Price;
            IsFree = ev.IsFree;
            Images = ev.Images;
            FavoritesCount = ev.FavoritesCount;
            CommentsCount = ev.CommentsCount;
            SiteUrl = ev.SiteUrl;
            Tag = ev.Tag;
            Participants = ev.Participants;
        }
    }
}
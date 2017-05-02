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
        public Int32 Place { get; set; }
        public String Description { get; set; }
        public String BodyText { get; set; }
        public String Location { get; set; }
        public String Categories { get; set; }
        public String Tagline { get; set; }
        public String AgeRestriction { get; set; }
        public String Price { get; set; }
        public Boolean IsFree { get; set; }
        public String[] Images { get; set; }
        public String FirstImage
        {
            get
            {
                if (Images != null)
                    if (Images.Count() > 0)
                        return Images[0].Trim();
                return "https://cdn1.iconfinder.com/data/icons/delivery-logistics-1/512/Logistics-24-512.png";
            }
        }
        public Int32 FavoritesCount { get; set; }
        public Int32 CommentsCount { get; set; }
        public String SiteUrl { get; set; }
        public String Tag { get; set; }
        public String Participants { get; set; }

        public List<Tag> Tags { get; set; }
        public List<Tag> TagsUser { get; set; }
        public List<Comment> Comments { get; set; }

        public EventViewModel(Event ev, int? userId)
        {
            EventId = ev.EventId;
            Id = ev.Id;
            PublicationDate = ev.PublicationDate;
            //Dates = ev.Dates;
            //string[] tempTitle = ev.Title.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //string title = "";
            //if (tempTitle.Length > 4)
            //{
            //    for( int i = 0; i< tempTitle.Length; i++)
            //    {
            //        if(i==4)
            //        {
            //            title += tempTitle[i] + " \n ";
            //        }
            //        else
            //        {
            //            title += tempTitle[i] + " ";
            //        }
            //    }
                
            //}
            //else
            //{
            //    title = ev.Title;
            //}
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
            Images = ev.Images.Split(new char[] { ',' });
            FavoritesCount = ev.FavoritesCount;
            CommentsCount = ev.CommentsCount;
            SiteUrl = ev.SiteUrl;
            Tag = ev.Tags;
            Participants = ev.Participants;
            Comments = ev.GetComments();
            Tags = ev.GetTagsCommon();
            if (!(userId == 0 || userId == null))
                TagsUser = ev.GetTagsByUser(userId.Value);
        }
    }
}
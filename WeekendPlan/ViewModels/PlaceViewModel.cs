using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class PlaceViewModel
    {
        public Int32 PlaceId { get; set; }
        public Int32 Id { get; set; }
        public String Title { get; set; }
        public String ShortTitle { get; set; }
        public String Slug { get; set; }
        public String Address { get; set; }
        public String Lacation { get; set; }
        public String Timetable { get; set; }
        public String Phone { get; set; }
        public Boolean? IsStub { get; set; }
        public String[] Images { get; set; }
        public String FirstImage { get
            {
                if (Images != null)
                    if (Images.Count() > 0)
                        return Images[0].Trim();
                return "https://cdn1.iconfinder.com/data/icons/delivery-logistics-1/512/Logistics-24-512.png";
            }
        }
        public String Description { get; set; }
        public String BodyText { get; set; }
        public String SiteUrl { get; set; }
        public String ForeignUrl { get; set; }
        public Coords Coords { get; set; }
        public String Subway { get; set; }
        public Int32? FavoritesCount { get; set; }
        public Int32? CommentsCount { get; set; }
        public Boolean? IsClosed { get; set; }
        public String CategoriesKudaGo { get; set; }
        public String TagsKudaGo { get; set; }
        public Int32 CityId { get; set; }

        public List<City> Cities { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Tag> TagsUser { get; set; }

        public PlaceViewModel(Place p, int? userId)
        {
            PlaceId = p.PlaceId;
            Id = p.Id;
            Title = p.Title;
            ShortTitle = p.ShortTitle;
            Slug = p.Slug;
            Address = p.Address;
            Lacation = p.Location;
            Timetable = p.Timetable;
            Phone = p.Phone;
            IsStub = p.IsStub;
            Images = p.Images.Split(new char[] { ',' });
            Description = p.Description;
            BodyText = p.BodyText;
            SiteUrl = p.SiteUrl;
            ForeignUrl = p.ForeignUrl;
            Coords = p.Coords;
            Subway = p.Subway;
            FavoritesCount = p.FavoritesCount;
            CommentsCount = p.CommentsCount;
            IsClosed = p.IsClosed;
            CategoriesKudaGo = p.CategoriesKudaGo;
            TagsKudaGo = p.TagsKudaGo;
            CityId = p.CityId;

            //Cities = p.PlaceId;
            Comments = p.GetComments();
            Tags = p.GetTagsCommon();
            if(!(userId==0||userId==null))
                TagsUser = p.GetTagsByUser(userId.Value) ;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class ProfileViewModel
    {
        public Int32 UserId { get; set; }
        public String Name { get; set; }
        public String Picture { get; set; }
        public Int32? City { get; set; }
        public Boolean? DriverLicense { get; set; }
        public Boolean? Car { get; set; }
        public String AspNetUserId { get; set; }

        //public City CurrentCity { get; set; }
        public List<Category> PreferredCategories { get; set; }
        public List<String> PreferredTags { get; set; }
        //public List<Additional> Additionals { get; set; }
        public List<SocialConnection> Connections { get; set; }
        public List<Comment> FilmComments { get; set; }
        public List<Comment> EventComments { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Tag> EventTags { get; set; }
        public List<Tag> FilmTags { get; set; }
        public string webRoot { get; set; }
        public Int32? GroupCount { get; set; }

        public ProfileViewModel(UserProfile user)
        {
            UserId = user.UserId;
            Name = user.Name;
            Picture = user.Picture;//(user.Picture == null) ? user.Picture = "https://www.anonymousvideo.eu/IMG/siteon0.png?1435050135" : user.Picture;
            City = user.City;
            DriverLicense = user.DriverLicense;
            Car = user.Car;
            AspNetUserId = user.AspNetUserId;
            GroupCount = user.GroupCount;
        }

        public void GetCommentsByUser()
        {
            FilmComments = UserProfile.GetCommentsByFilm(UserId);
            EventComments = UserProfile.GetCommentsByEvent(UserId);
        }

        public void GetTagsByUser()
        {
            Tags = UserProfile.GetTagsByUser(UserId);
        }

        public void GetRelatives(UserProfile user)
        {

        }
    }
}
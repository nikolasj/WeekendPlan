using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class Place
    {
        [Key]
        [Column("place_id")]
        public Int32 PlaceId { get; set; }
        [Column("id")]
        public Int32 Id { get; set; }
        [Column("title")]
        public String Title { get; set; }
        [Column("short_title")]
        public String ShortTitle { get; set; }
        [Column("slug")]
        public String Slug { get; set; }
        [Column("address")]
        public String Address { get; set; }
        [Column("location")]
        public Int32 Lacation { get; set; }
        [Column("timetable")]
        public String Timetable { get; set; }
        [Column("phone")]
        public String Phone { get; set; }
        [Column("is_stub")]
        public Boolean IsStub { get; set; }
        [Column("images")]
        public String Images { get; set; }
        [Column("description")]
        public String Description { get; set; }
        [Column("body_text")]
        public String BodyText { get; set; }
        [Column("site_url")]
        public String SiteUrl { get; set; }
        [Column("foreign_url")]
        public String ForeignUrl { get; set; }
        [Column("coords")]
        public Double Coords { get; set; }
        [Column("subway")]
        public String Subway { get; set; }
        [Column("favorites_count")]
        public Int32 FavoritesCount { get; set; }
        [Column("comments_count")]
        public Int32 CommentsCount { get; set; }
        [Column("is_closed")]
        public Boolean IsClosed { get; set; }
        [Column("categories")]
        public String CategoriesKudaGo { get; set; }
        [Column("tags")]
        public String TagsKudaGo { get; set; }
        [Column("city_id")]
        public Int32 CityId { get; set; }

        public List<City> Cities { get; set; }
        public List<Comment> Comments { get; set; }
        public List<String> Tags { get; set; }
    }
}
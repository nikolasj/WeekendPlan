using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class Event
    {
        [Key]
        [Column("event_id")]
        public Int32 EventId { get; set; }
        [Column("id")]
        public Int32 Id { get; set; }
        [Column("publication_date")]
        public DateTime PublicationDate { get; set; }
        [Column("dates")]
        public String Dates { get; set; }
        [Column("title")]
        public String Title { get; set; }
        [Column("short_title")]
        public String ShortTitle { get; set; }
        [Column("slug")]
        public Int32 Slug { get; set; }
        [Column("place")]
        public String Place { get; set; }
        [Column("description")]
        public String Description { get; set; }
        [Column("body_text")]
        public String BodyText { get; set; }
        [Column("location")]
        public String Location { get; set; }
        [Column("categories")]
        public String Categories { get; set; }
        [Column("tagline")]
        public String Tagline { get; set; }
        [Column("age_restriction")]
        public String AgeRestriction { get; set; }
        [Column("price")]
        public String Price { get; set; }
        [Column("is_free")]
        public Boolean IsFree { get; set; }
        [Column("images")]
        public String Images { get; set; }
        [Column("favorites_count")]
        public Int32 FavoritesCount { get; set; }
        [Column("comments_count")]
        public Int32 CommentsCount { get; set; }
        [Column("site_url")]
        public String SiteUrl { get; set; }
        [Column("tags")]
        public String Tag { get; set; }
        [Column("participants")]
        public String Participants { get; set; }

        public List<String> Tags { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
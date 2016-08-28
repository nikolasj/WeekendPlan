using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class Film
    {
        [Key]
        [Column("film_id")]
        public Int32 FilmId { get; set; }
        [Column("site_url")]
        public String SiteUrl { get; set; }
        [Column("publication_date")]
        public String PublicationDate { get; set; }
        [Column("slug")]
        public String Slug { get; set; }
        [Column("title")]
        public String Title { get; set; }
        [Column("description")]
        public String Description { get; set; }
        [Column("body_text")]
        public String BodyText { get; set; }
        [Column("is_editors_choice")]
        public Boolean IsEditorsChoice { get; set; }
        [Column("favorites_count")]
        public Int32 FavoritesCount { get; set; }
        [Column("genres")]
        public String Genres { get; set; }
        [Column("comments_count")]
        public Int32 CommentsCount { get; set; }
        [Column("original_title")]
        public String OriginalTitle { get; set; }
        [Column("locale")]
        public String Locale { get; set; }
        [Column("country")]
        public String Country { get; set; }
        [Column("year")]
        public String Year { get; set; }
        [Column("language")]
        public String Language { get; set; }
        [Column("running_time")]
        public String RunningTime { get; set; }
        [Column("budget_currency")]
        public String BudgetCurrency { get; set; }
        [Column("budget")]
        public String Budget { get; set; }
        [Column("mpaa_rating")]
        public String MpaaRating { get; set; }
        [Column("age_restriction")]
        public String AgeRestriction { get; set; }
        [Column("stars")]
        public String Stars { get; set; }
        [Column("director")]
        public String Director { get; set; }
        [Column("writer")]
        public String Writer { get; set; }
        [Column("awards")]
        public String Awards { get; set; }
        [Column("trailer")]
        public String Trailer { get; set; }
        [Column("images")]
        public String Images { get; set; }
        [Column("poster")]
        public String Poster { get; set; }
        [Column("url")]
        public String Url { get; set; }
        [Column("imdb_url")]
        public String ImdbUrl { get; set; }
        [Column("imdb_rating")]
        public String ImdbRating { get; set; }

        public List<Comment> Comments { get; set; }
        public List<String> Tags { get; set; }
    }
}
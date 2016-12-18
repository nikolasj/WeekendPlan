using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class FilmViewModel
    {
        public Int32 FilmId { get; set; }
        public String SiteUrl { get; set; }
        public String PublicationDate { get; set; }
        public String Slug { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String BodyText { get; set; }
        public Boolean? IsEditorsChoice { get; set; }
        public Int32? FavoritesCount { get; set; }
        public String Genres { get; set; }
        public Int32? CommentsCount { get; set; }
        public String OriginalTitle { get; set; }
        public String Locale { get; set; }
        public String Country { get; set; }
        public String Year { get; set; }
        public String Language { get; set; }
        public String RunningTime { get; set; }
        public String BudgetCurrency { get; set; }
        public String Budget { get; set; }
        public String MpaaRating { get; set; }
        public String AgeRestriction { get; set; }
        public String Stars { get; set; }
        public String Director { get; set; }
        public String Writer { get; set; }
        public String Awards { get; set; }
        public String Trailer { get; set; }
        public String Images { get; set; }
        public String Poster { get; set; }
        public String Url { get; set; }
        public String ImdbUrl { get; set; }
        public String ImdbRating { get; set; }

        public List<Comment> Comments { get; set; }
        public List<String> Tags { get; set; }
        public List<Film> SimilarMovies { get; set; }

        public FilmViewModel(Film film)
        {
            FilmId = film.FilmId;
            SiteUrl = film.SiteUrl;
            PublicationDate = film.PublicationDate;
            Slug = film.Slug;
            Title = film.Title;
            Description = film.Description;
            BodyText = Regex.Replace(film.BodyText, "<[^>]+>", string.Empty);
            IsEditorsChoice = film.IsEditorsChoice;
            FavoritesCount = film.FavoritesCount;
            Genres = film.Genres;
            CommentsCount = film.CommentsCount;
            OriginalTitle = film.OriginalTitle;
            Locale = film.Locale;
            Country = film.Country;
            Year = film.Year;
            Language = film.Language;
            RunningTime = film.RunningTime;
            BudgetCurrency = film.BudgetCurrency;
            Budget = film.Budget;
            MpaaRating = film.MpaaRating;
            AgeRestriction = film.AgeRestriction;
            Stars = film.Stars;
            Director = film.Director;
            Writer = film.Writer;
            Awards = film.Awards;
            Trailer = film.Trailer;
            Images = film.Images;
            Poster = film.Poster;
            Url = film.Url;
            ImdbUrl = film.ImdbUrl;
            ImdbRating = film.ImdbRating;
            Comments = film.GetComments();
            
        }
    }
}
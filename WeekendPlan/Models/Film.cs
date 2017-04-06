using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class Film
    {
        [Key]
        [Column("film_id")]
        public Int32 FilmId { get; set; }
        [Column("id")]
        public Int32 Id { get; set; }
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
        public Boolean? IsEditorsChoice { get; set; }
        [Column("favorites_count")]
        public Int32? FavoritesCount { get; set; }
        [Column("genres")]
        public String Genres { get; set; }
        [Column("comments_count")]
        public Int32? CommentsCount { get; set; }
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
        //public List<String> Tags { get; set; }
        public List<Tag> Tags { get; set; }

        public static List<Film> GetFilmsForOpportunity(DateTime concreteDate, UserProfile user, List<Tag> tags = null, int? price = null,
            String location = null, String transport = null, int countPersons = 0, int countEvents = 5, bool allWeather = false)
        {
            DbConnect connector = new DbConnect();
            List<int> resultFilmsByTag = new List<int>();
            List<Film> films = connector.Films.ToList();
            List<TagFilm> tagFilms = connector.TagFilms.ToList();
            List<Film> resultFilms = new List<Film>();

            var tagsByUser = tags; //Tag.GetTagsByUser(user.UserId);
            foreach (var tf in tagFilms)
            {
                foreach (var t in tagsByUser)
                {
                    if (tf.TagId == t.TagId)
                    {
                        resultFilmsByTag.Add(tf.FilmId);
                    }
                }
            }

            foreach (var t in resultFilmsByTag)
            {
                var f = films.FirstOrDefault(x => x.FilmId == t);
                if (f != null)
                    resultFilms.Add(f);
            }

            return resultFilms.ToList();
        }

        public static List<Film> GetFilms()
        {
            DbConnect connector = new DbConnect();
            List<Film> films = connector.Films.ToList<Film>();

            return films;
        }

        public static List<Film> GetFilmsByTag(int tagId)
        {
            DbConnect connector = new DbConnect();
            List<Film> films = connector.Films.ToList<Film>();
            //---
            List<int> tagFilmById = connector.TagFilms.Where(x=>x.TagId == tagId).Select(x=>x.FilmId).ToList();
            films = connector.Films.Where(t => tagFilmById.Contains(t.FilmId)).ToList();

            return films;
        }

        public static List<Comment> GetComments(int? filmId)
        {
            List<Comment> filmComments = new List<Comment>();
            DbConnect connector = new DbConnect();
            List<CommentFilm> commentsFilmById = connector.CommentFilms.Where(x=>x.FilmId == filmId).ToList<CommentFilm>();
            foreach(var c in commentsFilmById)
            {
                var comment = connector.Comments.FirstOrDefault(x => x.CommentId == c.CommentId);
                var userId = comment.UserId;
                comment.Author = connector.Users.FirstOrDefault(y => y.UserId == userId);

                if (comment!=null)
                {
                    filmComments.Add(comment);
                }
            }
            return filmComments;
        }

        public List<Comment> GetComments()
        {
            List<Comment> filmComments = new List<Comment>();
            DbConnect connector = new DbConnect();
            List<CommentFilm> commentsFilmById = connector.CommentFilms.Where(x => x.FilmId == this.FilmId).ToList<CommentFilm>();
            foreach (var c in commentsFilmById)
            {
                var comment = connector.Comments.FirstOrDefault(x => x.CommentId == c.CommentId);
                var userId = comment.UserId;
                comment.Author = connector.Users.FirstOrDefault(y => y.UserId == userId);

                if (comment != null)
                {
                    filmComments.Add(comment);
                }
            }
            Comments = filmComments;
            return filmComments;
        }

        public List<Tag> GetTags()
        {
            List<Tag> filmTags = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<TagFilm> tagsFilmById = connector.TagFilms.Where(x => x.FilmId == this.FilmId).ToList<TagFilm>();
            foreach (var c in tagsFilmById)
            {
                var comment = connector.Tags.FirstOrDefault(x => x.TagId == c.TagId);
                var userId = comment.UserId;
                //comment.Author = connector.Users.FirstOrDefault(y => y.UserId == userId);

                if (comment != null)
                {
                    filmTags.Add(comment);
                }
            }
            Tags = filmTags;
            return filmTags;
        }

        public List<Tag> GetTagsCommon()
        {
            List<Tag> filmTags = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<TagFilm> tagsFilmById = connector.TagFilms.Where(x => x.FilmId == this.FilmId).ToList<TagFilm>();
            foreach (var c in tagsFilmById)
            {
                var comment = connector.Tags.FirstOrDefault(x => x.TagId == c.TagId && (x.UserId == null || x.UserId == 0));
                // var userId = comment.UserId;
                //comment.Author = connector.Users.FirstOrDefault(y => y.UserId == userId);

                if (comment != null)
                {
                    filmTags.Add(comment);
                }
            }
            Tags = filmTags;
            return filmTags;
        }

        public List<Tag> GetTagsByUser(int userId)
        {
            List<Tag> filmTags = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<TagFilm> tagsFilmById = connector.TagFilms.Where(x => x.FilmId == this.FilmId).ToList<TagFilm>();
            foreach (var c in tagsFilmById)
            {
                var comment = connector.Tags.FirstOrDefault(x => x.TagId == c.TagId && x.UserId == userId);

                if (comment != null)
                {
                    filmTags.Add(comment);
                }
            }
            Tags = filmTags;
            return filmTags;
        }
    }
}
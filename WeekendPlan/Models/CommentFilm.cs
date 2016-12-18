using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class CommentFilm
    {
        [Key]
        [Column("comment_film_id")]
        public Int32 CommentFilmId { get; set; }
        [Column("comment_id")]
        public Int32 CommentId { get; set; }
        [Column("film_id")]
        public Int32 FilmId { get; set; }

        public static CommentFilm AddComment(CommentFilm c)
        {
            DbConnect connector = new DbConnect();
            connector.CommentFilms.Add(c);//?
            connector.SaveChanges();
            return c;
        }
    }
}
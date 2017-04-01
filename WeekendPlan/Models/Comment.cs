using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class Comment
    {
        [Key]
        [Column("comment_id")]
        public Int32 CommentId { get; set; }
        [Column("text")]
        public String Text { get; set; }
        [Column("user_id")]
        public Int32 UserId { get; set; }
        [Column("parent_id")]
        public Int32? ParentId { get; set; }
        public Film Film { get; set; }
        public virtual ICollection<CommentFilm> CommentsFilm { get; set; }
        public UserProfile Author { get; set; }

        public static Comment AddComment(Comment c)
        {
            DbConnect connector = new DbConnect();
            connector.Comments.Add(c);//?
            connector.SaveChanges();
            return c;
        }

        public static Film GetFilmByComment(int id)
        {
            DbConnect connector = new DbConnect();
            Film film = connector.Films.FirstOrDefault(x => x.FilmId == id);

            return film;
        }

    }
}
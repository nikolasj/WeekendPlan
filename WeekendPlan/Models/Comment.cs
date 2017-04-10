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

        public static Comment UpdateComment(int id, String text)
        {
            DbConnect connector = new DbConnect();

            var update = connector.Comments.Find(id);//?
            if (update != null)
            {
                update.Text = text;

                connector.Entry(update).CurrentValues.SetValues(update);
                connector.SaveChanges();
            }

            return update;
        }

        public static bool DeleteComment(int id)
        {
            DbConnect connector = new DbConnect();

            Comment search = connector.Comments.FirstOrDefault(x => x.CommentId == id);
            if (search != null)
            {
                connector.Comments.Remove(search);
                connector.SaveChanges();
                return true;
            }
            return false;
        }

        public static Film GetFilmByComment(int id)
        {
            DbConnect connector = new DbConnect();
            Film film = connector.Films.FirstOrDefault(x => x.FilmId == id);

            return film;
        }

        public static Comment GetComment(int id)
        {
            DbConnect connector = new DbConnect();
            Comment comment = connector.Comments.FirstOrDefault(x => x.CommentId == id);

            return comment;
        }

        public static int GetUserIdByCommentId(int id)
        {
            DbConnect connector = new DbConnect();
            Comment comment = connector.Comments.FirstOrDefault(x => x.CommentId == id);

            return comment.UserId;
        }
    }
}
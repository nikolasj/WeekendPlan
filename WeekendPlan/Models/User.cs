using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    [Table("User")]
    public class UserProfile
    {
        [Key]
        [Column("user_id")]
        public Int32 UserId { get; set; }
        [Column("name")]
        public String Name { get; set; }
        [Column("picture")]
        public String Picture { get; set; }
        [Column("city_id")]
        public Int32? City { get; set; }
        [Column("driver_license")]
        public Boolean? DriverLicense { get; set; }
        [Column("car")]
        public Boolean? Car { get; set; }
        [Column("asp_net_user_id")]
        public String AspNetUserId { get; set; }
        [Column("group_count")]
        public Int32? GroupCount { get; set; }
        [Column("location")]
        public String Location { get; set; }

        //public City CurrentCity { get; set; }
        public List<Category> PreferredCategories { get; set; }
        public List<String> PreferredTags { get; set; }
        //public List<Additional> Additionals { get; set; }
        public List<SocialConnection> Connections { get; set; }
        public List<Comment> Comments { get; set; }

        public static UserProfile AddUser(UserProfile u)
        {
            DbConnect connector = new DbConnect();
            connector.Users.Add(u);//?
            connector.SaveChanges();
            return u;
        }

        public static List<UserProfile> GetUsers()
        {
            DbConnect connector = new DbConnect();
            List<UserProfile> users = connector.Users.ToList<UserProfile>();

            return users;
        }

        public static UserProfile GetUser(int id)
        {
            DbConnect connector = new DbConnect();
            UserProfile user = connector.Users.FirstOrDefault(x => x.UserId == id);

            return user;
        }

        public static UserProfile UpdateUser(UserProfile updateUser)
        {
            DbConnect connector = new DbConnect();

            var update = connector.Users.Find(updateUser.UserId);//?
            if (update != null)
            {
                connector.Entry(update).CurrentValues.SetValues(updateUser);
                connector.SaveChanges();
            }

            return updateUser;
        }

        public static List<Comment> GetCommentsByUser(int? userId)
        {
            DbConnect connector = new DbConnect();
            List<Comment> сomments = connector.Comments.Where(x => x.UserId == userId).ToList<Comment>();
            
            return сomments;
        }

        public static List<Comment> GetCommentsByFilm(int? userId)
        {
            List<Comment> filmComments = new List<Comment>();
            DbConnect connector = new DbConnect();
            List<Comment> сomments = connector.Comments.Where(x => x.UserId == userId).ToList<Comment>();
            List<CommentFilm> commentsFilmById = connector.CommentFilms.ToList<CommentFilm>();
            foreach (var c in commentsFilmById)
            {
                foreach(var com in сomments)
                {
                    if(c.CommentId == com.CommentId)
                    {
                        com.Film = Comment.GetFilmByComment(c.FilmId);
                        com.Author = connector.Users.Find(userId);
                        filmComments.Add(com);
                    }
                }
            }
            return filmComments;
        }

        public static List<Comment> GetCommentsByEvent(int? userId)
        {
            List<Comment> eventComments = new List<Comment>();
            DbConnect connector = new DbConnect();
            List<Comment> сomments = connector.Comments.Where(x => x.UserId == userId).ToList<Comment>();
            List<CommentEvent> commentsEventById = connector.CommentEvents.ToList<CommentEvent>();
            foreach (var c in commentsEventById)
            {
                foreach (var com in сomments)
                {
                    if (c.CommentId == com.CommentId)
                    {
                        com.Author = connector.Users.Find(userId);
                        eventComments.Add(com);
                    }
                }
            }
            return eventComments;
        }

        public static List<Tag> GetTagsByFilm(int? userId)
        {
            List<Tag> tagComments = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<Tag> сomments = connector.Tags.Where(x => x.UserId == userId).ToList<Tag>();
            List<TagFilm> tagsFilmById = connector.TagFilms.ToList<TagFilm>();
            foreach (var c in tagsFilmById)
            {
                foreach (var com in сomments)
                {
                    if (c.TagId == com.TagId)
                    {
                        com.Author = connector.Users.Find(userId);
                        tagComments.Add(com);
                    }
                }
            }
            return tagComments;
        }

        public static List<Tag> GetTagsByEvent(int? userId)
        {
            List<Tag> tagComments = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<Tag> сomments = connector.Tags.Where(x => x.UserId == userId).ToList<Tag>();
            List<TagEvent> tagsEventById = connector.TagEvents.ToList<TagEvent>();
            foreach (var c in tagsEventById)
            {
                foreach (var com in сomments)
                {
                    if (c.TagId == com.TagId)
                    {
                        com.Author = connector.Users.Find(userId);
                        tagComments.Add(com);
                    }
                }
            }
            return tagComments;
        }

        public static List<Tag> GetTagsByUser(int? userId)
        {
            List<Tag> tagComments = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<Tag> tags = connector.Tags.Where(x => x.UserId == userId).ToList<Tag>();
            foreach (var t in tags)
            {
                  tagComments.Add(t);
            }
            return tagComments;
        }
    }
}
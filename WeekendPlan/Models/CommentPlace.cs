using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class CommentPlace
    {
        [Key]
        [Column("comment_place_id")]
        public Int32 CommentPlaceId { get; set; }
        [Column("comment_id")]
        public Int32 CommentId { get; set; }
        [Column("place_id")]
        public Int32 PlaceId { get; set; }

        public static CommentPlace AddComment(CommentPlace c)
        {
            DbConnect connector = new DbConnect();
            connector.CommentPlaces.Add(c);//?
            connector.SaveChanges();
            return c;
        }
    }
}
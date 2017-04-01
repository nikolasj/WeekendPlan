using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class CommentEvent
    {
        [Key]
        [Column("comment_event_id")]
        public Int32 CommentEventId { get; set; }
        [Column("comment_id")]
        public Int32 CommentId { get; set; }
        [Column("event_id")]
        public Int32 EventId { get; set; }

        public static CommentEvent AddComment(CommentEvent c)
        {
            DbConnect connector = new DbConnect();
            connector.CommentEvents.Add(c);//?
            connector.SaveChanges();
            return c;
        }
    }
}
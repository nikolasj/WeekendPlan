using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        public Int32 ParentId { get; set; }

        public User Author { get; set; }
        public List<Comment> Children { get; set; }
        public Comment Parent { get; set; }
    }
}
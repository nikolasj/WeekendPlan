using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class CommentViewModel
    {
        public Int32 CommentId { get; set; }
        public String Text { get; set; }
        public Int32 UserId { get; set; }
        public Int32? ParentId { get; set; }

        public CommentViewModel(Comment comment)
        {
            CommentId = comment.CommentId;
            Text = comment.Text;
            UserId = comment.UserId;
            ParentId = comment.ParentId;
        }
    }
}
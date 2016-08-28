using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class Tag
    {
        [Key]
        [Column("tag_id")]
        public Int32 TagId { get; set; }
        [Column("text")]
        public String Text { get; set; }
        [Column("user_id")]
        public Int32 UserId { get; set; }

        public User Author { get; set; }
    }
}
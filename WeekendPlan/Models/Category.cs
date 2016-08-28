using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class Category
    {
        [Key]
        [Column("category_id")]
        public Int32 CategoryId { get; set; }
        [Column("slug")]
        public String Slug { get; set; }
        [Column("name")]
        public String Name { get; set; }
    }
}
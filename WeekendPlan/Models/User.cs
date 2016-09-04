using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class UserProfile
    {
        [Key]
        [Column("user_id")]
        public Int32 UserId { get; set; }
        [Column("name")]
        public String name { get; set; }
        [Column("picture")]
        public String Picture { get; set; }
        [Column("city_id")]
        public Int32 City { get; set; }
        [Column("driver_license")]
        public Boolean DriverLicense { get; set; }
        [Column("car")]
        public string Car { get; set; }
        [Column("asp_net_user_id")]
        public String AspNetUserId { get; set; }

        public City CurrentCity { get; set; }
        public List<Category> PreferredCategories { get; set; }
        public List<String> PreferredTags { get; set; }
        //public List<Additional> Additionals { get; set; }
        public List<SocialConnection> Connections { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
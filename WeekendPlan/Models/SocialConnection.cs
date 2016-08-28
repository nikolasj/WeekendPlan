using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class SocialConnection
    {
        [Key]
        [Column("social_connection_id")]
        public Int32 SocialConnectionId { get; set; }
        [Column("user_id1")]
        public Int32 UserId1 { get; set; }
        [Column("user_id2")]
        public Int32 UserId2 { get; set; }
        [Column("connection_type_id")]
        public Int32 ConnectionTypeId { get; set; }
        [Column("connection_type_name")]
        public String ConnectionTypeName { get; set; }

        public User User1 { get; set; }
        public User User2 { get; set; }
    }
}
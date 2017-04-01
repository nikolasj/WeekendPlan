using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

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
        public Int32? UserId { get; set; }

        public UserProfile Author { get; set; }

        public static int GetTagIdByText(string tag)
        {
            DbConnect connector = new DbConnect();
            Tag search = connector.Tags.FirstOrDefault(x => x.Text.IndexOf(tag.Trim())>=0);
            if(search!=null)
            {
                return search.TagId;
            }

            return 0;
        }

        public static List<Tag> GetUserIdByTextTag(string tag)
        {
            DbConnect connector = new DbConnect();
            List<Tag> search = connector.Tags.Where(x => x.Text.IndexOf(tag.Trim()) >= 0).ToList();
            if (search != null)
            {
                return search;
            }

            return null;
        }

        public static List<Tag> GetTagsByUser(int userId)
        {
            DbConnect connector = new DbConnect();
            List<Tag> search = connector.Tags.Where(x => x.UserId == userId).ToList();
            if (search != null)
            {
                return search;
            }

            return null;
        }

        public static Tag AddTag(Tag t)
        {
            DbConnect connector = new DbConnect();
            connector.Tags.Add(t);//?
            connector.SaveChanges();
            return t;
        }

        public static bool DeleteTag(Tag t)
        {
            DbConnect connector = new DbConnect();
            Tag search = connector.Tags.FirstOrDefault(x => x.TagId == t.TagId);
            if (search != null)
            {
                connector.Tags.Remove(search);
                connector.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
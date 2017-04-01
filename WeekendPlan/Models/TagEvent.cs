using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class TagEvent
    {
        [Key]
        [Column("tag_event_id")]
        public Int32 TagEventId { get; set; }
        [Column("tag_id")]
        public Int32 TagId { get; set; }
        [Column("event_id")]
        public Int32 EventId { get; set; }

        public static TagEvent AddTag(TagEvent t)
        {
            DbConnect connector = new DbConnect();
            connector.TagEvents.Add(t);//?
            connector.SaveChanges();
            return t;
        }

        public static List<Tag> GetTags()
        {
            List<Tag> result = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<TagEvent> tagsEvent = connector.TagEvents.ToList<TagEvent>();
            List<Tag> tags = connector.Tags.ToList<Tag>();
            foreach (var t in tagsEvent)
            {
                if (result.Count(x => x.TagId == t.TagId) == 0)
                    result.Add(tags.Find(x => x.TagId == t.TagId));
            }

            return result;
        }

        public static bool DeleteTagEvent(Tag t)
        {
            DbConnect connector = new DbConnect();
            TagEvent search = connector.TagEvents.FirstOrDefault(x => x.TagId == t.TagId);
            if (search != null)
            {
                connector.TagEvents.Remove(search);
                connector.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
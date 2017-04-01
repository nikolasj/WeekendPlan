using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class TagPlace
    {
        [Key]
        [Column("tag_place_id")]
        public Int32 TagPlaceId { get; set; }
        [Column("tag_id")]
        public Int32 TagId { get; set; }
        [Column("place_id")]
        public Int32 PlaceId { get; set; }

        public static TagPlace AddTag(TagPlace t)
        {
            DbConnect connector = new DbConnect();
            connector.TagPlaces.Add(t);//?
            connector.SaveChanges();
            return t;
        }

        public static List<Tag> GetTags()
        {
            List<Tag> result = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<TagPlace> tagsPlace = connector.TagPlaces.ToList<TagPlace>();
            List<Tag> tags = connector.Tags.ToList<Tag>();
            foreach (var t in tagsPlace)
            {
                if (result.Count(x => x.TagId == t.TagId) == 0)
                    result.Add(tags.Find(x => x.TagId == t.TagId));
            }

            return result;
        }

        public static bool DeleteTagPlace(Tag t)
        {
            DbConnect connector = new DbConnect();
            TagPlace search = connector.TagPlaces.FirstOrDefault(x => x.TagId == t.TagId);
            if (search != null)
            {
                connector.TagPlaces.Remove(search);
                connector.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
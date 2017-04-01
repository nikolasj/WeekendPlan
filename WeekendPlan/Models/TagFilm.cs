using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class TagFilm
    {
        [Key]
        [Column("tag_film_id")]
        public Int32 TagFilmId { get; set; }
        [Column("tag_id")]
        public Int32 TagId { get; set; }
        [Column("film_id")]
        public Int32 FilmId { get; set; }

        public static TagFilm AddTag(TagFilm t)
        {
            DbConnect connector = new DbConnect();
            connector.TagFilms.Add(t);//?
            connector.SaveChanges();
            return t;
        }

        public static List<Tag> GetTags()
        {
            List<Tag> result = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<TagFilm> tagsFilm = connector.TagFilms.ToList<TagFilm>();
            List<Tag> tags = connector.Tags.ToList<Tag>();
            foreach( var t in tagsFilm)
            {
                if(result.Count(x => x.TagId == t.TagId)==0)
                    result.Add(tags.Find(x => x.TagId == t.TagId));
            }

            return result;
        }

        public static bool DeleteTagFilm(Tag t)
        {
            DbConnect connector = new DbConnect();
            TagFilm search = connector.TagFilms.FirstOrDefault(x => x.TagId == t.TagId);
            if (search != null)
            {
                connector.TagFilms.Remove(search);
                connector.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class Genres
    {
        [Key]
        [Column("genres_id")]
        public Int32 GenresId { get; set; }
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Slug { get; set; }

        public static List<Genres> GetGenres()
        {
            DbConnect connector = new DbConnect();
            List<Genres> genres = connector.Genres.ToList<Genres>();

            return genres;
        }
    }
}
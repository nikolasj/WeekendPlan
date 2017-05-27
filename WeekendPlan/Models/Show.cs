using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class Show
    {
        [Key]
        [Column("show_id")]
        public Int32 ShowId { get; set; }
        [Column("kudago_id")]
        public Int32 KudagoId { get; set; }
        [Column("movie_id")]
        public Int32 MovieId { get; set; }
        [Column("place_id")]
        public Int32 PlaceId { get; set; }
        [Column("datetime")]
        public DateTime Datetime { get; set; }
        [Column("three_d")]
        public Boolean ThreeD { get; set; }
        [Column("imax")]
        public Boolean Imax { get; set; }
        [Column("four_dx")]
        public Boolean FourDx { get; set; }
        [Column("original_language")]
        public Boolean OriginalLanguage { get; set; }
        [Column("price")]
        public String Price { get; set; }
        [Column("film_name")]
        public String FilmName { get; set; }
        [Column("description")]
        public String Description { get; set; }

        public static List<Show> GetShowsForOpportunity(DateTime concreteDate, UserProfile user, List<Place> places, List<Tag> tags = null, int? price = null,
    String location = null, String transport = null, int countPersons = 0, int countEvents = 1, bool allWeather = false)
        {
            List<Film> films = Film.GetFilmsForOpportunity(concreteDate,user,tags);
            DbConnect connector = new DbConnect();
            List<Show> shows = connector.Shows.Where(x=>x.Datetime <= concreteDate && x.Datetime >=DateTime.Now).ToList();
            //List<Place> places = connector.Places.Where(x=>x.Location == user.Location).ToList();
            List<Show> resultShows = new List<Show>();
            List<Show> locationShows = new List<Show>();
            List<Show> sortedShows = new List<Show>();

            foreach (var s in shows)
            {
                foreach (var p in places)
                {
                    if (p.Id == s.PlaceId)
                    {
                        locationShows.Add(s);
                    }
                }
            }

            foreach (var ls in locationShows)
            {
                var f = films.FirstOrDefault(x => x.Id == ls.MovieId);
                if (f != null)
                {
                    ls.FilmName = f.Title;
                    ls.Description = f.BodyText;

                    if (!resultShows.Any(x => x.ShowId == ls.ShowId))
                    {
                        ls.FilmName = f.Title;
                        ls.Description = f.BodyText;
                        resultShows.Add(ls);
                    }
                }
            }

            //sortedShows.Add(resultShows[0]);
            //foreach (var f in resultShows)
            //{
            //    var show =
            //}

            return resultShows.ToList();
        }
    }
}
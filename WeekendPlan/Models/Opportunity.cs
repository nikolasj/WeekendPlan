using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class Opportunity
    {
        // AIzaSyD5ZFQ_hw2SLWc8o8vyiWVjmR5_UaX_J_M
        [Key]
        [Column("opportunity_id")]
        public Int32 OpportunityId { get; set; }
        [Column("title")]
        public String Title { get; set; }
        [Column("description")]
        public String Description { get; set; }
        [Column("date_from")]
        public DateTime DateFrom { get; set; }
        [Column("date_to")]
        public DateTime DateTo { get; set; }
        [Column("duration")]
        public String Duration { get; set; }
        [Column("cost")]
        public String Cost { get; set; }
        [Column("place_id")]
        public Int32 PlaceId { get; set; }
        //[Column("coordinates")]
        //public Double Coordinates { get; set; }
        [Column("rating")]
        public String Rating { get; set; }
        [Column("show_id")]
        public Int32 ShowId { get; set; }
        [Column("event_id")]
        public Int32 EventId { get; set; }
        [Column("coords")]
        public String CoordsStr { get; set; }
        [Column("type_vacation")]
        public Int32 TypeVacation { get; set; }

        [NotMapped]
        public String Image { get; set; }

        public List<Category> Categories { get; set; }
        [NotMapped]
        public Place CurrentPlace { get; set; }
        [NotMapped]
        public Show CurrentShow { get; set; }
        [NotMapped]
        public Event CurrentEvent { get; set; }
        public List<Comment> Comments { get; set; }
        public List<String> Tags { get; set; }

        public static List<Opportunity> GetOpportunitiesByDateForUser(UserProfile user, DateTime date, String location,
            int typeVacation, int count, List<Opportunity> opportunity, List<Tag> tags = null, int? price = null,
            String transport = null, int countPersons = 0, int countEvents = 1, bool allWeather = false)
        {
            List<Opportunity> result = new List<Opportunity>();

            //объединение списков вариантов фильмы+ивенты+(рестораны)+"свободные" Opportunity
            List<Place> places = Place.GetPlacesForOpportunity(date, user, tags, location: location);
            List<Show> shows = Show.GetShowsForOpportunity(date, user, places, tags, countEvents: count, location: location); //100
            List<Event> events = Event.GetEventsForOpportunity(date, user, tags, countEvents: count, location: location);
            var listEvents = ConvertEventToOpportunity(events); //20
            //MovieId
            List<Opportunity> listShows = ConvertFilmToOpportunity(shows.GroupBy(x => x.MovieId).Select(y => y.First()).ToList());
            var listPlaces = ConvertPlaceToOpportunity(places);

            if (count < 2) count = 2;
            
            result.AddRange(listEvents.Where(x =>x.TypeVacation == typeVacation));
            result.AddRange(listShows.Where(x => x.TypeVacation == typeVacation));
       
            //generate fake id's
            
            //foreach(var o in result)
            //{
            //    He
            //}

            for (int i = 0; i < result.Count; i++)
            {
                result[i].OpportunityId = i;
            }

            return result;//.Take(count).ToList();
        }

        private static List<Opportunity> ConvertFilmToOpportunity(List<Show> shows)
        {
            List<Opportunity> result = new List<Opportunity>();
            List<Opportunity> tempResult = new List<Opportunity>();
            var s = shows.GroupBy(x => x.Datetime.Hour).Select(y => y.First()).ToList();
            foreach (var e in s)
            {
                if (result.Count == 0)
                {
                    var film = Film.GetFilms().Find(x => x.Id == e.MovieId);

                    Opportunity opportunity = new Opportunity();
                    opportunity.Title = e.FilmName;
                    opportunity.Description = e.Description;
                    opportunity.DateFrom = e.Datetime;
                    opportunity.ShowId = e.ShowId;
                    var place = Place.GetPlaceById(e.PlaceId);
                    opportunity.CoordsStr = place.CoordsStr;
                    opportunity.Image = (film.Images.Any())? film.Images.ToString():"";
                    //opportunity.OpportunityId = e.F
                    opportunity.CurrentShow = e;// new Show(e);
                    opportunity.TypeVacation = 2;
                    opportunity.Tags = film.Tags.Select(x => x.Text).ToList();
                    result.Add(opportunity);
                    continue;
                }
                if (e.Datetime.Hour - result[0].DateFrom.Hour >= 2 || result[0].DateFrom.Hour - e.Datetime.Hour >= 2)
                {
                    Opportunity opportunity = new Opportunity();
                    opportunity.Title = e.FilmName;
                    opportunity.Description = e.Description;
                    opportunity.DateFrom = e.Datetime;
                    opportunity.ShowId = e.ShowId;
                    opportunity.CoordsStr = Place.GetPlaceById(e.PlaceId).CoordsStr;
                    opportunity.CurrentShow = e;// new Show(e);
                    opportunity.TypeVacation = 2;
                    if (!result.Any(x => x.DateFrom.Hour == e.Datetime.Hour))
                        result.Add(opportunity);
                }
            }
            return result;
        }

        private static List<Opportunity> ConvertPlaceToOpportunity(List<Place> places)
        {
            List<Opportunity> result = new List<Opportunity>();
            foreach (var e in places)
            {
                Opportunity opportunity = new Opportunity();
                opportunity.Title = e.Title;
                opportunity.Description = e.Description;
                //opportunity.DateFrom = DateTime.Parse(e.Timetable);
                opportunity.PlaceId = e.PlaceId;
                //opportunity.Coords = Place.GetPlaceById(e.PlaceId).Coords;
                result.Add(opportunity);
            }
            return result;
        }

        private static List<Opportunity> ConvertEventToOpportunity(List<Event> events)
        {
            List<Opportunity> result = new List<Opportunity>();
            //var ev = events.GroupBy(x => DateTime.Parse(x.DateStart).Hour).Select(y => y.First()).ToList();
            foreach (var e in events)
            {               
                    Opportunity opportunity = new Opportunity();
                    opportunity.Title = e.Title;
                    opportunity.Description = e.Description;
                    opportunity.DateFrom = DateTime.Parse(e.DateStart);
                    opportunity.DateTo = DateTime.Parse(e.DateEnd);
                    opportunity.EventId = e.EventId;
                    opportunity.CoordsStr = Place.GetPlaceById(e.Place).CoordsStr;
                    opportunity.Image = (e.Images.Any())? e.Images.ToString():"";
                    opportunity.CurrentEvent = new Event(e);
                    opportunity.Tags = e.Tags.Split(new string[] { ", " },StringSplitOptions.RemoveEmptyEntries).ToList();
                    opportunity.TypeVacation = Helper.GetTypeVacationByOpportunity(opportunity);
                    result.Add(opportunity);                 
                //}

                //foreach (var i in events)
                //{
                //    if (DateTime.Parse(i.DateStart).Hour - result[0].DateFrom.Hour >= 2 || result[0].DateFrom.Hour - DateTime.Parse(i.DateStart).Hour >= 2)
                //    {
                //        Opportunity opportunity = new Opportunity();
                //        opportunity.Title = i.Title;
                //        opportunity.Description = i.Description;
                //        opportunity.DateFrom = DateTime.Parse(i.DateStart);
                //        opportunity.DateTo = DateTime.Parse(i.DateEnd);
                //        opportunity.EventId = i.EventId;
                //        opportunity.CoordsStr = Place.GetPlaceById(e.Place).CoordsStr;
                //        result.Add(opportunity);
                //    }
                //}
            }
            return result;
        }

        public static List<Opportunity> GetOpportunitiesForAdministration()
        {
            DbConnect connector = new DbConnect();
            List<Opportunity> opportunities = connector.Opportunities.Where(x=>x.TypeVacation == 2).ToList<Opportunity>();

            return opportunities;
        }

        public static Opportunity AddOpportunity(Opportunity c)
        {
            DbConnect connector = new DbConnect();
            connector.Opportunities.Add(c);//?
            connector.SaveChanges();
            return c;
        }

        public static Opportunity GetOpportunity(int id)
        {
            DbConnect connector = new DbConnect();
            var c = connector.Opportunities.FirstOrDefault(x=>x.OpportunityId == id);//?

            return c;
        }

        public static Opportunity UpdateOpportunity(Opportunity opportunity)
        {
            DbConnect connector = new DbConnect();

            var update = connector.Opportunities.Find(opportunity.OpportunityId);//?
            if (update != null)
            {
                connector.Entry(update).CurrentValues.SetValues(opportunity);
                connector.SaveChanges();
            }

            return update;
        }
    }
}
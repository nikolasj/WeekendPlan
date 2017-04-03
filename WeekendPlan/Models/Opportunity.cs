using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
            int typeVacation, int count, List<Opportunity> opportunity)
        {
            List<Opportunity> result = new List<Opportunity>();

            //объединение списков вариантов фильмы+ивенты+(рестораны)+"свободные" Opportunity
            List<Place> places = Place.GetPlacesForOpportunity(date, user,location: location);
            List<Show> shows = Show.GetShowsForOpportunity(date, user, places, countEvents: count, location: location); //100
            List<Event> events = Event.GetEventsForOpportunity(date, user, countEvents: count, location: location);
            var listEvents = ConvertEventToOpportunity(events); //20
            //MovieId
            List<Opportunity> listShows = ConvertFilmToOpportunity(shows.GroupBy(x => x.MovieId).Select(y => y.First()).ToList());
            var listPlaces = ConvertPlaceToOpportunity(places);

            if (count < 2) count = 2;

            //if (opportunity != null)
            //{
            //    List<Opportunity> tempResult = new List<Opportunity>();
            //    List<Opportunity> tempOpportunity = new List<Opportunity>();
            //    tempResult.AddRange(listEvents);
            //    tempResult.AddRange(listShows);
            //    foreach (Opportunity o in opportunity)
            //    {
            //        tempOpportunity.Add(tempResult.Find(x=>x.Title != o.Title));
            //    }
            //    if (tempResult != null)
            //    {
            //        result.AddRange(tempResult.Take(count / 2));
            //    }   
            //}

            result.AddRange(listEvents.Take(count / 2));
            result.AddRange(listShows.Take(count / 2));

            //generate fake id's


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
                    Opportunity opportunity = new Opportunity();
                    opportunity.Title = e.FilmName;
                    opportunity.Description = e.Description;
                    opportunity.DateFrom = e.Datetime;
                    opportunity.ShowId = e.ShowId;
                    var place = Place.GetPlaceById(e.PlaceId);
                    opportunity.CoordsStr = place.CoordsStr;
                    opportunity.Image = (Film.GetFilms().Find(x => x.Id == e.MovieId).Images.Any())? Film.GetFilms().Find(x => x.Id == e.MovieId).Images.ToString():"";
                    //opportunity.OpportunityId = e.F
                    opportunity.CurrentShow = e;// new Show(e);
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
                if (result.Count == 0)
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
                    result.Add(opportunity);                 
                }

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

    }
}
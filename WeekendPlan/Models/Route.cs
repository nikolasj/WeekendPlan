﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class Route
    {
        [Key]
        [Column("route_id")]
        public Int32 RouteId { get; set; }
        [Column("name")]
        public String Name { get; set; }
        [Column("event_cost")]
        public String EventCost { get; set; }
        [Column("transport_cost")]
        public String TransportCost { get; set; }
        [Column("total_cost")]
        public String TotalCost { get; set; }
        [Column("duration")]
        public String Duration { get; set; }
        [Column("rating")]
        public String Rating { get; set; }
        [Column("user_id")]
        public Int32 UserId { get; set; }
        [Column("route_dates_to")]
        public DateTime RouteDatesTo { get; set; }
        [Column("route_dates_from")]
        public DateTime RouteDatesFrom { get; set; }

        public UserProfile Author { get; set; }
        public List<Opportunity> Opportunities { get; set; }
        public List<Comment> Comments { get; set; }
        public List<String> Tags { get; set; }

        public Route ()
        {

        }

        public Route(List<Opportunity> list, int userId)
        {
            Opportunities = list;
            UserId = userId;

            if (list.Count > 0)
            {
                foreach (var op in list)
                {
                    TotalCost += op.Cost;

                }
            }
        }

        public static List<Route> GetRoutesByDateForUser(UserProfile user, DateTime date, String location,
            int typeVacation, int count, out List<Opportunity> opportunitiesAllByUser,
            List<Tag> tags = null, int? price = null,
            String transport = null, int countPersons = 0, int countEvents = 1, bool allWeather = false)
        {
            List<Route> result = new List<Route>();
            //....
            opportunitiesAllByUser = null;
            for (int i = 0; i < count; i++)
            {
                List<Opportunity> resAll = new List<Opportunity>();
                List<Opportunity> tempOpList =
                    Opportunity.GetOpportunitiesByDateForUser(user, date, location, typeVacation, 
                    Int32.Parse(WebConfigurationManager.AppSettings["DefaultOpportunitiesCount"]), null, out resAll, (tags==null) ? Tag.GetTagsByUser(user.UserId):tags);
                opportunitiesAllByUser = resAll;
                Route temp = new Route(tempOpList, user.UserId);
                temp.RouteDatesFrom = date.Date;
                temp.RouteDatesTo = date.AddDays(1).AddSeconds(-1);
                //temp.Name = "Маршрут на " + temp.RouteDatesFrom.ToShortDateString() + "-" + temp.RouteDatesTo.ToShortDateString() + " номер " + (i + 1).ToString();
                temp.Name = "Маршрут на " + temp.RouteDatesFrom.ToShortDateString();

                result.Add(temp);
            }

            foreach (var r in result)
            {
                r.FillRouteFields();
            }

            return result;
        }

        private void FillRouteFields()
        {
            int EventCost = 0;
            int Duration = 0;

            foreach (var op in Opportunities)
            {
                if (op.CurrentEvent != null)
                {
                    EventCost += String.IsNullOrWhiteSpace(op.CurrentEvent.Price) ? 0 : Helper.GetPriceAverage(op.CurrentEvent.Price);
                    DateTime dateTo = DateTime.MaxValue;
                    DateTime dateFrom = DateTime.MinValue;

                    if (!String.IsNullOrWhiteSpace(op.CurrentEvent.DateEnd))
                    {
                        dateTo = DateTime.Parse(op.CurrentEvent.DateEnd);
                        if (dateTo.Year == 1) dateTo.AddYears(op.DateTo.Year);
                    }

                    if (!String.IsNullOrWhiteSpace(op.CurrentEvent.DateStart))
                    {
                        dateFrom = DateTime.Parse(op.CurrentEvent.DateStart);
                        if (dateFrom.Year == 1)
                        {
                            dateFrom.AddYears(op.DateFrom.Year);
                            if (dateFrom > op.DateFrom) dateFrom.AddYears(-1);
                            if (dateFrom > dateTo) dateFrom.AddYears(-1);
                        }
                    }

                    var timing = dateTo - dateFrom;
                    if (timing.Minutes > 60 * 12) timing = TimeSpan.FromMinutes(Int32.Parse(WebConfigurationManager.AppSettings["DefaultDuration"]));

                    Duration += timing.Minutes;
                }
                //if (op.CurrentPlace != null)
                //    EventCost += String.IsNullOrWhiteSpace(op.CurrentPlace.) ? 0 : Int32.Parse(op.CurrentPlace.Price);
                if (op.CurrentShow != null)
                {
                    EventCost += String.IsNullOrWhiteSpace(op.CurrentShow.Price) ? 0 : Helper.GetPriceAverage(op.CurrentShow.Price);
                    var film = Film.GetFilms().Find(x => x.FilmId == op.CurrentShow.MovieId);
                    if (film != null)
                    {
                        Duration += String.IsNullOrWhiteSpace(film.RunningTime) ? Int32.Parse(WebConfigurationManager.AppSettings["DefaultDuration"]) : Int32.Parse(film.RunningTime);
                    }
                    else
                        Duration += Int32.Parse(WebConfigurationManager.AppSettings["DefaultDuration"]);
                }
            }

            this.Duration = Duration.ToString();
            this.EventCost = EventCost.ToString();
        }

        public static List<Route> GetRoutesByUser(UserProfile user)
        {
            DbConnect connector = new DbConnect();
            List<Route> route = connector.Routes.Where(x => x.UserId == user.UserId).ToList();
            foreach(var r in route)
            {
                r.Opportunities = connector.Opportunities.Where(x => x.RouteId == r.RouteId).ToList();
            }

            return route;
        }

        public static int SaveRouteByUser(Route route)
        {

            DbConnect connector = new DbConnect();
            connector.Routes.Add(route);//?
            connector.SaveChanges();
            return route.RouteId;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeekendPlan.Models;
using WeekendPlan.ViewModels;

namespace WeekendPlan.Controllers
{
    public class OpportunityController : Controller
    {
        public ActionResult Opportunities()
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {
                OpportunityListViewModel opportunityLVM = new OpportunityListViewModel();
                opportunityLVM.Opportunities = new List<OpportunityViewModel>();
                RouteListViewModel routeLVM = new RouteListViewModel();
                routeLVM.Routes = new List<RouteViewModel>();

                if (Session["user_routes"] != null)
                {
                    routeLVM = Session["user_routes"] as RouteListViewModel;

                    int date_start_hour = 0;
                    foreach (var oppor in routeLVM.Routes)
                    {
                        foreach (var o in oppor.Opportunities)
                        {
                            OpportunityViewModel opportunityVM = new OpportunityViewModel(o);
                            if (o.DateFrom != null)
                            {
                                date_start_hour = Int32.Parse(Helper.ConvertDateStartHourToInt(o.DateFrom.ToString()));
                            }
                            if (date_start_hour != 0)
                            {
                                opportunityVM.TimeHour = date_start_hour;
                            }
                            opportunityLVM.Opportunities.Add(opportunityVM);
                        }
                    }

                    Session["user_opportunities"] = opportunityLVM;

                    return View("Opportunities", opportunityLVM);
                }
                //-------------------------------

                ViewBag.Date = (Request.Form["Date"] == null) ? DateTime.Now.AddDays(1).ToString("s") : Request.Form["Date"];
                DateTime date = DateTime.Parse(ViewBag.Date);
                List<City> city = City.GetCities();
                city.Add(new City() { CityId = 0, Name = "<Нет>" });
                var cityId = (Request.Form["Cities"] != null) ? Int32.Parse(Request.Form["Cities"]) : user.City;
                ViewBag.DropDownValuesCities = new SelectList(city, "CityId", "Name", cityId);
                opportunityLVM.TypeVacation = (Request.Form["TypeVacation"] != null) ? Int32.Parse(Request.Form["TypeVacation"]) : 1;

                //ViewBag.DropDownValuesTypeVacation = new SelectList()
                //-------------------------------
                if (Session["user_opportunities"] == null)
                {
                    String location = City.GetCities().Find(x => x.CityId == cityId).Slug;
                    List<Opportunity> opportunity = Opportunity.GetOpportunitiesByDateForUser(user, date, location, opportunityLVM.TypeVacation, 4, null);
                    int date_start_hour = 0;
                    foreach (var o in opportunity)
                    {
                        OpportunityViewModel opportunityVM = new OpportunityViewModel(o);
                        if (o.DateFrom != null)
                        {
                            date_start_hour = Int32.Parse(Helper.ConvertDateStartHourToInt(o.DateFrom.ToString()));
                        }
                        if (date_start_hour != 0)
                        {
                            opportunityVM.TimeHour = date_start_hour;
                        }
                        opportunityLVM.Opportunities.Add(opportunityVM);
                    }

                    Session["user_opportunities"] = opportunityLVM;
                }
                else
                {
                    opportunityLVM = Session["user_opportunities"] as OpportunityListViewModel;
                }

                return View("Opportunities", opportunityLVM);
            }
            return View();
        }

        public ActionResult Routes()
        {
            //Session["user_routes"] = null;
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {
                RouteListViewModel routeLVM = new RouteListViewModel();
                routeLVM.Routes = new List<RouteViewModel>();

                //-------------------------------

                ViewBag.Date = DateTime.Now.AddDays(1).ToString("s");
                DateTime date = DateTime.Parse(ViewBag.Date);
                List<City> city = City.GetCities();
                city.Add(new City() { CityId = 0, Name = "<Нет>" });
                var cityId = user.City;
                ViewBag.DropDownValuesCities = new SelectList(city, "CityId", "Name", cityId);
                routeLVM.TypeVacation = 1;
                var tags = Tag.GetTagsByUser(user.UserId);
                routeLVM.TagsByUser = tags.Select(x => x.Text).ToList();
                //-------------------------------

                String location = City.GetCities().Find(x => x.CityId == cityId).Slug;
                //DateTime concreteDate, UserProfile user, List<Tag> tags = null, int? price = null,
                //String location = null, String transport = null, int countPersons = 0, int countEvents = 1, bool allWeather = false)
                // routeLVM = GetRoutesByParameters(routeLVM, user, date, location, tags);

                return View("Routes", routeLVM);
            }
            return View();
        }

        public ActionResult SearchRoute()
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {
                RouteListViewModel routeLVM = new RouteListViewModel();
                routeLVM.Routes = new List<RouteViewModel>();

                ViewBag.Date = (Request.Form["Date"] == null) ? DateTime.Now.AddDays(1).ToString("s") : Request.Form["Date"];
                DateTime date = DateTime.Parse(ViewBag.Date);
                //--City
                List<City> city = City.GetCities();
                city.Add(new City() { CityId = 0, Name = "<Нет>" });
                var cityId = (Request.Form["Cities"] != null) ? Int32.Parse(Request.Form["Cities"]) : user.City;
                ViewBag.DropDownValuesCities = new SelectList(city, "CityId", "Name", cityId);

                routeLVM.TypeVacation = (Request.Form["TypeVacation"] != null) ? Int32.Parse(Request.Form["TypeVacation"]) : 1;
                String[] arrayTags = Request.Form["inputTags"].Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                List<Tag> newTags = new List<Tag>();
                //--Transport
                routeLVM.TypeTransport = (Request.Form["TypeTransport"] != null) ? Int32.Parse(Request.Form["TypeTransport"]) : 1;
                //--AllWeather
                //routeLVM.AllWeather

                foreach (String tagValue in arrayTags)
                {
                    Tag tag = new Tag() { Text = tagValue.Trim(), UserId = user.UserId };
                    newTags.Add(tag);
                }
                routeLVM.TagsByUser = arrayTags.ToList();
                String location = City.GetCities().Find(x => x.CityId == cityId).Slug;

                routeLVM = GetRoutesByParameters(routeLVM, user, date, location, newTags);

                return View("Routes", routeLVM);
            }
            return View();
        }

        private RouteListViewModel GetRoutesByParameters(RouteListViewModel routeLVM, UserProfile user, DateTime date, 
            String location, List<Tag> tags)
        {
            List<Route> routes = Route.GetRoutesByDateForUser(user, date, location, routeLVM.TypeVacation, 1, tags);
            int date_start_hour = 0;
            foreach (var r in routes)
            {
                RouteViewModel routeVM = new RouteViewModel(r);
                if (r.RouteDatesFrom != null)
                {
                    date_start_hour = Int32.Parse(Helper.ConvertDateStartHourToInt(r.RouteDatesFrom.ToString()));
                }
                //if (date_start_hour != 0)
                //{
                //    routeVM.TimeHour = date_start_hour;
                //}
                routeLVM.Routes.Add(routeVM);
            }
            
            Session["user_routes"] = routeLVM;
            return routeLVM;
        }

        public ActionResult GetPlaceCoords()
        {
            List<Coords> result = new List<Coords>();
            var opportunityLVM = Session["user_opportunities"] as OpportunityListViewModel;

            foreach (var c in opportunityLVM.Opportunities)
            {
                if (c.Coords != null)
                {
                    if (c.Coords.Lat != null && c.Coords.Lon != null)
                    {
                        Coords coords = new Coords() { Content = c.Title, Description = c.Description, Lat = c.Coords.Lat, Lon = c.Coords.Lon };
                        result.Add(coords);
                    }
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MarkOpportunityDelete(int id)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {
                OpportunityListViewModel opportunityLVM = new OpportunityListViewModel();
                opportunityLVM.Opportunities = new List<OpportunityViewModel>();
                if (Session["user_opportunities"] != null)
                {
                    opportunityLVM = Session["user_opportunities"] as OpportunityListViewModel;
                    opportunityLVM.Opportunities.Remove(opportunityLVM.Opportunities.Find(x => x.OpportunityId == id));
                    Session["user_opportunities"] = opportunityLVM;
                }

                return RedirectToAction("Opportunities", "Opportunity");
            }
            return View();
        }

    }
}
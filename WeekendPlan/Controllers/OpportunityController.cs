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

                //-------------------------------
               
                ViewBag.Date = (Request.Form["Date"]==null)? DateTime.Now.AddDays(1).ToString("s"): Request.Form["Date"];
                DateTime date = DateTime.Parse(ViewBag.Date);
                List<City> city = City.GetCities();
                city.Add(new City() { CityId = 0, Name = "<Нет>" });
                var cityId = (Request.Form["Cities"] != null) ? Int32.Parse(Request.Form["Cities"]) : user.City;
                ViewBag.DropDownValuesCities = new SelectList(city, "CityId", "Name", cityId);
                opportunityLVM.TypeVacation = (Request.Form["TypeVacation"] !=null)?Int32.Parse(Request.Form["TypeVacation"]):1;

                //ViewBag.DropDownValuesTypeVacation = new SelectList()
                //-------------------------------
                if (Session["user_opportunities"] == null)
                {
                    String location = City.GetCities().Find(x => x.CityId==cityId).Slug;
                    List<Opportunity> opportunity = Opportunity.GetOpportunitiesByDateForUser(user, date, location, opportunityLVM.TypeVacation, 4);
                    int date_start_hour = 0;
                    foreach (var o in opportunity)
                    {
                        OpportunityViewModel opportunityVM = new OpportunityViewModel(o);
                        if (o.DateFrom != null)
                        {
                            date_start_hour = Int32.Parse(Opportunity.ConvertDateStartHourToInt(o.DateFrom.ToString()));
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
                
                return View("Opportunities",opportunityLVM);
            }
            return View();
        }

        public ActionResult Routes()
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {
                RouteListViewModel routeLVM = new RouteListViewModel();
                routeLVM.Routes = new List<RouteViewModel>();

                //-------------------------------

                ViewBag.Date = (Request.Form["Date"] == null) ? DateTime.Now.AddDays(1).ToString("s") : Request.Form["Date"];
                DateTime date = DateTime.Parse(ViewBag.Date);
                List<City> city = City.GetCities();
                city.Add(new City() { CityId = 0, Name = "<Нет>" });
                var cityId = (Request.Form["Cities"] != null) ? Int32.Parse(Request.Form["Cities"]) : user.City;
                ViewBag.DropDownValuesCities = new SelectList(city, "CityId", "Name", cityId);
                routeLVM.TypeVacation = (Request.Form["TypeVacation"] != null) ? Int32.Parse(Request.Form["TypeVacation"]) : 1;

                //ViewBag.DropDownValuesTypeVacation = new SelectList()
                //-------------------------------
                if (Session["user_routes"] == null)
                {
                    String location = City.GetCities().Find(x => x.CityId == cityId).Slug;
                    List<Route> routes = Route.GetRoutesByDateForUser(user, date, location, routeLVM.TypeVacation, 1);
                    int date_start_hour = 0;
                    foreach (var r in routes)
                    {
                        RouteViewModel routeVM = new RouteViewModel(r);
                        if (r.RouteDatesFrom != null)
                        {
                            date_start_hour = Int32.Parse(Opportunity.ConvertDateStartHourToInt(r.RouteDatesFrom.ToString()));
                        }
                        //if (date_start_hour != 0)
                        //{
                        //    routeVM.TimeHour = date_start_hour;
                        //}
                        routeLVM.Routes.Add(routeVM);
                    }

                    Session["user_routes"] = routeLVM;
                }
                else
                {
                    routeLVM = Session["user_routes"] as RouteListViewModel;
                }

                return View("Routes", routeLVM);
            }
            return View();
        }

        public ActionResult GetPlaceCoords()
        {
            List<Coords> result = new List<Coords>();          
            var opportunityLVM = Session["user_opportunities"] as OpportunityListViewModel;

            foreach(var c in opportunityLVM.Opportunities)
            {
                if (c.Coords != null)
                {
                    if (c.Coords.Lat !=null && c.Coords.Lon != null)
                    {
                        Coords coords = new Coords() { Content = c.Title, Description = c.Description, Lat = c.Coords.Lat, Lon = c.Coords.Lon };
                        result.Add(coords);
                    }
                }
            }

           return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult SomeActionMethod()
        //{
        //    return Content("hello world!");
        //}

        //public ActionResult SomeActionMethod()
        //{
        //    return Content("hello world!");
        //}
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
                    opportunityLVM.Opportunities.Remove(opportunityLVM.Opportunities.Find(x=>x.OpportunityId == id));
                    Session["user_opportunities"] = opportunityLVM;
                }

                return RedirectToAction("Opportunities", "Opportunity");
            }
            return View();
        }

        //public ActionResult MarkTaskUndone(int id)
        //{
        //    Task t = Task.GetTasks().Find(x => x.Task_id == id);
        //    UserProfile u = UserProfile.GetUsers().Find(x => x.UserName.ToLower() == User.Identity.Name.ToLower());

        //    if (Done.GetTodayDone().Where(x => x.Task_id == id && x.User_id == u.UserId) != null)
        //    {
        //        Done task = Done.GetTodayDone().Where(x => x.Task_id == id && x.User_id == u.UserId).Last();
        //        if (task != null)
        //        {
        //            Done.DeleteDone(task);
        //        }
        //    }
        //    return TaskCalendar();

        //}
    }
}
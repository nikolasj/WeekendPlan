using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeekendPlan.Models;
using WeekendPlan.ViewModels;

namespace WeekendPlan.Controllers
{
    public class EventController : Controller
    {
        public ActionResult EventsView()
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {
                EventListViewModel eventLVM = new EventListViewModel();
                eventLVM.Events = new List<EventViewModel>();

                foreach (Event e in Event.GetEvents())
                {
                    EventViewModel eventVM = new EventViewModel(e);
                    //filmVM.FilmId = f.FilmId;
                    //filmVM.Title = f.Title;
                    //filmVM.Poster = f.Poster;

                    eventLVM.Events.Add(eventVM);
                }

                return View("EventsView", eventLVM);
            }
            return View();
        }

        public ActionResult EventDetails(int? id)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (id == null)
            {
                return HttpNotFound();
            }

            Event ev = Event.GetEvents().FirstOrDefault(x => x.EventId == id);
            if (ev != null)
            {
                EventViewModel eventVM = new EventViewModel(ev);
                return View("EventDetails", eventVM);
            }
            else
            {
                return View("EventDetails", "???");
            }
        }
    }
}
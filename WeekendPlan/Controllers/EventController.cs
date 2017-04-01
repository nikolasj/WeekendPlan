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

        public ActionResult EventsViewByCount(int id)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {
                EventListViewModel eventLVM = new EventListViewModel();
                eventLVM.Events = new List<EventViewModel>();
                int count = 0;
                int idFinish = id + 3;
                foreach (Event e in Event.GetEvents())
                {
                    count++;
                    if (count >= id && count <= idFinish)
                    {
                        EventViewModel eventVM = new EventViewModel(e,user.UserId);
                        eventLVM.Tags = TagEvent.GetTags();
                        eventLVM.Events.Add(eventVM);
                    }
                }
                eventLVM.Count = count;
                eventLVM.CurrentNumber = id;
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
                EventViewModel eventVM = new EventViewModel(ev,user.UserId);
                return View("EventDetails", eventVM);
            }
            else
            {
                return View("EventDetails", "???");
            }
        }

        public ActionResult EventComments(string id, string btnSave)
        {
            switch (btnSave)
            {
                case "save":
                    if (ModelState.IsValid)
                    {
                        UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
                        String commentText = Request.Form["Comment"];
                        Comment comment = new Comment() { Text = commentText, UserId = user.UserId, ParentId = null };
                        var c = Comment.AddComment(comment);
                        CommentEvent commentEvent = new CommentEvent { EventId = Int32.Parse(id), CommentId = comment.CommentId };
                        var cf = CommentEvent.AddComment(commentEvent);                    
                    }
                    return EventDetails(Int32.Parse(id));
            }
            return View();
        }

        //public ActionResult EventTags(string id, string btnSave)
        //{
        //    switch (btnSave)
        //    {
        //        case "save":
        //            if (ModelState.IsValid)
        //            {
        //                UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
        //                String tagText = Request.Form["Tag"];
        //                Tag tag = new Tag() { Text = tagText, UserId = user.UserId };
        //                var c = Tag.AddTag(tag);
        //                TagEvent tagEvent = new TagEvent { EventId = Int32.Parse(id), TagId = tag.TagId };
        //                var cf = TagEvent.AddTag(tagEvent);
        //            }
        //            return EventsView();
        //    }
        //    return View();
        //}

        public ActionResult EventTags(string id, string btnSave)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            switch (btnSave)
            {
                case "save":
                    if (ModelState.IsValid)
                    {
                        UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
                        String tagText = Request.Form["inputTags"];

                        Event p = Event.GetEvents().FirstOrDefault(x => x.EventId == Int32.Parse(id));
                        var eventTags = p.GetTagsByUser(user.UserId);

                        List<Tag> listToAdd = new List<Tag>();
                        List<Tag> listToDelete = new List<Tag>();

                        List<Tag> newTags = new List<Tag>();
                        String[] arrayTags = tagText.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (String tagValue in arrayTags)
                        {
                            Tag tag = new Tag() { Text = tagValue.Trim(), UserId = user.UserId };
                            newTags.Add(tag);
                        }

                        foreach (var k in newTags)
                        {
                            var place = eventTags.FirstOrDefault(x => x.Text == k.Text && (k.UserId == x.UserId));
                            if (place == null)
                                listToAdd.Add(k);
                        }

                        foreach (var t in listToAdd)
                        {
                            var addTag = Tag.AddTag(t);
                            if (addTag != null)
                            {
                                TagEvent addTagEvent = new TagEvent() { TagId = addTag.TagId, EventId = Int32.Parse(id) };
                                var addTagPl = TagEvent.AddTag(addTagEvent);
                            }
                        }

                        foreach (var k in eventTags)
                        {
                            var place = newTags.FirstOrDefault(x => x.Text == k.Text && (k.UserId == x.UserId));
                            if (place == null)
                                listToDelete.Add(k);
                        }

                        foreach (var t in listToDelete)
                        {
                            var deleteTagPlace = TagEvent.DeleteTagEvent(t);
                            if (deleteTagPlace)
                            {
                                var deleteTag = Tag.DeleteTag(t);
                            }
                        }

                    }
                    return EventDetails(Int32.Parse(id));
            }
            return View();
        }
    }
}
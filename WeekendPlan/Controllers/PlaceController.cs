using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeekendPlan.Models;
using WeekendPlan.ViewModels;

namespace WeekendPlan.Controllers
{
    public class PlaceController: Controller
    {

        public ActionResult PlacesViewByCount(int id)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {
                PlaceListViewModel placeLVM = new PlaceListViewModel();
                placeLVM.Places = new List<PlaceViewModel>();
                int count = 0;
                int idFinish = id + 3;
                var places = Place.GetPlaces().Where(x => x.Location == user.Location);
                foreach (Place p in places)
                {
                    count++;
                    if (count >= id && count <= idFinish)
                    {
                        PlaceViewModel placeVM = new PlaceViewModel(p,user.UserId);
                        placeLVM.Places.Add(placeVM);
                    }
                }
                placeLVM.Count = count;
                placeLVM.CurrentNumber = id;
                placeLVM.Tags = TagPlace.GetTags();
                return View("PlacesView", placeLVM);
            }
            return View();
        }

        public ActionResult PlaceDetails(int? id)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (id == null)
            {
                return HttpNotFound();
            }

            Place p = Place.GetPlaces().FirstOrDefault(x => x.PlaceId == id);
            if (p != null)
            {
                PlaceViewModel placeVM = new PlaceViewModel(p, user.UserId);
                //if (placeVM.Tags == null) placeVM.Tags = placeVM.TagsKudaGo.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries).ToList()
                //        .Select(x=>new Tag() { Text = x.Trim(), UserId = 0 }).ToList();
                return View("PlaceDetails", placeVM);
            }
            else
            {
                return View("PlaceDetails", "???");
            }
        }

        public ActionResult PlaceComments(string id, string btnSave)
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
                        CommentPlace commentPlace = new CommentPlace { PlaceId = Int32.Parse(id), CommentId = comment.CommentId };
                        var cf = CommentPlace.AddComment(commentPlace);
                    }
                    return PlaceDetails(Int32.Parse(id));
            }
            return View();
        }

        public ActionResult PlaceTags(string id, string btnSave)
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
                        
                        Place p = Place.GetPlaces().FirstOrDefault(x => x.PlaceId == Int32.Parse(id));
                        var placeTags = p.GetTagsByUser(user.UserId);

                        List<Tag> listToAdd = new List<Tag>();
                        List<Tag> listToDelete = new List<Tag>();

                        List<Tag> newTags = new List<Tag>();
                        String[] arrayTags = tagText.Split(new char[] { '#' },StringSplitOptions.RemoveEmptyEntries);
                        foreach(String tagValue in arrayTags)
                        {
                            Tag tag = new Tag() { Text = tagValue.Trim(), UserId = user.UserId };
                            newTags.Add(tag);
                        }

                        foreach (var k in newTags)
                        {
                            var place = placeTags.FirstOrDefault(x => x.Text == k.Text && (k.UserId == x.UserId));
                            if (place == null)
                                listToAdd.Add(k);
                        }

                        foreach(var t in listToAdd)
                        {
                            var addTag = Tag.AddTag(t);
                            if (addTag !=null)
                            {
                                TagPlace addTagPlace = new TagPlace() { TagId = addTag.TagId, PlaceId = Int32.Parse(id) };
                                var addTagPl = TagPlace.AddTag(addTagPlace);
                            }
                        }

                        foreach (var k in placeTags)
                        {
                            var place = newTags.FirstOrDefault(x => x.Text == k.Text && (k.UserId == x.UserId));
                            if (place == null)
                                listToDelete.Add(k);
                        }

                        foreach(var t in listToDelete)
                        {
                            var deleteTagPlace = TagPlace.DeleteTagPlace(t);
                            if (deleteTagPlace)
                            {
                                var deleteTag = Tag.DeleteTag(t);
                            }
                        }

                    }
                    return PlaceDetails(Int32.Parse(id));
            }
            return View();
        }
    }
}
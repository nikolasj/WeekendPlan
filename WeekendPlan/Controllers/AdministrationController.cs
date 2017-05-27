using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeekendPlan.Models;
using WeekendPlan.ViewModels;

namespace WeekendPlan.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministrationController : Controller
    {

        [Authorize(Roles = "admin")]
        public ActionResult Administration()
        {
            AdministrationListViewModel profileLVM;
            profileLVM = GetUsers();

            return View("Administration", profileLVM);
        }

        private AdministrationListViewModel GetUsers()
        {
            List<UserProfile> users = UserProfile.GetUsers();
            List<Opportunity> opportunities = Opportunity.GetOpportunitiesForAdministration();
            AdministrationListViewModel profileLVM = new AdministrationListViewModel();
            profileLVM.Profiles = new List<ProfileViewModel>();
            profileLVM.Opportunities = new List<OpportunityViewModel>();

            foreach (var user in users)
            {
                ProfileViewModel profileVM = new ProfileViewModel(user);
                profileLVM.Profiles.Add(profileVM);
            }

            foreach (var o in opportunities)
            {
                OpportunityViewModel opportunityVM = new OpportunityViewModel(o);
                profileLVM.Opportunities.Add(opportunityVM);
            }

            return profileLVM;
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdministrationProfileView(int id)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.UserId == id);
            if (user != null)
            {

                ProfileViewModel profileVM = new ProfileViewModel(user);

                List<City> city = City.GetCities();
                city.Add(new City() { CityId = 0, Name = "<Нет>" });
                var uId = (user.City == null) ? 0 : user.City;
                ViewBag.DropDownValuesCities = new SelectList(city, "CityId", "Name", uId);
                profileVM.GetTagsByUser();
                profileVM.webRoot = Server.MapPath("~/");

                return View("AdministrationProfileView", profileVM);

            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdministrationCommentsByUser(int id)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.UserId == id);
            if (user != null)
            {
                ProfileViewModel profileVM = new ProfileViewModel(user);
                profileVM.Comments = UserProfile.GetCommentsByUser(user.UserId);
                return View("AdministrationCommentsByUser", profileVM);
            }

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdministrationUpdateOrDeleteComment(int id)
        {
            Comment comment = Comment.GetComment(id);
            CommentViewModel commentVM = new CommentViewModel(comment);
            return View("AdministrationUpdateOrDeleteComment", commentVM);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdministrationComments(int id, string btn)
        {
            switch (btn)
            {
                case "save":
                    if (ModelState.IsValid)
                    {
                        // var userId = Comment.GetUserIdByCommentId(id);
                        //UserProfile user = UserProfile.GetUser(userId);
                        String commentText = Request.Form["Comment"];
                        var c = Comment.UpdateComment(id, commentText);

                    }
                    return Administration();
                case "delete":
                    if (ModelState.IsValid)
                    {
                        var c = Comment.DeleteComment(id);
                    }
                    return Administration();
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdministrationOpportunity()
        {
            OpportunityViewModel opportunityVM = new OpportunityViewModel();
            return View("AdministrationOpportunity", opportunityVM);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdministrationOppor(int id)
        {
            Opportunity opportunity = Opportunity.GetOpportunity(id);
            OpportunityViewModel opportunityVM = new OpportunityViewModel(opportunity);
            return View("AdministrationOpportunityUpdate", opportunityVM);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdministrationCreateOpportunity()
        {
            var title = Request.Form["Title"];
            var description = Request.Form["Description"];
            var duration = Request.Form["Duration"];
            var cost = Request.Form["Cost"];

            Opportunity opportunity = new Opportunity
            {
                Title = title,
                Description = description,
                Duration = duration,
                Cost = cost,
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(100),
                TypeVacation = 2
            };
            var cf = Opportunity.AddOpportunity(opportunity);

            return Administration();
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdministrationOpportunityUpdate(int id)
        {
            var title = Request.Form["Title"];
            var description = Request.Form["Description"];
            var duration = Request.Form["Duration"];
            var cost = Request.Form["Cost"];

            Opportunity opportunity = new Opportunity
            {
                OpportunityId = id,
                Title = title,
                Description = description,
                Duration = duration,
                Cost = cost,
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(100),
                TypeVacation = 2
            };
            var cf = Opportunity.UpdateOpportunity(opportunity);

            return Administration();
        }
    }
}
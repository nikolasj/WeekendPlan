using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeekendPlan.Models;
using WeekendPlan.ViewModels;

namespace WeekendPlan.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult ProfileView()
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {

                ProfileViewModel profileVM = new ProfileViewModel(user);

                List<City> city = City.GetCities();
                city.Add(new City() { CityId = 0, Name = "<Нет>" });
                var id = (user.City == null) ? 0 : user.City;
                ViewBag.DropDownValuesCities = new SelectList(city, "CityId", "Name", id);
                profileVM.GetTagsByUser();
                profileVM.webRoot = Server.MapPath("~/");

                return View("ProfileView", profileVM);

                //List<Genres> genres = Genres.GetGenres();
                //genres.Add(new Genres() { GenresId = 0, Name = "<All>" });
                //ViewBag.DropDownValuesGenre = new SelectList(genres, "name", "name", "<All>");

                //return View("FilmsView", filmLVM);
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChangeProfile(FormCollection formValues, int id)
        {
            //if (ModelState.IsValid)
            //{
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            user.Name = Request.Form["UserName"];
            City c = City.GetCities().Find(x => x.CityId == Int32.Parse(Request.Form["Cities"]));
            user.City = c.CityId;
            if (Request.Form["UserCar"] != null)
            {
                user.Car = (Request.Form["UserCar"] == "on") ? true : false;
            }
            else
                user.Car = false;
            user.Picture = (Request.Form["files"] == "") ? user.Picture : Request.Form["files"];
            if (Request.Form["UserDriverLicense"] != null)
            {
                user.DriverLicense = (Request.Form["UserDriverLicense"] == "on") ? true : false;
            }
            else
                user.DriverLicense = false;

            var uploads = Path.Combine(Server.MapPath("~/"), "Content\\Images\\UserPicture");

            user.GroupCount = Int32.Parse(Request.Form["GroupCount"]);

            var userProfile = UserProfile.UpdateUser(user);
            List<City> city = City.GetCities();
           
            city.Add(new City() { CityId = 0, Name = "<Нет>" });
            ViewBag.DropDownValuesCities = new SelectList(city, "CityId", "Name", c.CityId);
            ProfileViewModel profileVM = new ProfileViewModel(userProfile);
            profileVM.webRoot = Server.MapPath("~/");
            return View("ProfileView", profileVM);

        }

        public ActionResult CommentsByUser()
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {
                ProfileViewModel profileVM = new ProfileViewModel(user);
                profileVM.GetCommentsByUser();
                return View("CommentsByUser", profileVM);
            }

            return View();
        }

        public ActionResult TagsByUser()
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {
                ProfileViewModel profileVM = new ProfileViewModel(user);
                profileVM.GetTagsByUser();
                return View("TagsByUser", profileVM);
            }

            return View();
        }

        public ActionResult UsersView()
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {
                ProfileListViewModel profileLVM = new ProfileListViewModel();
                profileLVM.Profiles = new List<ProfileViewModel>();
                //List<int> group = new List<int>();
                //group.Add(new String() { GenresId = 0, Name = "<Не задано>" });
                //ViewBag.DropDownValuesGroup = new SelectList(group, "GenresId", "name", id);
                foreach (UserProfile u in UserProfile.GetUsers())
                {
                    ProfileViewModel profileVM = new ProfileViewModel(u);
                    profileLVM.Profiles.Add(profileVM);
                }
                return View("UsersView", profileLVM);
            }

            return View();
        }

        public ActionResult Relatives(int id)
        {
            var user = UserProfile.GetUser(id);
            
            return UsersView();

        }

    }
}
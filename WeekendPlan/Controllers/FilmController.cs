using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeekendPlan.Models;
using WeekendPlan.ViewModels;

namespace WeekendPlan.Controllers
{
    public class FilmController : Controller
    {
        public ActionResult FilmsView()
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user !=null)
            {
                FilmListViewModel filmLVM = new FilmListViewModel();
                filmLVM.Films = new List<FilmViewModel>();

                foreach (Film f in Film.GetFilms())
                {
                    FilmViewModel filmVM = new FilmViewModel(f);
                    filmLVM.Films.Add(filmVM);
                }
                List<Genres> genres = Genres.GetGenres();
                genres.Add(new Genres() { GenresId = 0, Name = "<All>" });
                ViewBag.DropDownValuesGenre = new SelectList(genres, "name", "name", "<All>");

                return View("FilmsView", filmLVM);
            }
            return View();
        }

        public ActionResult FilmDetails(int? id)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (id == null)
            {
                return HttpNotFound();
            }



            Film film = Film.GetFilms().FirstOrDefault(x => x.FilmId == id);
            if (film != null)
            {
                FilmViewModel filmVM = new FilmViewModel(film);
                return View("FilmDetails", filmVM);
            }
            else
            {
                return View("FilmDetails", "???");
            }
        }

        public ActionResult FilmSelectGenre(Film genre_id, string btnGenre)
        {
            switch(btnGenre)
            {
                case "selectGenre":
                    if (ModelState.IsValid)
                    {
                        genre_id.Genres = Request.Form["Genres"];
                        FilmListViewModel filmLVM = new FilmListViewModel();
                        filmLVM.Films = new List<FilmViewModel>();
                        Int32 id = Int32.Parse(genre_id.Genres);
                        Genres g = Genres.GetGenres().Find(x=>x.GenresId == id);
                        foreach (Film film in Film.GetFilms().Where(x=>x.Genres==g.Name))
                        {
                            FilmViewModel filmVM = new FilmViewModel(film);
                            filmLVM.Films.Add(filmVM);
                        }
                        List<Genres> genres = Genres.GetGenres();
                        genres.Add(new Genres() { GenresId = 0, Name = "<Не задано>" });
                        ViewBag.DropDownValuesGenre = new SelectList(genres, "GenresId", "name", id);
                        return View("FilmsView", filmLVM);
                    }
                    return View();
            }
            return View();
        }

        public ActionResult GenreFilmsView(String id)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {
                FilmListViewModel filmLVM = new FilmListViewModel();
                filmLVM.Films = new List<FilmViewModel>();

               foreach (Film f in Film.GetFilms().Where(x=>x.Genres.Contains(id)))
                {
                    FilmViewModel filmVM = new FilmViewModel(f);
                    filmLVM.Films.Add(filmVM);
                }
                List<Genres> genres = Genres.GetGenres();
                genres.Add(new Genres() { GenresId = 0, Name = "<Не задано>" });
                ViewBag.DropDownValuesGenre = new SelectList(genres, "name", "name", id);

                return View("FilmsView", filmLVM);
            }
            return View();

        }

        public ActionResult FilmComments(string id, string btnSave)
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
                        CommentFilm commentFilm = new CommentFilm { FilmId = Int32.Parse(id), CommentId = comment.CommentId };
                        var cf = CommentFilm.AddComment(commentFilm);
                        //FilmListViewModel filmLVM = new FilmListViewModel();
                        //filmLVM.Films = new List<FilmViewModel>();
                        //Int32 id = Int32.Parse(genre_id.Genres);
                        //Genres g = Genres.GetGenres().Find(x => x.GenresId == id);
                        //foreach (Film film in Film.GetFilms().Where(x => x.Genres == g.Name))
                        //{
                        //    FilmViewModel filmVM = new FilmViewModel(film);
                        //    filmLVM.Films.Add(filmVM);
                        //}
                        //List<Genres> genres = Genres.GetGenres();
                        //genres.Add(new Genres() { GenresId = 0, Name = "<Не задано>" });
                        //ViewBag.DropDownValuesGenre = new SelectList(genres, "GenresId", "name", id);
                        //return View("FilmsView", filmLVM);
                    }
                    return View();
            }
            return View();
        }
    }
}
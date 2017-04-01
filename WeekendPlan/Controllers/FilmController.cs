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

        public ActionResult FilmsViewByCount(int id)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            if (user != null)
            {
                FilmListViewModel filmLVM = new FilmListViewModel();
                filmLVM.Films = new List<FilmViewModel>();
                int count = 0;
                int idFinish = id + 3;

                foreach (Film f in Film.GetFilms())
                {
                    count++;
                    if (count >= id && count <= idFinish)
                    {
                        FilmViewModel filmVM = new FilmViewModel(f, user.UserId);
                        filmLVM.Films.Add(filmVM);
                    }
                }
                filmLVM.Count = count;
                filmLVM.CurrentNumber = id;
                List<Genres> genres = Genres.GetGenres();
                genres.Add(new Genres() { GenresId = 0, Name = "<All>" });
                ViewBag.DropDownValuesGenre = new SelectList(genres, "name", "name", "<All>");
                filmLVM.Tags = TagFilm.GetTags();
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
                FilmViewModel filmVM = new FilmViewModel(film,user.UserId);
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
                            FilmViewModel filmVM = new FilmViewModel(film,null);
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
                    FilmViewModel filmVM = new FilmViewModel(f,user.UserId);
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
                    }
                    return View();
            }
            return View();
        }

        public ActionResult FilmTags(string id, string btnSave)
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

                        Film p = Film.GetFilms().FirstOrDefault(x => x.FilmId == Int32.Parse(id));
                        var filmTags = p.GetTagsByUser(user.UserId);

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
                            var place = filmTags.FirstOrDefault(x => x.Text == k.Text && (k.UserId == x.UserId));
                            if (place == null)
                                listToAdd.Add(k);
                        }

                        foreach (var t in listToAdd)
                        {
                            var addTag = Tag.AddTag(t);
                            if (addTag != null)
                            {
                                TagFilm addTagFilm = new TagFilm() { TagId = addTag.TagId, FilmId = Int32.Parse(id) };
                                var addTagPl = TagFilm.AddTag(addTagFilm);
                            }
                        }

                        foreach (var k in filmTags)
                        {
                            var place = newTags.FirstOrDefault(x => x.Text == k.Text && (k.UserId == x.UserId));
                            if (place == null)
                                listToDelete.Add(k);
                        }

                        foreach (var t in listToDelete)
                        {
                            var deleteTagPlace = TagFilm.DeleteTagFilm(t);
                            if (deleteTagPlace)
                            {
                                var deleteTag = Tag.DeleteTag(t);
                            }
                        }

                    }
                    return FilmDetails(Int32.Parse(id));
            }
            return View();
        }
    }
}
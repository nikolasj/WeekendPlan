using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeekendPlan.Models;
using WeekendPlan.ViewModels;

namespace WeekendPlan.Controllers
{
    public class SearchController : Controller
    {
        [Authorize]
        public ActionResult Search(string tag, string type, string text, string page)
        {
            SearchListViewModel searchLVM = new SearchListViewModel();
            searchLVM.Films = new List<FilmViewModel>();
            searchLVM.Events = new List<EventViewModel>();
            searchLVM.Places = new List<PlaceViewModel>();


            int? tagId = String.IsNullOrWhiteSpace(tag) ? null : (int?)Tag.GetTagIdByText(tag);

            switch (type)
            {
                case "film":
                    // -- TagFilm
                    searchLVM = FillFilmsByTagId(tagId, searchLVM);

                    break;
                case "event":
                    // -- TagEvent
                    searchLVM = FillEventsByTagId(tagId, searchLVM); 
                    break;
                case "place":
                    // -- TagPlace
                    searchLVM = FillPlacesByTagId(tagId, searchLVM);
                    break;
                default:
                    searchLVM = FillEventsByTagId(tagId, searchLVM);
                    searchLVM = FillFilmsByTagId(tagId, searchLVM);
                    searchLVM = FillPlacesByTagId(tagId, searchLVM); 
                    break;
            }

            if (String.IsNullOrWhiteSpace(text))
                text = Request.Form["searchTitle"];
            if (!String.IsNullOrWhiteSpace(text))
                searchLVM = FilterByTitle(text, searchLVM, type);

            switch (type)
            {
                case "film":
                    //FilterByPageId(searchLVM.Films...)
                    FilterFilmsByPageId(searchLVM, String.IsNullOrWhiteSpace(page) ? 1 : Int32.Parse(page));
                   
                    List<Genres> genres = Genres.GetGenres();
                    genres.Add(new Genres() { GenresId = 0, Name = "<All>" });
                    ViewBag.DropDownValuesGenre = new SelectList(genres, "name", "name", "<All>");
                    searchLVM.Tags = TagFilm.GetTags();
                    break;
                case "event":
                    FilterEventsByPageId(searchLVM, String.IsNullOrWhiteSpace(page) ? 1 : Int32.Parse(page));
                    searchLVM.Tags = TagEvent.GetTags();
                    break;
                case "place":
                    FilterPlacesByPageId(searchLVM, String.IsNullOrWhiteSpace(page) ? 1 : Int32.Parse(page));
                    searchLVM.Tags = TagPlace.GetTags();
                    break;
                default:
                    searchLVM.Tags = TagFilm.GetTags();
                    searchLVM.Tags.AddRange(TagEvent.GetTags());
                    searchLVM.Tags.AddRange(TagPlace.GetTags());

                    break;
            }
            return View("Search", searchLVM);
        }

        #region Films

        SearchListViewModel FilterFilmsByPageId(SearchListViewModel searchLVM, int id)
        {
            var pageFilms = new List<FilmViewModel>();
            int count = 0;
            int idFinish = id + 5;

            foreach (var f in searchLVM.Films)
            {
                count++;
                if (count >= id && count <= idFinish)
                {
                    pageFilms.Add(f);
                }
            }
            searchLVM.Count = searchLVM.Films.Count;
            searchLVM.CurrentNumber = id;
            searchLVM.Films = pageFilms;

            return searchLVM;
        }


        public ActionResult FilmSelectGenre(Film genre_id, string btnGenre)
        {
            switch (btnGenre)
            {
                case "selectGenre":
                    if (ModelState.IsValid)
                    {
                        genre_id.Genres = Request.Form["Genres"];
                        FilmListViewModel filmLVM = new FilmListViewModel();
                        filmLVM.Films = new List<FilmViewModel>();
                        Int32 id = Int32.Parse(genre_id.Genres);
                        Genres g = Genres.GetGenres().Find(x => x.GenresId == id);
                        foreach (Film film in Film.GetFilms().Where(x => x.Genres == g.Name))
                        {
                            FilmViewModel filmVM = new FilmViewModel(film, null);
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

                foreach (Film f in Film.GetFilms().Where(x => x.Genres.Contains(id)))
                {
                    FilmViewModel filmVM = new FilmViewModel(f, user.UserId);
                    filmLVM.Films.Add(filmVM);
                }
                List<Genres> genres = Genres.GetGenres();
                genres.Add(new Genres() { GenresId = 0, Name = "<Не задано>" });
                ViewBag.DropDownValuesGenre = new SelectList(genres, "name", "name", id);

                return View("FilmsView", filmLVM);
            }
            return View();

        }

        #endregion

        #region Events

        SearchListViewModel FilterEventsByPageId(SearchListViewModel searchLVM, int id)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            var pageEvents = new List<EventViewModel>();
            int count = 0;
            int idFinish = id + 5;

            foreach (var f in searchLVM.Events)
            {
                count++;
                if (count >= id && count <= idFinish)
                {
                    pageEvents.Add(f);
                }
            }
            searchLVM.Count = searchLVM.Events.Count;
            searchLVM.CurrentNumber = id;
            searchLVM.Events = pageEvents;

            return searchLVM;
        }

        SearchListViewModel FillEventsByTagId(int? tagId, SearchListViewModel searchLVM)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            EventListViewModel eventLVM = new EventListViewModel();
            eventLVM.Events = new List<EventViewModel>();
            List<Event> events;
            if (tagId == null)
                events = Event.GetEvents().Where(x=>x.Location == user.Location && DateTime.Parse(x.DateStart) >= DateTime.Now).ToList();
            else
                events = Event.GetEventsByTag(tagId.Value);

            foreach (Event e in events)
            {
                EventViewModel eventVM = new EventViewModel(e, null);
                //filmLVM.Films.Add(filmVM);               
                searchLVM.Events.Add(eventVM);
            }
            return searchLVM;
        }

#endregion

        SearchListViewModel FilterByTitle(string text, SearchListViewModel searchLVM, string type)
        {
            List<FilmViewModel> filteredFilms;
            List<EventViewModel> filteredEvents;
            List<PlaceViewModel> filteredPlaces;
            switch (type)
            {
                case "film":
                    filteredFilms = searchLVM.Films.Where(x => x.Title.IndexOf(text) >= 0).ToList();
                    searchLVM.Films = filteredFilms;

                    break;
                case "event":
                    filteredEvents = searchLVM.Events.Where(x => x.Title.IndexOf(text) >= 0).ToList();
                    searchLVM.Events = filteredEvents;
                    break;
                case "place":
                    filteredPlaces = searchLVM.Places.Where(x => x.Title.IndexOf(text) >= 0).ToList();
                    searchLVM.Places = filteredPlaces;
                    break;
                default:
                    filteredFilms = searchLVM.Films.Where(x => x.Title.IndexOf(text) >= 0).ToList();
                    searchLVM.Films = filteredFilms;
                    filteredEvents = searchLVM.Events.Where(x => x.Title.IndexOf(text) >= 0).ToList();
                    searchLVM.Events = filteredEvents;
                    filteredPlaces = searchLVM.Places.Where(x => x.Title.IndexOf(text) >= 0).ToList();
                    searchLVM.Places = filteredPlaces;
                    break;
            }
            return searchLVM;
        }

        SearchListViewModel FillFilmsByTagId(int? tagId, SearchListViewModel searchLVM)
        {
            FilmListViewModel filmLVM = new FilmListViewModel();
            filmLVM.Films = new List<FilmViewModel>();
            List<Film> films;
            if (tagId == null)
                films = Film.GetFilms();
            else
                films = Film.GetFilmsByTag(tagId.Value);

            foreach (Film f in films)
            {
                FilmViewModel filmVM = new FilmViewModel(f,null);
                //filmLVM.Films.Add(filmVM);
                searchLVM.Films.Add(filmVM);
            }
            return searchLVM;
        }

        #region Places

        SearchListViewModel FilterPlacesByPageId(SearchListViewModel searchLVM, int id)
        {
            var pagePlaces = new List<PlaceViewModel>();
            int count = 0;
            int idFinish = id + 5;

            foreach (var p in searchLVM.Places)
            {
                count++;
                if (count >= id && count <= idFinish)
                {
                    pagePlaces.Add(p);
                }
            }
            searchLVM.Count = searchLVM.Places.Count;
            searchLVM.CurrentNumber = id;
            searchLVM.Places = pagePlaces;

            return searchLVM;
        }

        SearchListViewModel FillPlacesByTagId(int? tagId, SearchListViewModel searchLVM)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            PlaceListViewModel placeLVM = new PlaceListViewModel();
            placeLVM.Places = new List<PlaceViewModel>();
            List<Place> places;
            if (tagId == null)
                places = Place.GetPlaces().Where(x=>x.Location==user.Location).ToList();
            else
                places = Place.GetPlacesByTag(tagId.Value);

            foreach (Place e in places)
            {
                PlaceViewModel placeVM = new PlaceViewModel(e, null);
                //filmLVM.Films.Add(filmVM);
                searchLVM.Places.Add(placeVM);
            }
            return searchLVM;
        }

        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class SearchListViewModel
    {
        public List<EventViewModel> Events { get; set; }
        public List<FilmViewModel> Films { get; set; }
        public List<PlaceViewModel> Places { get; set; }
        public Int32 Count { get; set; }
        public Int32 CurrentNumber { get; set; }
        public Int32 PageSize { get; set; }
        public List<Genres> Genres { get; set; }
        public List<Tag> Tags { get; set; }

        public SearchListViewModel()
        {
            PageSize = 4;
        }
    }
}
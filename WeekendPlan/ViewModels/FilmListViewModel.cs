using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class FilmListViewModel
    {
        public List<FilmViewModel> Films { get; set; }
        public Int32 Count { get; set; }
        public List<Genres> Genres { get; set; }
    }
}
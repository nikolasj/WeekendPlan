using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class PlaceListViewModel
    {
        public List<PlaceViewModel> Places { get; set; }
        public List<Tag> Tags { get; set; }
        public Int32 Count { get; set; }
        public Int32 CurrentNumber { get; set; }
    }
}
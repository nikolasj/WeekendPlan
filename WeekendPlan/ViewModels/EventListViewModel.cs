using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class EventListViewModel
    {
        public List<EventViewModel> Events { get; set; }
        public List<Tag> Tags { get; set; }
        public Int32 Count { get; set; }
        public Int32 CurrentNumber { get; set; }
    }
}
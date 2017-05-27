using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class IndexViewModel
    {
        public List<EventViewModel> Events { get; set; }
        public List<Tag> TagsUser { get; set; }
        public UserProfile User { get; set; }
    }
}
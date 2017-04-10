using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class AdministrationListViewModel
    {
        public List<ProfileViewModel> Profiles { get; set; }
        public List<OpportunityViewModel> Opportunities { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
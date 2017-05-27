using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeekendPlan.ViewModels
{
    public class OpportunityListViewModel
    {
        public List<OpportunityViewModel> Opportunities { get; set; }
        public List<OpportunityViewModel> SelectedRoute { get; set; }
        public DateTime Date { get; set; }
        public Int32 TypeVacation { get; set; }
        public Int32 TimeHour { get; set; }
        public List<OpportunityViewModel> AllOpportunities { get; set; }
    }
}
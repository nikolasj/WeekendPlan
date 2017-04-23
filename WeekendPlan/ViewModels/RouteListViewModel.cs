using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeekendPlan.ViewModels
{
    public class RouteListViewModel
    {
        public List<RouteViewModel> Routes { get; set; }
        public DateTime Date { get; set; }
        public Int32 TypeVacation { get; set; }
        public Int32 TimeHour { get; set; }
        public List<String> TagsByUser { get; set; }
        public Int32 TypeTransport { get; set; }
        public String AllWeather { get; set; }
    }
}
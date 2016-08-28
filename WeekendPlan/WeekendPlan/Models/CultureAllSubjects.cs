using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class CultureAllSubjects
    {
        [JsonProperty("info")]
        public Info Information { get; set; }
        [JsonProperty("data")]
        public List<FederalSubject> FederalSubjects { get; set; }
    }
}
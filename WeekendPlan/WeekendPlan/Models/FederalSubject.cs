using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class FederalSubject
    {
        //{"federal_subject_id":2,"ll":"(52.536717,31.933222)","objects_count":1,"title":"Новозыбков","id":32,"events_count":"4"}
        [JsonProperty("federal_subject_id")]
        public Int32 FederalSubjectId { get; set; }
        [JsonProperty("ll")]
        public String L1 { get; set; }
        [JsonProperty("objects_count")]
        public Int32 ObjectsCount { get; set; }
        [JsonProperty("title")]
        public String Title { get; set; }
        [JsonProperty("id")]
        public Int32 Id { get; set; }
        [JsonProperty("events_count")]
        public Int32 EventsCount { get; set; }
    }
}
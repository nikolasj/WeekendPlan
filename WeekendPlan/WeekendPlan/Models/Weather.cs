using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class Weather
    {
        public String Main { get; set; }
        public String Description { get; set; }

        [JsonProperty("id")]
        public Int32 Id { get; set; }
        public String Icon { get; set; }
    }
}
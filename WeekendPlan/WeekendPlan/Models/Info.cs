using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class Info
    {
        //"page_size":50,"count":79,"pages_count":2,"page":1
        [JsonProperty("page_size")]
        public Int32 PageSize { get; set; }
        [JsonProperty("count")]
        public Int32 Count { get; set; }
        [JsonProperty("pages_count")]
        public Int32 PagesCount { get; set; }
        [JsonProperty("page")]
        public Int32 Page { get; set; }
    }
}
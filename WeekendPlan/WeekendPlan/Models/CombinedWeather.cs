using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class CombinedWeather
    {
        [JsonProperty("weather")]
        List<Weather> WeatherInfo;
    }
}
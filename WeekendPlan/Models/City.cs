using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class City
    {
        [Key]
        [Column("city_id")]
        public Int32 CityId { get; set; }
        [Column("slug")]
        public String Slug { get; set; }
        [Column("name")]
        public String Name { get; set; }
        [Column("timezone")]
        public String Timezone { get; set; }
        //[Column("coords")]
        //public Double Coords { get; set; }
        [Column("language")]
        public String Language { get; set; }

        public static List<City> GetCities()
        {
            DbConnect connector = new DbConnect();
            List<City> cities = connector.Cities.ToList<City>();

            return cities;
        }
    }
}
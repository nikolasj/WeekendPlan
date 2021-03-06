﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WeekendPlan.Models;

namespace WeekendPlan.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var user = new User()
            {
                Id = "404",
                Email = "chernikov@gmail.com",
                UserName = "rollinx",
                Name = "Andrey",
                FirstName = "Andrey",
                MiddleName = "Alexandrovich",
                LastName = "Chernikov",
                Gender = "M"
            };
            var jsonUser = JsonConvert.SerializeObject(user);

            var jsonUserSource = "{\"Id\":\"405\",\"Name\":\"Andrey\",\"FirstName\":\"Andrey\",\"MiddleName\":\"Alexandrovich\",\"LastName\":\"Chernikov\",\"UserName\":\"rollinx\",\"Gender\":\"M\",\"Email\":\"chernikov@gmail.com\"}";

            var user2 = JsonConvert.DeserializeObject<User>(jsonUserSource);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View("About");
        }

        public ActionResult Map()
        {
            ViewBag.Message = "Your application description page.";

            return View("Map");
        } 

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            var w = new Weather()
            {
                Main = "Rainy",
                Description = "light rain",
                Id = 222,
                Icon = "02d"
            };

            var jsonW = JsonConvert.SerializeObject(w);

            string json = "{\"id\":801,\"Main\":\"Clouds\",\"Description\":\"few clouds\",\"Icon\":\"02d\"}";
            var w2 = JsonConvert.DeserializeObject<Weather>(json);

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://api.openweathermap.org");
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    var response = client.GetAsync("data/2.5/weather?q=Moscow&appid=0a28974e139fd12ec95731b95608263e").Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        string responseString = response.Content.ReadAsStringAsync().Result;
            //    }
            //}
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.culture.ru");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("api/v1/cities?criteria=ново").Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    CultureAllSubjects c = JsonConvert.DeserializeObject <CultureAllSubjects>(responseString);
                }
            }
            return View();
        }
    }
}
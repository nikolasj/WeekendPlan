using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using WeekendPlan.Models;

[assembly: OwinStartupAttribute(typeof(WeekendPlan.Startup))]
namespace WeekendPlan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }

    }
}

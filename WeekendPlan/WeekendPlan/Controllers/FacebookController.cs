using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeekendPlan.Models;

namespace WeekendPlan.Controllers
{
    public class FacebookController : Controller
    {
        // GET: Facebook
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //private FbProvider fbProvider;

        //protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        //{
        //    fbProvider = new FbProvider();
        ////    fbProvider.Config = FacebookSetting;
        //    base.Initialize(requestContext);
        //}

        //public ActionResult Index()
        //{
        //    return Redirect(fbProvider.Authorize("http://" + HostName + "/Facebook/Token"));
        //}

        //public ActionResult Token()
        //{
        //    if (Request.Params.AllKeys.Contains("code"))
        //    {
        //        var code = Request.Params["code"];
        //        if (fbProvider.GetAccessToken(code, "http://" + HostName + "/Facebook/Token"))
        //        {
        //            var jObj = fbProvider.GetUserInfo();
        //            var fbUserInfo = JsonConvert.DeserializeObject<FbUserInfo>(jObj.ToString());

        //            return View(fbUserInfo);
        //        }

        //    }
        //    return View("CantInitialize");
        //}

    }
}
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WeekendPlan.Models
{
    public class FbProvider 
    {
        //private static string AuthorizeUri = "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope=email";
       // private static string GetAccessTokenUri = "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}";
       // private static string GetUserInfoUri = "https://graph.facebook.com/me?access_token={0}";

        //private static string GraphUri = "https://graph.facebook.com/{0}";

        //public IFbAppConfig Config { get; set; }

        //public string AccessToken { get; set; }

        //public string Authorize(string redirectTo)
        //{
        //    return string.Format(AuthorizeUri, Config.AppID, redirectTo);
        //}

        //public bool GetAccessToken(string code, string redirectTo)
        //{
        //    var request = string.Format(GetAccessTokenUri, Config.AppID, redirectTo, Config.AppSecret, code);
        //    WebClient webClient = new WebClient();
        //    string response = webClient.DownloadString(request);
        //    try
        //    {
        //        var pairResponse = response.Split('&');
        //        AccessToken = pairResponse[0].Split('=')[1];
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public JObject GetUserInfo()
        //{
        //    var request = string.Format(GetUserInfoUri, AccessToken);
        //    WebClient webClient = new WebClient();

        //    string response = webClient.DownloadString(request);
        //    return JObject.Parse(response);
        //}
    }
}
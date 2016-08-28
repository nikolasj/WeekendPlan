using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class FacebookSetting : ConfigurationSection, IFbAppConfig
    {
        [ConfigurationProperty("AppID", IsRequired = true)]
        public string AppID
        {
            get
            {
                return this["AppID"] as string;
            }
            set
            {
                this["AppID"] = value;
            }
        }

        [ConfigurationProperty("AppSecret", IsRequired = true)]
        public string AppSecret
        {
            get
            {
                return this["AppSecret"] as string;
            }
            set
            {
                this["AppSecret"] = value;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public static class Helper
    {
        public static int GetPriceAverage(string val)
        {
            val = val.Trim();
            val = val.Replace("  ", " ");
            var allVals = val.Split(new string[] { "от", "до", "-", " ", "руб", "руб.", "рублей", "рублей." }, StringSplitOptions.RemoveEmptyEntries);
            List<Int32> vals = new List<int>();
            //String[] val2 = val.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            //if (val2.Length > 1)
            //{
            //    allVals = val2; 300700
            //}
            foreach (var v in allVals)
            {
                int n = 0;
                Int32.TryParse(v, out n);
                if (n != 0)
                    vals.Add(n);
            }

            return (vals.Count != 0) ? (int)vals.Average() : 0;
        }

        public static String ConvertDateStartHourToInt(String dateStart)
        {
            String[] dateVals = dateStart.Split(' ', ':');

            return dateVals[1];
        }

        public static int GetTypeVacationByOpportunity(Opportunity opportunity)
        {
            var activity = new string[] { "парк", "скалодром", "спортивный", "велосипед", "марафон", "вечеринки", "потанцевать", "пешеходные" };
            foreach (var v in activity)
                if (opportunity.Description.ToLower().Contains(v.ToLower()) ||
                    opportunity.Title.ToLower().Contains(v.ToLower()) ||
                    opportunity.Tags.Contains(v.ToLower()))
                    return 1;
            return 2;
        }
    }
}
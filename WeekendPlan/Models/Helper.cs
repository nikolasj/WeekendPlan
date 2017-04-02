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
            var allVals = val.Split(new string[] { "от", "до", "-", " " }, StringSplitOptions.RemoveEmptyEntries);
            List<Int32> vals = new List<int>();
            foreach (var v in allVals)
            {
                int n = 0;
                Int32.TryParse(v, out n);
                if (n != 0)
                    vals.Add(n);
            }

            return (int)vals.Average();
        }

        public static String ConvertDateStartHourToInt(String dateStart)
        {
            String[] dateVals = dateStart.Split(' ', ':');
            return dateVals[1];
        }
    }
}
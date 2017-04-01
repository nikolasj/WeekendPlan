using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeekendPlan.ViewModels
{
    public class QuestionnaireListViewModel
    {
        public List<QuestionnaireViewModel> Questions { get; set; }
        public Int32 QuestionNumber { get; set; }
        public String Image { get; set; }
    }
}
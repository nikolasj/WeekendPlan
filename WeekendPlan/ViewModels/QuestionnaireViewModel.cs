using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.ViewModels
{
    public class QuestionnaireViewModel
    {
        public Int32 QuestionId { get; set; }
        public Int32? ParentId { get; set; }
        public String Questions { get; set; }
        public String TagQuestion { get; set; }
        public String Image { get; set; }
        public Int32 NextQuestionId { get; set; }
        public List<QuestionnaireViewModel> Children { get; set; }
        public Int32 QuestionNumber { get; set; }

        public QuestionnaireViewModel(Question q)
        {
            QuestionId = q.QuestionId;
            ParentId = q.ParentId;
            Questions = q.Questions;
            TagQuestion = q.TagQuestion;
            Image = q.Image;
            Children = new List<QuestionnaireViewModel>();
        }

    }
}
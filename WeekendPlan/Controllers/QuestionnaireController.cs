using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeekendPlan.Models;
using WeekendPlan.ViewModels;

namespace WeekendPlan.Controllers
{
    public class QuestionnaireController : Controller
    {
        public ActionResult Questionnaire(int? questionId, string type)
        {
            QuestionnaireListViewModel questionnareLVM = new QuestionnaireListViewModel();
            questionnareLVM.Questions = new List<QuestionnaireViewModel>();

            if(questionId.Value == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var root = Question.GetRootQuestion();
            for (int i = 0; i < root.Count; i++)
            {

                QuestionnaireViewModel questionnareVM = new QuestionnaireViewModel(root[i]);
                questionnareVM.QuestionNumber = i;
                if (root[i].QuestionId == questionId)
                {
                    questionnareLVM.Image = questionnareVM.Image;
                    questionnareLVM.QuestionNumber = i;
                }
                if (i < root.Count - 1)
                {                  
                    questionnareVM.NextQuestionId = root[i + 1].QuestionId;
                }
                var children = Question.GetQuestionByParentId(root[i].QuestionId);
                if (children != null)
                {
                    foreach (var c in children)
                    {
                        questionnareVM.Children.Add(new QuestionnaireViewModel(c));
                    }
                }               
                questionnareLVM.Questions.Add(questionnareVM);

            }

           // questionnareLVM.QuestionNumber = 0;
            return View("Questionnaire", questionnareLVM);
        }

        public ActionResult SaveTags(int questionId, int nextQuestionId, string saveTags)
        {
            switch (saveTags)
            {
                case "Дальше":
                    if (ModelState.IsValid)
                    {
                        UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
                        var tagsId = Request.Form["saveChildren"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        var questions = Question.GetQuestionByParentId(questionId);
                        foreach (var t in questions)
                        {
                            foreach (var tId in tagsId)
                            {
                                if (t.QuestionId == Int32.Parse(tId) && !FindTagByUser(t.TagQuestion))
                                {
                                    Tag userTag = new Tag() { Text = t.TagQuestion, UserId = user.UserId };
                                    Tag userTags = Tag.AddTag(userTag);
                                }
                            }

                        }
                    }
                    return Redirect("~/Questionnaire/Questionnaire/?questionId="+nextQuestionId);
            }
            return View();

        }

        private bool FindTagByUser(string tagText)
        {
            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
            var tagsByText = Tag.GetUserIdByTextTag(tagText);
            foreach (var t in tagsByText)
            {
                if (t.UserId == user.UserId)
                {
                    return true;
                }
            }
            return false;
        }
        //public ActionResult QuestionnaireSelect(string id, string question)
        //{
        //    //int number = Int32.Parse(id) + 1;
        //    //switch (question)
        //    //{
        //    //    case "Да":
        //    //        if (ModelState.IsValid)
        //    //        {
        //    //            UserProfile user = UserProfile.GetUsers().Find(x => x.Name.ToLower() == User.Identity.Name.ToLower());
        //    //            QuestionnaireViewModel questionnareVM = new QuestionnaireViewModel();
        //    //            var tagsByText = Tag.GetUserIdByTextTag(questionnareVM.TagsByQuestion[Int32.Parse(id)]);
        //    //            foreach (var t in tagsByText)
        //    //            {
        //    //                if (t.UserId == user.UserId)
        //    //                {
        //    //                    return Questionnaire(number);
        //    //                }
        //    //            }
        //    //            Tag userTag = new Tag() { Text = questionnareVM.TagsByQuestion[Int32.Parse(id)], UserId = user.UserId };
        //    //            Tag userTags = Tag.AddTag(userTag);
        //    //        }
        //    //        return Questionnaire(number);
        //    //}
        //    //return Questionnaire(number);

        //    QuestionnaireListViewModel questionnareLVM = new QuestionnaireListViewModel();
        //    questionnareLVM.Questions = new List<QuestionnaireViewModel>();

        //    var root = Question.GetRootQuestion();
        //    for (int i = 0; i < root.Count; i++)
        //    {
        //        QuestionnaireViewModel questionnareVM = new QuestionnaireViewModel(root[i]);
        //        if (i <= root.Count - 1)
        //        {
        //            questionnareVM.NextQuestionId = root[i + 1].QuestionId;
        //        }
        //        questionnareLVM.Questions.Add(questionnareVM);
        //    }

        //    questionnareLVM.QuestionNumber = (questionNumber == null) ? 0 : questionNumber.Value;
        //    return View("Questionnaire", questionnareLVM);
        //}
    }
}
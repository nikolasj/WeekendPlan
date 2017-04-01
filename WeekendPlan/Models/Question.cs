using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class Question
    {
        [Key]
        [Column("question_id")]
        public Int32 QuestionId { get; set; }
        [Column("parent_id")]
        public Int32? ParentId { get; set; }
        [Column("question")]
        public String Questions { get; set; }
        [Column("tag_question")]
        public String TagQuestion { get; set; }
        [Column("image")]
        public String Image { get; set; }


        public static List<Question> GetQuestionByParentId(int? questionId)
        {
            if (questionId == null) return null;
            DbConnect connector = new DbConnect();
            List<Question> search = connector.Questions.Where(x => x.ParentId == questionId).ToList();
            if (search != null)
            {
                return search;
            }

            return null;
        }

        public static List<Question> GetRootQuestion()
        {
            DbConnect connector = new DbConnect();
            List<Question> search = connector.Questions.Where(x => x.ParentId == null).ToList();
            if (search != null)
            {
                return search;
            }

            return null;
        }
    }
}
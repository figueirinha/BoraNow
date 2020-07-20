using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Quizzes
{
    public class QuizAnswer : Entity
    {
        private string _answer;

        [Required]
        public string Answer
        {
            get
            {
                return _answer;
            }
            set
            {
                _answer = value;
                RegisterChange();
            }
        }

        [ForeignKey("QuizQuestion")]
        public Guid QuizQuestionId { get; set; }
        public virtual QuizQuestion QuizQuestion { get; set; }


        public virtual ICollection<CategoryQuizAnswer> QuizAnswerCategories { get; set; }

        public QuizAnswer(string answer, Guid quizQuestionId) : base()
        {
            _answer = answer;
            QuizQuestionId = quizQuestionId;
        }

        public QuizAnswer(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string answer, Guid quizQuestionId) : base(id, createAt, updateAt, isDeleted)
        {
            _answer = answer;
            QuizQuestionId = quizQuestionId;
        }
    }
}

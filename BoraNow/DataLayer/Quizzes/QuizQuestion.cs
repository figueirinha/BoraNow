using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Quizzes
{
    public class QuizQuestion : Entity
    {
        private string _question;

        [Required]
        public string Question
        {
            get
            {
                return _question;
            }
            set
            {
                _question = value;
                RegisterChange();
            }
        }

        [ForeignKey("Quiz")]
        public Guid QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
        public virtual ICollection<QuizAnswer> QuizAnwsers { get; set; }

        public QuizQuestion(string question, Guid quizId) : base()
        {
            _question = question;
            QuizId = quizId;
        }

        public QuizQuestion(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string question, Guid quizId) : base(id, createAt, updateAt, isDeleted)
        {
            _question = question;
            QuizId = quizId;
        }
    }
}

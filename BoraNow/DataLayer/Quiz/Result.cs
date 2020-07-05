using Recodme.RD.BoraNow.DataLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.RD.BoraNow.DataLayer.Quiz
{
    public class Result : Entity
    {
        private string _title;

        [Required]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RegisterChange();
            }
        }

        private DateTime _date;
        
        [Required]
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                RegisterChange();
            }
        }

        [ForeignKey("Quiz")]
        public Guid QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
        public virtual ICollection<ResultInterestPoint> InterestPointResults { get; set; }

        public Result(string title, DateTime date, Guid quizId) : base()
        {
            _title = title;
            _date = date;
            QuizId = quizId;
        }

        public Result(Guid id, DateTime createAt, DateTime updateAt, bool isDeleted, string title, DateTime date, Guid quizId) : base(id, createAt, updateAt, isDeleted)
        {
            _title = title;
            _date = date;
            QuizId = quizId;
        }
    }
}
